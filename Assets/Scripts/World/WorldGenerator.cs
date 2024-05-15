using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;

public class WorldGenerator : MonoBehaviour
{
    public HeightMapParameters heightMapParameters;
    private HeightMapGenerator heightMapGenerator;

    public Transform Parent;

    public GameObject GrassTile;
    public GameObject ForestTile;
    public GameObject MountainTile;
    public GameObject WaterTile;
    public Gradient GrassColor;
    public Gradient ForestColor;
    public Gradient WaterColor;
    public float RandomPositionRange;

    public Tile[,] TilesGrid;
    public int GridSizeX;
    public int GridSizeY;
    public float NoiseStrenght;
    public float NoiseSize;
    public float GrassHeight;
    public float ForestHeight;
    public float MountainHeight;
    public float ProxyStrenght;
    public int ProxyAmount;
    public int ProxySize;

    public float MountainsDensity;

    private float[,] HeightGrid;

    void Setup()
    {
        Tile.GrassGO = GrassTile;
        Tile.ForestGO = ForestTile;
        Tile.MountainGO = MountainTile;
        Tile.WaterGO = WaterTile;
        Tile.RandomPosRange = RandomPositionRange;

        TilesGrid = new Tile[GridSizeX, GridSizeY];
        HeightGrid = new float[GridSizeX, GridSizeY];

        heightMapParameters.center = new Vector2(GridSizeX / 2f, GridSizeY / 2f);
        heightMapGenerator = new HeightMapGenerator(heightMapParameters);
        heightMapGenerator.Generate();
    }

