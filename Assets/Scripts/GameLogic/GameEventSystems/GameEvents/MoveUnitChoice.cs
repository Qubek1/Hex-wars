namespace GameLogic
{
    public class MoveUnitChoiceData : GameEvent
    {
        public MoveUnitChoiceData()
        {
            eventType = GameEventType.MoveUnitChoice;
        }
    }

    public class MoveUnitChoiceExecution : ChoiceGameEventExecution
    {
        private GameData gameData;

        public MoveUnitChoiceExecution(GameData gameData)
        {
            this.gameData = gameData;
        }

        public override void Undo(GameEvent gameEventData)
        {
            MoveUnitChoiceData data = (MoveUnitChoiceData)gameEventData;
        }

        public override bool ValidateChosenEvent(GameEvent gameEventData)
        {
            MoveUnitChoiceData data = (MoveUnitChoiceData)gameEventData;
            if (data.eventType != GameEventType.MoveUnit)
            {
                return false;
            }

            return true;
        }
    }
}