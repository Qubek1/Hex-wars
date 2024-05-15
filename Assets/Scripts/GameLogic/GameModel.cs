using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public partial class GameModel
    {
        public GameData gameData;
        private BoardParameters boardParameters;

        public GameModel(BoardParameters boardParameters)
        {
            this.boardParameters = boardParameters;

            NewGame();
        }

        public void NewGame()
        {
            gameData = new GameData(boardParameters);
            PopulateEventsExecution();

            List<GameEventType> possibleChoices = new List<GameEventType>
            {
                GameEventType.UnitAttack,
                GameEventType.MoveUnit
            };
            gameData.eventsSequence.AddGameEvent(new ChooseNewEventData(possibleChoices));
        }

        public void Update()
        {
            GameEventData currentEventData = GetCurrentEvent();
            if (!currentEventData.playerChoiceEvent)
            {
                ExecuteEventData(currentEventData);
            }
            else
            {
                GameEventData newPlayerMove = new DamageUnitData(new HexVector(0, 0), 1);
                if (((ChoiceGameEventExecution)GetEventExecutionFromType(currentEventData.eventType)).ValidateChosenEvent(newPlayerMove))
                {
                    ExecuteEventData(currentEventData);
                }
                else
                {
                    Debug.LogWarning("Incorrect Move!");
                }
            }
        }
    }
}
