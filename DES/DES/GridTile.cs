using System;
using System.Collections.Generic;

namespace DES
{
    public struct coords
    {
        public int x;
        public int y;
    }

    [Serializable]
    public class GridTile
    {
        public static float VolumeHardCap;
        public static float VolumeSoftCap;
        public static int ObjectLimit;

        public coords Coordinates;
        public Floor Floor;
        public List<Item> Things;
        public float ThingVolume;
        public bool Passable;

        public static bool Test()
        {
            GridTile.ObjectLimit = 5;
            GridTile.VolumeSoftCap = 30;
            GridTile.VolumeHardCap = 50;

            coords c;
            c.x = 1;
            c.y = 2;
            GridTile gt = new GridTile(c);
            if (gt.Coordinates.x != 1 || gt.Coordinates.y != 2)
            {
                throw new Exception("Grid Tile failed to set coordinates");
            }

            Item a = new Item("a", 3, 10, 0);
            Item b = new Item("b", 3, 9, 0);
            Item C = new Item("c", 3, 8, 0);
            Item d = new Item("d", 3, 7, 0);
            Item e = new Item("e", 3, 6, 0);
            Item f = new Item("f", 3, 29, 0);

            gt.AddThing(a);
            gt.AddThing(b);

            if (gt.ThingVolume != 19)
            {
                throw new Exception("GridTile has incorrect volume expected 19 got: " + gt.ThingVolume);
            }

            gt.AddThing(C);
            gt.AddThing(d);
            gt.AddThing(e);
            try
            {
                gt.AddThing(f);
                throw new Exception("GridTile exceeded maximum object capacity");
            }
            catch (GridTileException)
            {

            }

            if (gt.ThingVolume != 40)
            {
                throw new Exception("GridTile has incorrect volume expected 40 got: " + gt.ThingVolume);
            }

            if (gt.Passable == true)
            {
                throw new Exception("GridTile is passable but exceeds volume softcap");
            }

            gt.RemoveThing(a);
            if (gt.Passable == false)
            {
                throw new Exception("GridTile is impassable but does not exceed volume softcap");
            }
            if (gt.ThingVolume != 30)
            {
                throw new Exception("GridTile has incorrect volume expected 30 got: " + gt.ThingVolume);
            }

            return true;
        }

        public GridTile(coords coords)
        {
            Coordinates = coords;
            Things = new List<Item>();
            Passable = true;
            ThingVolume = 0;
        }

        public void AddThing(Item thing)
        {
            if (Things.Count >= ObjectLimit)
            {
                throw new GridTileException(String.Format("Grid is full at [{0},{1}]. Attempting to insert item {2}",
                    Coordinates.x, Coordinates.y, thing.ToString()), this);
            }
            else if (ThingVolume + thing.Volume > GridTile.VolumeHardCap)
            {
                throw new GridTileException(String.Format("Grid is overflowing at [{0},{1}]. Attempting to insert item {2}",
                    Coordinates.x, Coordinates.y, thing.ToString()), this);
            }
            else
            {
                Things.Add(thing);
                ThingVolume += thing.Volume;

                if (ThingVolume > GridTile.VolumeSoftCap)
                {
                    Passable = false;
                }
            }
        }

        public void RemoveThing(Item thing)
        {
            if (Things.Contains(thing))
            {
                Things.Remove(thing);
                ThingVolume -= thing.Volume;
                if (ThingVolume <= GridTile.VolumeSoftCap)
                {
                    Passable = true;
                }
            }
            else
            {
                throw new GridTileException(String.Format("Attempted to remove {2} at Grid [{0},{1}] but item does not exist",
                    Coordinates.x, Coordinates.y, thing.ToString()), this);
            }
        }
    }

    public class GridTileException : Exception
    {
        public GridTile tile;
        public GridTileException(string message, GridTile tile) : base(message)
        {
            this.tile = tile;
        }


    }

    [Serializable]
    public class Floor
    {

    }

}