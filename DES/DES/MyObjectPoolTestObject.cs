using System;
public class MyObjectPoolTestObject
{
    public string data;

    public MyObjectPoolTestObject() {
        data = "default";
    }

    public MyObjectPoolTestObject(string incoming) {
        data = incoming;
    }

    public static MyObjectPoolTestObject MakeObject(string incoming) {
        return new MyObjectPoolTestObject(incoming);
    }

    public static MyObjectPoolTestObject MakeObject()
    {
        return new MyObjectPoolTestObject();
    }

}

