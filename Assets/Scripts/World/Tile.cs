using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public GameObject TileGO;
    public Vector3 RealPosition;
    public Vector2 GridPosition;
    public Type TileType;
    public float height;

    public static GameObject GrassGO;
    public static GameObject ForestGO;
    public static GameObject MountainGO;
    public static GameObject WaterGO;
    public static float RandomPosRange;

    public enum Type
    {
        Grass,
        Forest,
        Mountain,
        Water
    }

    public Tile()
    { }

    public Tile(Type _type, Vector2 _GridPosition, float heigth)
    {
        TileType = _type;
        GridPosition = _GridPosition;
        RealPosition = WorldGenerator.CalculateRealPos(GridPosition);
        this.height = heigth;
        if (TileType == Type.Water)
            RealPosition.y -= 0.3f;
        else
            RealPosition.y += Random.Range(-RandomPosRange, RandomPosRange) + (height - 0.75f) * 15f;
    }

    public GameObject GetTileGO()
    {
        if (TileType == Type.Grass)
            return GrassGO;
        if (TileType == Type.Forest)
            return ForestGO;
        if (TileType == Type.Mountain)
            return MountainGO;
        if (TileType == Type.Water)
            return WaterGO;
        return WaterGO;
    }
}
