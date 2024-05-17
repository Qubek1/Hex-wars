using System.Collections.Generic;

namespace GameLogic
{
    public class DefaultGameStateData : GameEvent
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

        public override void Execute(GameEvent gameEventData)
        {
            List<GameEventType> possibleEvents = new List<GameEventType>
            {

            };
            ChooseNewEventData newPossibleEventsTypes = new ChooseNewEventData(possibleEvents);
            gameData.eventsSequence.AddGameEvent(newPossibleEventsTypes);
        }

        public override void Undo(GameEvent gameEventData)
        {
            DefaultGameStateData data = (DefaultGameStateData)gameEventData;
        }
    }
}