namespace GameLogic
{
    public class DamageUnitData : GameEventData
    {
        public HexVector unitPosition;
        public int damage;
        public int unitAmountBefore;

        public DamageUnitData(HexVector unitPosition,int damage)
        {
            eventType = GameEventType.DamageUnit;

            this.unitPosition = unitPosition;
            this.damage = damage;
        }
    }

    public class DamageUnitExecution : GameEventExecution
    {
        private GameData gameData;

        public DamageUnitExecution(GameData gameData)
        {
            this.gameData = gameData;
        }

        public override void Execute(GameEventData gameEventData)
        {
            DamageUnitData data = (DamageUnitData)gameEventData;
            Unit unit = gameData.board.GetUnit(data.unitPosition);
            data.unitAmountBefore = unit.amount;
            unit.TakeDamage(data.damage);
        }

        public override void Undo(GameEventData gameEventData)
        {
            DamageUnitData data = (DamageUnitData)gameEventData;
            Unit unit = gameData.board.GetUnit(data.unitPosition);
            unit.amount = data.unitAmountBefore;
        }
    }
}