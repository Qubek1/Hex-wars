namespace GameLogic
{
    public class UnitAttackData : GameEventData
    {
        public HexVector attackerPosition, defenderPosition;

        public UnitAttackData(HexVector attackerPosition, HexVector defenderPosition)
        {
            eventType = GameEventType.UnitAttack;

            this.attackerPosition = attackerPosition;
            this.defenderPosition = defenderPosition;
        }
    }

    public class UnitAttackExecution : GameEventExecution
    {
        private GameData gameData;

        public UnitAttackExecution(GameData gameData)
        {
            this.gameData = gameData;
        }

        public override void Execute(GameEventData gameEventData)
        {
            UnitAttackData data = (UnitAttackData)gameEventData;
            Unit attacker = gameData.board.GetUnit(data.attackerPosition);
            Unit defender = gameData.board.GetUnit(data.defenderPosition);
            int damage = attacker.unitStats.damage * attacker.amount;
            gameData.eventsSequence.AddGameEvent(new DamageUnitData(defender.position, damage));
        }

        public override void Undo(GameEventData gameEventData)
        {
        }
    }
}