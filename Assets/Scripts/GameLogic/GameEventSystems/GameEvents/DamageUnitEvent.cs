using UnityEngine.Playables;

namespace GameLogic
{
    public class DamageUnitEvent : GameEvent
    {
        public HexVector unitPosition;
        public int damage;
        public int unitAmountBefore;

        public DamageUnitEvent(HexVector unitPosition,int damage)
        {
            this.unitPosition = unitPosition;
            this.damage = damage;
        }

        public override void Execute(GameData gameData)
        {
            Unit unit = gameData.board.GetUnit(unitPosition);
            unitAmountBefore = unit.amount;
            unit.TakeDamage(damage);
        }

        public override void Undo(GameData gameData)
        {
            Unit unit = gameData.board.GetUnit(unitPosition);
            unit.amount = unitAmountBefore;
        }
    }
}