    void Awake()
    {
        Setup();

        //ClearHeightGrid();
        //for(int i=0; i<ProxyAmount; i++)
        //    Proxy(new Vector2((int)Random.Range(0,GridSizeX), (int)Random.Range(0, GridSizeY)), ProxySize);
        //ApplyNoise();
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                HeightGrid[x, y] = heightMapGenerator.GetHeightInPosition(new Vector2(x, y));
            }
        }
        MakeTiles();
    }

    private void Proxy(Vector2 Position, int size)
    {
        Vector2[] ProxyTiles = TilesInRange(Position, size);
        Vector2[] newProxyTiles = TilesInRange(Position, size*2-1);
        foreach (Vector2 i in ProxyTiles)
        {
            //HeightGrid[(int)i.x, (int)i.y] = Mathf.Max(HeightGrid[(int)i.x, (int)i.y], ((float)size+20f)/20f * ProxyStrenght / (Vector2.Distance(Position, i) + 20) * 20f);
            //HeightGrid[(int)i.x, (int)i.y] += ((float)size + 10f) / 10f * ProxyStrenght / (Vector2.Distance(Position, i) + 3) * 3f;
            HeightGrid[(int)i.x, (int)i.y] = (Mathf.Max(HeightGrid[(int)i.x, (int)i.y], (((float)size + MountainsDensity) / (Vector2.Distance(Position, i) + 1f)) * ProxyStrenght));
        }
        for(int i=0; i<size/2+1; i++)
        {
            int newProxySize = (int)Random.Range(0f, (float)size*0.8f);
            if (newProxySize < 2)
                continue;
            Proxy( ProxyTiles[(int)(Random.Range(1, ProxyTiles.Length - 1))], newProxySize);
        }
    }

    private void MakeTiles()
    {
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                Vector2 TilePosition = new Vector2(x, y);
                if (HeightGrid[x, y] >= MountainHeight)
                {
                    TilesGrid[x, y] = new Tile(Tile.Type.Mountain, TilePosition, HeightGrid[x, y]);
                }
                else if (HeightGrid[x, y] >= ForestHeight)
                {
                    TilesGrid[x, y] = new Tile(Tile.Type.Forest, TilePosition, HeightGrid[x, y]);
                }
                else if (HeightGrid[x, y] >= GrassHeight)
                {
                    TilesGrid[x, y] = new Tile(Tile.Type.Grass, TilePosition, HeightGrid[x, y]);
                }
                else
                {
                    TilesGrid[x, y] = new Tile(Tile.Type.Water, TilePosition, HeightGrid[x, y]);
                }
                CreateTile(TilesGrid[x,y]);
            }
        }
    }

    private void ClearHeightGrid()
    {
        for (int x = 0; x < GridSizeX; x++)
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                HeightGrid[x, y] = 0;
            }
        }
    }

    private void ApplyNoise()
    {
        for(int x=0; x<GridSizeX; x++)
        {
            for(int y=0; y<GridSizeY; y++)
            {
                HeightGrid[x, y] += Mathf.PerlinNoise(x / NoiseSize, y / NoiseSize) * NoiseStrenght; 
            }
        }
    }

    public void CreateTile(Tile tile)
    {
        tile.TileGO = Instantiate(tile.GetTileGO(), transform);
        tile.TileGO.transform.position = tile.RealPosition;
        tile.TileGO.transform.Rotate(Vector3.forward, Random.Range(0, 6) * 60f);
        //tile.TileGO.transform.position += new Vector3(0, tile.height, 0);
        Material TileMaterial = tile.TileGO.GetComponent<MeshRenderer>().material;
        if (tile.TileType == Tile.Type.Grass)
            TileMaterial.color = GrassColor.Evaluate((tile.height - GrassHeight) / (1 - GrassHeight));
        if (tile.TileType == Tile.Type.Forest)
            TileMaterial.color = ForestColor.Evaluate(Random.Range(0f, 1f));
        if (tile.TileType == Tile.Type.Water)
            TileMaterial.color = WaterColor.Evaluate(tile.height/GrassHeight);
    }

    public static Vector3 CalculateRealPos(Vector2 GridPos)
    {
        Vector3 NewPos;
        NewPos.x = GridPos.x * 1.5f;
        NewPos.z = GridPos.y * Mathf.Sqrt(3);
        if (GridPos.x % 2 == 0)
            NewPos.z += Mathf.Sqrt(3) / 2f;
        NewPos.y = 0f;
        return NewPos;
    }

    public Vector2[] TilesInRange(Vector2 Pos, int range)
    {
        List<Vector2> ret = new List<Vector2>();
        ret.Add(Pos);
        Vector2 CurrentPos = new Vector2(Pos.x, Pos.y);
        for(int i= 2; i<=range; i++)
        {
            CurrentPos.y--;
            for (int j = 0; j < i - 1; j++)
            {
                if (DoesTileExist(CurrentPos))
                    ret.Add(CurrentPos);
                CurrentPos = RightUp(CurrentPos);
            }
            for (int j = 0; j < i - 1; j++)
            {
                if (DoesTileExist(CurrentPos))
                    ret.Add(CurrentPos);
                CurrentPos.y++;
            }
            for (int j = 0; j < i - 1; j++)
            {
                if (DoesTileExist(CurrentPos))
                    ret.Add(CurrentPos);
                CurrentPos = LeftUp(CurrentPos);
            }
            for (int j = 0; j < i - 1; j++)
            {
                if (DoesTileExist(CurrentPos))
                    ret.Add(CurrentPos);
                CurrentPos = Leftdown(CurrentPos);
            }
            for (int j = 0; j < i - 1; j++)
            {
                if (DoesTileExist(CurrentPos))
                    ret.Add(CurrentPos);
                CurrentPos.y--;
            }
            for (int j = 0; j < i - 1; j++)
            {
                if (DoesTileExist(CurrentPos))
                    ret.Add(CurrentPos);
                CurrentPos = Rightdown(CurrentPos);
            }
        }
        return ret.ToArray();
    }

    public bool DoesTileExist(Vector2 Pos)
    {
        if (Pos.x < 0 || Pos.y < 0)
            return false;
        if (Pos.x >= GridSizeX || Pos.y >= GridSizeY)
            return false;
        return true;
    }
    public static Vector2 RightUp(Vector2 Pos)
    {
        if(Pos.x %2 == 0)
        {
            return new Vector2(Pos.x + 1, Pos.y + 1);
        }
        return new Vector2(Pos.x + 1, Pos.y);
    }
    public static Vector2 LeftUp(Vector2 Pos)
    {
        if (Pos.x % 2 == 0)
        {
            return new Vector2(Pos.x - 1, Pos.y + 1);
        }
        return new Vector2(Pos.x - 1, Pos.y);
    }
    public static Vector2 Rightdown(Vector2 Pos)
    {
        if (Pos.x % 2 == 0)
        {
            return new Vector2(Pos.x + 1, Pos.y);
        }
        return new Vector2(Pos.x + 1, Pos.y - 1);
    }
    public static Vector2 Leftdown(Vector2 Pos)
    {
        if (Pos.x % 2 == 0)
        {
            return new Vector2(Pos.x - 1, Pos.y);
        }
        return new Vector2(Pos.x - 1, Pos.y - 1);
    }
}
