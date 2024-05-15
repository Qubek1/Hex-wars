using System.Collections.Generic;

namespace GameLogic
{
    public class ChooseNewEventData : GameEventData
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

        public override void Undo(GameEventData gameEventData)
        {
        }

        public override bool ValidateChosenEvent(GameEventData gameEventData)
        {
            ChooseNewEventData data = (ChooseNewEventData)gameEventData;
            return data.possibleNewEvents.Contains(data.eventType);
        }
    }
}