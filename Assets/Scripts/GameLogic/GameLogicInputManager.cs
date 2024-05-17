using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public partial class GameModel
    {
        public GameEvent GetCurrentEvent()
        {
            return gameData.eventsSequence.GetCurrentGameEvent();
        }

        public void ChooseMove(GameEvent moveData)
        {
            GameEvent gameEventData = GetCurrentEvent();
            if (!gameEventData.playerChoiceEvent)
            {
                Debug.LogWarning("You are trying to choose a player action while the current event is automatic and does not provide any choices");
                return;
            }
            if (!ValidateChoice(moveData))
            {
                Debug.LogWarning("You are trying to make invalid move");
                return;
            }
            gameData.eventsSequence.AddGameEvent(moveData);
            gameData.eventsSequence.EndCurrentGameEvent();
        }

        public bool ValidateChoice(GameEvent moveData)
        {
            return ((ChoiceGameEventExecution)GetEventExecutionFromType(GetCurrentEvent().eventType)).ValidateChosenEvent(moveData);
        }
    }
}