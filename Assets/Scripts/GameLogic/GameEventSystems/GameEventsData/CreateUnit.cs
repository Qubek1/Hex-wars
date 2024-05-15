namespace GameLogic
{
    public class CreateUnitData : GameEventData
    {
        public HexVector position;
        public UnitType unitData;
        public int amount;

        public CreateUnitData(HexVector position, UnitType unitData, int amount)
        {
            eventType = GameEventType.CreateUnit;

            this.position = position;
            this.unitData = unitData;
            this.amount = amount;
        }
    }

    public class CreateUnitExecution : GameEventExecution
    {
        private GameData gameData;

        public CreateUnitExecution(GameData gameData)
        {
            this.gameData = gameData;
        }

        public override void Execute(GameEventData gameEventData)
        {
            CreateUnitData data = (CreateUnitData)gameEventData;

            Unit newUnit = new Unit
                (
                data.unitData,
                data.position,
                data.amount,
                gameData.eventsSequence
                );

            //TODO!: add unit to the players units list
            gameData.board.SetUnitOnPosition(newUnit, data.position);
        }

        public override void Undo(GameEventData gameEventData)
        {
            CreateUnitData data = (CreateUnitData)gameEventData;
            gameData.board.SetUnitOnPosition(null, data.position);
            //TODO!: remove unit from the players units list 
        }
    }
}