using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;

namespace GameLogic
{
    public class UnitDeathEvent : GameEvent
    {
        public HexVector unitPosition;
        public UnitType unitData;
        public int amount;

        public UnitDeathEvent(HexVector unitPosition)
        {
            this.unitPosition = unitPosition;
        }

        public override void Execute(GameData gameData)
        {
            gameData.board.RemoveUnitFromPosition(unitPosition);
            //TODO!: add removing dead unit from list of units
        }

        public override void Undo(GameData gameData)
        {
            Unit unit = new Unit(unitData, unitPosition, amount, gameData.eventsSequence);
            gameData.board.SetUnitOnPosition(unit, unitPosition);
            //TODO!: add unit to players list of units
        }
    }
}