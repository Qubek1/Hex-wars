using UnityEngine.Playables;

namespace GameLogic
{
    public class UnitAttackEvent : GameEvent
    {
        public HexVector attackerPosition, defenderPosition;
        public int damageDealt;

        public UnitAttackEvent(HexVector attackerPosition, HexVector defenderPosition)
        {
            this.attackerPosition = attackerPosition;
            this.defenderPosition = defenderPosition;
        }

        public override void Execute(GameData gameData)
        {
            Unit attacker = gameData.board.GetUnit(attackerPosition);
            Unit defender = gameData.board.GetUnit(defenderPosition);
            damageDealt = attacker.unitStats.damage * attacker.amount;
            gameData.eventsSequence.AddGameEvent(new DamageUnitEvent(defender.position, damageDealt));
        }

        public override void Undo(GameData gameData)
        {
        }
    }
}