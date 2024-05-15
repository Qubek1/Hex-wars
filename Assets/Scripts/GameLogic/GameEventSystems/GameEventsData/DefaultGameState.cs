using System.Collections.Generic;

namespace GameLogic
{
    public class DefaultGameStateData : GameEventData
    {
        public DefaultGameStateData()
        {
            eventType = GameEventType.DefaultGameState;
        }
    }

    public class DefaultGameStateExecution : GameEventExecution
    {
        private GameData gameData;

        public DefaultGameStateExecution(GameData gameData)
        {
            this.gameData = gameData;
        }

        public override void Execute(GameEventData gameEventData)
        {
            List<GameEventType> possibleEvents = new List<GameEventType>
            {

            };
            ChooseNewEventData newPossibleEventsTypes = new ChooseNewEventData(possibleEvents);
            gameData.eventsSequence.AddGameEvent(newPossibleEventsTypes);
        }

        public override void Undo(GameEventData gameEventData)
        {
            DefaultGameStateData data = (DefaultGameStateData)gameEventData;
        }
    }
}