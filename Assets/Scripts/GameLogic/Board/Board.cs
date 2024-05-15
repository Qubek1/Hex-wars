using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

namespace GameLogic
{
    public class Board
    {
        public int arraySize;
        public Unit[,] unitsBoard;
        public Tile[,] tilesBoard;
        public float[,] heightMap;
        public int[,] heightLevels;
        private HeightMapGenerator heightMapGenerator;
        public BoardParameters boardParameters;

        public Board(BoardParameters boardParameters) 
        {
            this.boardParameters = boardParameters;
            InitArrays();
            CreateHeightMap();
        }

        private void InitArrays()
        {
            arraySize = boardParameters.boardRadius * 2 - 1;
            unitsBoard = new Unit[arraySize, arraySize];
            tilesBoard = new Tile[arraySize, arraySize];
            heightMap = new float[arraySize, arraySize];
            heightLevels = new int[arraySize, arraySize];
        }

        private void CreateHeightMap()
        {
            boardParameters.heightMapParameters.center = new Vector2(0, 0); //(new HexVector(boardParameters.boardRadius, boardParameters.boardRadius)).ConvertToRealPosition();
            heightMapGenerator = new HeightMapGenerator(boardParameters.heightMapParameters);
            heightMapGenerator.Generate();
            Vector2 pos;
            for (int x = 0; x < arraySize; x++)
            {
                for (int y = 0; y < arraySize; y++)
                {
                    if (ValidateHexPosition(new HexVector(x, y)))
                    {
                        pos = (new HexVector(x - boardParameters.boardRadius, y - boardParameters.boardRadius)).ConvertToRealPosition();

                        heightMap[x, y] = heightMapGenerator.GetHeightInPosition(pos);
                        heightLevels[x, y] = GetHeightLevel(heightMap[x, y]);
                    }
                }
            }
        }

        public bool ValidateHexPosition(HexVector pos)
        {
            return -pos.z > boardParameters.boardRadius - 2 && -pos.z < 3*boardParameters.boardRadius - 2;
        }

        public Unit GetUnit(HexVector boardPosition)
        {
            return unitsBoard[boardPosition.x, boardPosition.y];
        }

        public void SetUnitOnPosition(Unit unit, HexVector boardPosition)
        {
            unitsBoard[boardPosition.x, boardPosition.y] = unit;
            unit.position = boardPosition;
        }

        public void RemoveUnitFromPosition(HexVector boardPosition)
        {
            unitsBoard[boardPosition.x, boardPosition.y] = null;
        }

        public void MoveUnitToNewPosition(HexVector unitPosition, HexVector newPosition)
        {
            SetUnitOnPosition(GetUnit(unitPosition), newPosition);
            RemoveUnitFromPosition(unitPosition);
        }

        private int GetHeightLevel(float height)
        {
            int heightLevel = 0;
            for (; heightLevel < boardParameters.heightLevels.Count - 1; heightLevel++)
            {
                if (boardParameters.heightLevels[heightLevel+1] > height)
                {
                    return heightLevel;
                }
            }
            return heightLevel;
        }

        public int GetHeightLevelInPosition(HexVector position)
        {
            return heightLevels[position.x, position.y];
        }

        public int GetHeightLevelDifference(HexVector position1, HexVector position2)
        {
            return GetHeightLevelInPosition(position1) - GetHeightLevelInPosition(position2);
        }

        public List<HexVector> FindPossibleMoves(HexVector position)
        {
            return FindPossibleMoves(GetUnit(position));
        }

        public List<HexVector> FindPossibleMoves(Unit unit)
        {
            List<HexVector> possibleMoves = new List<HexVector>();

            Queue<PathFindingQueueElement> pathFindingQueue = new Queue<PathFindingQueueElement>(20);
            HashSet<HexVector> visitedTiles = new HashSet<HexVector>();

            if (unit.unitStats.moveRange > 0)
            {
                pathFindingQueue.Enqueue(new PathFindingQueueElement(unit.unitStats.moveRange, unit.position));
            }

            while (pathFindingQueue.Count > 0)
            {
                HexVector currentPosition = pathFindingQueue.Peek().position;
                Debug.Log(currentPosition);
                int leftRange = pathFindingQueue.Peek().leftRange - 1;
                visitedTiles.Add(currentPosition);
                List<HexVector> neighbourPositions = currentPosition.getNeighbours();
                foreach(HexVector neighbour in neighbourPositions)
                {
                    if (!ValidateHexPosition(neighbour) || visitedTiles.Contains(neighbour))
                    {
                        continue;
                    }
                    bool possibleMove = false;
                    switch (unit.unitStats.unitPositionState)
                    {
                        case UnitPositionState.Ground:
                            possibleMove =
                                GetHeightLevelInPosition(neighbour) != 0 &&
                                Mathf.Abs(GetHeightLevelDifference(currentPosition, neighbour)) < 2;
                            break;
                        case UnitPositionState.Flying:
                            possibleMove = true;
                            break;
                        case UnitPositionState.Floating:
                            possibleMove = GetHeightLevelInPosition(neighbour) == 0;
                            break;
                    }
                    if (GetUnit(neighbour) == null)
                    {
                        if (possibleMove)
                        {
                            possibleMoves.Add(neighbour);
                        }
                    }
                    else if (GetUnit(neighbour).teamId != unit.teamId)
                    {
                        possibleMove = false;
                    }

                    if (possibleMove && leftRange > 0)
                    {
                        pathFindingQueue.Enqueue(new PathFindingQueueElement(leftRange, neighbour));
                    }
                }
                pathFindingQueue.Dequeue();
            }
            return possibleMoves;
        }

        private struct PathFindingQueueElement
        {
            public int leftRange;
            public HexVector position;

            public PathFindingQueueElement(int leftRange, HexVector position) 
            {
                this.leftRange = leftRange;
                this.position = position;
            }
        }
    }
}