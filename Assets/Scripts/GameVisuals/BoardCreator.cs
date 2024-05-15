using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;
using TMPro;
using UnityEngine.Events;

namespace PlayerInterface
{
    public class BoardCreator : MonoBehaviour
    {
        public Board board;
        public Gradient GrassGradient;

        public List<GameObject> hexHeightPrefabs;
        public List<Gradient> heightLevelColorGradient;
        public GameObject[,] hexObjects;
        public List<Tile> tiles = new List<Tile>();
        public HexTileObject[,] hexTiles;

        public float hexRadius;
        public float heightMapStrenght;
        public float heightLevelsStrenght;

        public void CreateBoard()
        {
            hexObjects = new GameObject[board.arraySize, board.arraySize];
            hexTiles = new HexTileObject[board.arraySize, board.arraySize];
            for (int x = 0; x < board.arraySize; x++)
            {
                for (int y = 0; y < board.arraySize; y++)
                {
                    if (board.ValidateHexPosition(new HexVector(x, y)))
                    {
                        Vector2 realPosition = (new HexVector(x, y)).ConvertToRealPosition();
                        hexObjects[x, y] = Instantiate(hexHeightPrefabs[board.heightLevels[x, y]], new Vector3(realPosition.x, CalculateRealHeight(x, y), realPosition.y) * hexRadius, new Quaternion(0, 0, 0, 0), transform);
                        hexObjects[x, y].GetComponentInChildren<MeshRenderer>().material.color = GetHexColor(x, y);
                        tiles.Add(new Tile(Tile.Type.Water, new Vector2(x, y), board.heightMap[x, y]));
                        HexTileObject hexTileObject = hexObjects[x, y].GetComponent<HexTileObject>();
                        if (hexTileObject != null)
                        {
                            hexTileObject.hexPosition = new HexVector(x, y);
                            hexTileObject.height = board.heightMap[x, y];
                            hexTileObject.heightLevel = board.heightLevels[x, y];
                            hexTiles[x, y] = hexTileObject;
                        }
                    }
                }
            }
        }

        private float CalculateRealHeight(int x, int y)
        {
            if (board.heightLevels[x, y] == 0)
            {
                return 0;
            }
            return board.heightLevels[x, y] * heightLevelsStrenght + (board.heightMap[x, y] - board.boardParameters.heightLevels[board.heightLevels[x, y]]) * heightMapStrenght;
        }

        private Color GetHexColor(int x, int y)
        {
            float min = board.boardParameters.heightLevels[board.heightLevels[x, y]];
            if (board.heightLevels[x, y] == board.boardParameters.heightLevels.Count - 1)
            {
                return heightLevelColorGradient[board.heightLevels[x, y]].Evaluate((board.heightMap[x, y] + min) / (2 - min));
            }
            float max = board.boardParameters.heightLevels[board.heightLevels[x, y] + 1];
            return heightLevelColorGradient[board.heightLevels[x, y]].Evaluate((board.heightMap[x, y] - min) / (max - min));
        }
    }
}