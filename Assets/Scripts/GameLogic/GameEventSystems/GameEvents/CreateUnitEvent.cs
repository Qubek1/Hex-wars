using UnityEngine.Playables;

namespace GameLogic
{
    public class CreateUnitEvent : GameEvent
    {
        public HexVector position;
        public UnitType unitData;
        public int amount;

        public CreateUnitEvent(HexVector position, UnitType unitData, int amount)
        {
            this.position = position;
            this.unitData = unitData;
            this.amount = amount;
        }

        public override void Execute(GameData gameData)
        {
            Unit newUnit = new Unit
                (
                unitData,
                position,
                amount,
                gameData.eventsSequence
                );

            //TODO!: add unit to the players units list
            gameData.board.SetUnitOnPosition(newUnit, position);
        }

        public override void Undo(GameData gameData)
        {
            gameData.board.SetUnitOnPosition(null, position);
            //TODO!: remove unit from the players units list 
        }
    }
}