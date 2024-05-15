using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLogic
{
    public class UnitDeathData : GameEventData
    {
        public HexVector unitPosition;
        public UnitType unitData;
        public int amount;

        public UnitDeathData(HexVector unitPosition)
        {
            eventType = GameEventType.UnitDeath;
            this.unitPosition = unitPosition;
        }
    }

    public class UnitDeathExecution : GameEventExecution
    {
        private GameData gameData;

        public UnitDeathExecution(GameData gameData)
        {
            this.gameData = gameData;
        }

        public override void Execute(GameEventData gameEventData)
        {
            UnitDeathData data = (UnitDeathData)gameEventData;
            gameData.board.RemoveUnitFromPosition(data.unitPosition);
            //TODO!: add removing dead unit from list of units
        }

        public override void Undo(GameEventData gameEventData)
        {
            UnitDeathData data = (UnitDeathData)gameEventData;
            Unit unit = new Unit(data.unitData, data.unitPosition, data.amount, gameData.eventsSequence);
            gameData.board.SetUnitOnPosition(unit, data.unitPosition);
            //TODO!: add unit to players list of units
        }
    }
}