using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public enum GameEventType
    {
        None,

        // Units events
        UnitDeath,
        MoveUnit,
        CreateUnit,
        UnitAttack,
        DamageUnit,

        //Units choices
        MoveUnitChoice,

        // GameStates
        ChangeEventState,
        DefaultGameState,
    }

    public partial class GameModel
    {
        private Dictionary<GameEventType, GameEventExecution> eventsExecution = new Dictionary<GameEventType , GameEventExecution>();

        private void PopulateEventsExecution()
        {
            eventsExecution = new Dictionary<GameEventType , GameEventExecution>();

            eventsExecution[GameEventType.UnitDeath] = new UnitDeathExecution(gameData);
            eventsExecution[GameEventType.MoveUnit] = new MoveUnitExecution(gameData);
            eventsExecution[GameEventType.CreateUnit] = new CreateUnitExecution(gameData);
            eventsExecution[GameEventType.UnitAttack] = new UnitAttackExecution(gameData);
            eventsExecution[GameEventType.DamageUnit] = new DamageUnitExecution(gameData);
            eventsExecution[GameEventType.ChangeEventState] = new ChooseNewEventExecution(gameData);
            eventsExecution[GameEventType.DefaultGameState] = new DefaultGameStateExecution(gameData);
            eventsExecution[GameEventType.MoveUnitChoice] = new MoveUnitChoiceExecution(gameData);
        }

        private GameEventExecution GetEventExecutionFromType(GameEventType eventType)
        {
            if (!eventsExecution.ContainsKey(eventType))
            {
                Debug.LogError("There is no Event Execution to: " + eventType);
            }
            return eventsExecution[eventType];
        }

        private GameEventExecution GetEventExecutionFromEventData(GameEventData data)
        {
            return GetEventExecutionFromType(data.eventType);
        }

        private void ExecuteEventData(GameEventData data)
        {
            GetEventExecutionFromType(data.eventType).Execute(data);
        }

        public bool ValidateChosenMove(GameEventData data)
        {
            return ((ChoiceGameEventExecution)GetEventExecutionFromEventData(data)).ValidateChosenEvent(data);
        }
    }
}
