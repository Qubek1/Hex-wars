using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class MoveUnitData : GameEventData
    {
        public HexVector from, to;

        public MoveUnitData(HexVector from, HexVector to)
        {
            this.from = from;
            this.to = to;
        }
    }

    public class MoveUnitExecution : GameEventExecution
    {
        private GameData gameData;

        public MoveUnitExecution(GameData gameData)
        {
            this.gameData = gameData;
        }

        public override void Execute(GameEventData gameEventData)
        {
            MoveUnitData data = (MoveUnitData)gameEventData;
            gameData.board.MoveUnitToNewPosition(data.from, data.to);
        }

        public override void Undo(GameEventData gameEventData)
        {
            MoveUnitData data = (MoveUnitData)gameEventData;
            gameData.board.MoveUnitToNewPosition(data.to, data.from);
        }
    }
}