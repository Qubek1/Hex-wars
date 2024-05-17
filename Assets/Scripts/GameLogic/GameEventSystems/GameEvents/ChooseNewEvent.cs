using System.Collections.Generic;

namespace GameLogic
{
    public class ChooseNewEventData : GameEvent
    {
        public List<GameEventType> possibleNewEvents;

        public ChooseNewEventData(List<GameEventType> possibleNewEvents)
        {
            eventType = GameEventType.ChangeEventState;
            this.possibleNewEvents = possibleNewEvents;
        }
    }

    public class ChooseNewEventExecution : ChoiceGameEventExecution
    {
        private GameData gameData;

        public ChooseNewEventExecution(GameData gameData)
        {
            this.gameData = gameData;
        }

        public override void Undo(GameEvent gameEventData)
        {
        }

        public override bool ValidateChosenEvent(GameEvent gameEventData)
        {
            ChooseNewEventData data = (ChooseNewEventData)gameEventData;
            return data.possibleNewEvents.Contains(data.eventType);
        }
    }
}