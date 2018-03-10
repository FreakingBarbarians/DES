
Once upon a time in the late summer of 2017 I'd come up with the idea of a data-driven system describing actions in role-playing games. 

I realized that it was a challenging endeavor as actions in role-playing games can be as simple as *do three damage* to something as complex as rituals involving several people. So I tried to develop a robust system that would be able to encode a variety of different actions.

*Aaaand here's the result.*

# Dynamic Effect System [DES]
--------
DES is a bytecode interpreter that gives instructions to an extremely simple version of a cpu that will run mutating functions on target objects. The cpu features an instruction set that can be any void method, an argument stack, and a heap. The system, I feel, is very robust in its ability to express actions, and should be able to be expanded to express virtually any action that is turing computable (*I say should be because there are a lot of stub methods. I only have so much time!*).

Before we get into the code let's better define what an action is.

##### Action
---*Something that acts on an object to produce an "effect".*

Now this is pretty vague so we're going to steal some paradigms from d20 systems like Dungeons and Dragons. 

Let's take a look at the "delayed blast fireball" spell from 5th edition Dungeons and Dragons.

>A beam of yellow light flashes from your pointing finger, then condenses to linger at a chosen point within range as a glowing bead for the Duration. When the spell ends, either because your Concentration is broken or because you decide to end it, the bead blossoms with a low roar into an explosion of flame that spreads around corners. Each creature in a 20-foot-radius Sphere centered on that point must make a Dexterity saving throw. A creature takes fire damage equal to the total accumulated damage on a failed save, or half as much damage on a successful one.
>The spell's base damage is 12d6. If at the end of Your Turn the bead has not yet detonated, the damage increases by 1d6.

Immediately we can distill some requirements for our definition of an action.

We can break the action down into two parts:

##### A Header
Describes the action and "how" the action is applied to targets. It contains fields such as:
* Name
* Description
* Keywords (think elemental typing for actions)
* Targeting Type (is it ranged, is it melee? What is the Area of Effect?)
* Etc.

The important thing about these fields is that the enumerations they can have are compartively small. We can consider this as action "meta-data" that tells the system how to handle it.

##### An Effect
Expresses change on the target.

From the above example of the "Delayed Blast Fireball" we can see that the system will need to:

* Preform Attribute I/O (manipulation and reading of object attributes)
* Preform Arithmetic (Adding dice rolls, addition, etc.)
* Preform Boolean Logic (Comparators, Logic Gates)
* Branching (Conditions)
* Game-Specific Hooks (Creating particle effects, etc.)

The requirements listed reminded me of a lot of the things a simple CPU is capable of. And this model served as a core component in my system.


The result is something like this.


![alt text](https://raw.githubusercontent.com/FreakingBarbarians/FreakingBarbarians_Images/master/DES_FIG_2.png "Logo Title Text 1")

## The DES VM

To facilitate these operations I created a virtual CPU, which I've called the VM.
The VM runs on byte instructions (also known as ByteCode) in order to determine what methods to run, the arguments for each method, and when to run them.
Each effect is broken down as a series of instructions. Each instruction is in reality, an array of bytes accompanied by an array of integers that specify the start point of arguments. The VM also supports an argument stack as well as registers and a heap for long-term data persistence.

An easily expandable set of instructions allows us to encode an extremely varied set of actions.

![alt text](https://raw.githubusercontent.com/FreakingBarbarians/FreakingBarbarians_Images/master/DES_FIG_3.png "Logo Title Text 1")

## The DES Language
Since bytecode is not human readable, nor is it easily writeable, I have also built a customized windows form application for the creation of actions and the compilation of a custom language into byte isntructions.

A barebones specification can be found in the VM Utils class under the compile method.

Here is an example of a one line effect

`IDamageHealth i%10`

When compiled, the `IDamageHealth` will be turned into a function code that will call the IDamageHealth method in the VM class. 

The IDamageHealth Method expects exactly one integer argument. `i%x` indicates an integer argument with the value x.

Then the code above will call the `IDamageHealth` with the argument of `i%10` that will remove 10 hitpoints from the target.

In this instance the `I` in `IDamageHealth` stands for immediate, where the function arguments are contained within the instruction.

There is an equivalent `SDamageHealth` that expects the argument to be the top object on the stack.

In the meantime below is the delayed blast fireball spell put into this language.

```
Load s%ttl                  // loade time to live from heap (unique to this instance)
IJumpEQ i%0 $explode        // If time to live is 0 we jump to explode
Load s%disturbed            // If the ball was disturbed in any major way
IJumpEQ b%true $explode     // This flag will be set true outside. And we jump
IJump $end                  // Otherwise we don't explode and go to end
Explode: IRollDice i%12 i%6 // We roll 12d6, the perscribed base damage
Load s%rounds               // For each round alive, we add 1d6
SRollDiceI i%6              // Roll dat number!
SAddS                       // Add the damages up
SDamageHealth               // And apply the damage!
END                         // We use the special instruction END to flag
                            // This effect for deletion
End: Load s%rounds          // We load the round counter (initially 0)
SAddI i%1                   // And add one to it
Store s%rounds              // We store it back onto the heap
load s%ttl                  // load ttl
SAddI i%-1                  // Decrement it by 1
store s%ttl                 // store ttl back onto the heap
```

## DES' place in the world
Unfortunately, although DES is a very cool system (in my opinion at least), I doubt it would see much use in game production. And this is for several reasons.

**First** and foremost is the availability of robust and more functional alternatives. LUA is a good example of a system that does what DES does but better.

**Secondly** although the DES language is more human readable than bytecode, it's basically assembly (turing tarpit! D:). And I doubt there are a lot of game developers who would want to program in assembly. This problem could be circumvented by creating a higher level language on top of the assembly language, and huge improvements to the editor. But this is not something I have the time to do :)

So DES finds itself as an interesting project on my git, probably never to be touched again. The core functionality is there, but a lot of some-what important things (like saving actions to templates) are either stub or missing. From the outset I knew that DES was going to be a conceptual project, and I definitely have a greater appreciation for, and understanding of the engineering that goes behind systems like this.

\**cough*\* Also might be good to put on a resume \**cough*\*

## Classes of interest
The most interesting classes in my opinion are the VM and VMUtils classes.

I'll now list some cool  things I used to implement each class of interest.

#### VM - Responsible for parsing and running instructions.
* To facilitate instructions I used method attributes to give each instruction method a code. In initialization reflection was used to find these marked methods and add them to a dictionary.
* Lots of byte parsing. LOTS!
* Instructions follow this contract where, on invoke they expect that the stack has valid arguments for it. Otherwise we run into BIG problems. It's up to the effect creator to manage this properly.
* To facilitate branching Jump instructions were created as well, an instruction counter is incremented and changed.

#### VM Utils - Responsible for some utility fucntions, including compilation.
* To facilitate compilation, I created a string parser that turns DES Language into bytecode instructions. It even supports labels!
* Used two stage compilation and some complicated lookup tables so that label names could be resolved into instruction numbers.
* Lots of string parsing. LOTS!!!
