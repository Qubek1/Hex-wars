using GameLogic;
using System.Collections.Generic;
using UnityEngine;
using GameVisuals;

namespace PlayerInterface
{
    public class UnitsManager : MonoBehaviour
    {
        public float unitPositionRandomnes;
        public List<UnitVisualsData> unitVisualsList;
        
        List<GameObject>[,] unitGroups;
        HexTileObject[,] hexTileObjects;
        Dictionary<UnitType, GameObject> unitVisualsDictionary;

        public void Initialize(HexTileObject[,] hexTileObjects)
        {
            unitVisualsDictionary = new Dictionary<UnitType, GameObject>();
            this.hexTileObjects = hexTileObjects;
            unitGroups = new List<GameObject>[hexTileObjects.GetLength(0), hexTileObjects.GetLength(1)];
            for (int x = 0; x < hexTileObjects.GetLength(0); x++)
            {
                for (int y = 0; y < hexTileObjects.GetLength(1); y++)
                {
                    unitGroups[x, y] = new List<GameObject>();
                }
            }
            foreach (UnitVisualsData unitVisual in unitVisualsList)
            {
                unitVisualsDictionary[unitVisual.unitType] = unitVisual.prefab;
            }
        }

        public void CreateUnit(HexVector hexPosition, UnitType unitType, int amount = 1)
        {
            GameObject unitObject = Instantiate(unitVisualsDictionary[unitType]);
            unitGroups[hexPosition.x, hexPosition.y].Add(unitObject);
            unitObject.transform.SetParent(GetRandomUnitPositionOnTile(hexPosition));
            unitObject.transform.localPosition = GetRandomVector();
        }

        public void MoveUnits(HexVector fromTile, HexVector toTile, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                MoveUnitHexPosition(fromTile, toTile, unitGroups[fromTile.x, fromTile.y][i]);
            }
        }

        private void MoveUnitHexPosition(HexVector fromTile, HexVector toTile, GameObject unitObject)
        {
            unitGroups[fromTile.x, fromTile.y].Remove(unitObject);
            unitGroups[toTile.x, toTile.y].Add(unitObject);
            unitObject.transform.SetParent(GetRandomUnitPositionOnTile(toTile));
            unitObject.transform.localPosition = GetRandomVector();
        }

        private Transform GetRandomUnitPositionOnTile(HexVector hexPosition)
        {
            List<Transform> possiblePositions = new List<Transform>();
            foreach (Transform position in hexTileObjects[hexPosition.x, hexPosition.y].UnitPositions)
            {
                if (position.childCount == 0)
                {
                    possiblePositions.Add(position);
                }
            }
            return possiblePositions[Random.Range(0, possiblePositions.Count)];
        }

        private Vector3 GetRandomVector()
        {
            float angle = Random.Range(0, Mathf.PI * 2);
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
            pos *= unitPositionRandomnes;
            return pos;
        }
    }
}