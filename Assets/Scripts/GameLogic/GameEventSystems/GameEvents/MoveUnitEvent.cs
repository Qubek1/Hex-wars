using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace GameLogic
{
    public class MoveUnitEvent : GameEvent
    {
        public HexVector from, to;

        public MoveUnitEvent(HexVector from, HexVector to)
        {
            this.from = from;
            this.to = to;
        }

        public override void Execute(GameData gameData)
        {
            gameData.board.MoveUnitToNewPosition(from, to);
        }

        public override void Undo(GameData gameData)
        {
            gameData.board.MoveUnitToNewPosition(to, from);
        }
    }
}