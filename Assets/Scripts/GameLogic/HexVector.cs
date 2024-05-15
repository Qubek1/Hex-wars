using GameLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HexVector
{
    // TODO: override Equals and GetHashCode functions
    public int x, y, z;

    public HexVector(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        if (x + y + z != 0)
        {
            throw new ArgumentException("Hex Vector was not created properly, coordinates always have to add up to 0\n" + "x: " + x + "y: " + y + "z: " + z + ",sum: " + (x+y+z));
        }
    }

    public HexVector(int x, int y)
    {
        this.x=x;
        this.y=y;
        z = -x - y;
    }

    public static HexVector operator +(HexVector a, HexVector b)
    {
        return new HexVector(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static HexVector operator -(HexVector a, HexVector b)
    {
        return new HexVector(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static HexVector operator *(HexVector a, int m)
    {
        return new HexVector(a.x * m, a.y * m, a.z * m);
    }

    public static bool operator ==(HexVector a, HexVector b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public static bool operator !=(HexVector a, HexVector b)
    {
        return !(a == b);
    }

    public static readonly List<HexVector> directionVectors = new List<HexVector> 
    {
        new HexVector(1, 0 ,-1),
        new HexVector(-1, 0, 1),
        new HexVector(0, 1, -1),
        new HexVector(0, -1, 1),
        new HexVector(1, -1, 0),
        new HexVector(-1, 1, 0)
    };

    public List<HexVector> getNeighbours()
    {
        List<HexVector> neighbours = new List<HexVector>(6);
        foreach (HexVector v in directionVectors)
        {
            neighbours.Add(this + v);
        }
        return neighbours;
    }

    public static Vector2 yVector = new Vector2(1, 0);
    public static Vector2 zVector = new Vector2(-0.5f, -Mathf.Sqrt(3)/2f);
    public static Vector2 xVector = new Vector2(-0.5f, Mathf.Sqrt(3)/2f);

    public Vector2 ConvertToRealPosition()
    {
        return x * xVector + y * yVector + z * zVector;
    }

    public override string ToString()
    {
        return "x: " + x + ", y: " + y + ", z:" + z;
    }
}
