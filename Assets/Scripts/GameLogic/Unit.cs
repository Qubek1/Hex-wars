using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLogic
{
    public class Unit
    {
        public readonly UnitType unitStats;
        public int amount;
        public int lastUnitLostHP;
        private GameEventsSequence gameEventsQueue;
        public HexVector position;
        public int teamId;
        public int playerId;

        public Unit(UnitType unitStats, HexVector position, int amount, GameEventsSequence gameEventsQueue, int lastUnitLostHP = 0)
        {
            this.unitStats = unitStats;
            this.position = position;
            this.amount = amount;
            this.gameEventsQueue = gameEventsQueue;
            this.lastUnitLostHP = lastUnitLostHP;
        }

        public void TakeDamage(int damage)
        {
            int amountToLose = (damage + lastUnitLostHP) / unitStats.hp;
            if (amount <= amountToLose)
            {
                lastUnitLostHP = 0;
                amount = 0;
                Die();
                return;
            }
            amount -= amountToLose;
            lastUnitLostHP = (lastUnitLostHP + damage) % unitStats.hp;
        }

        public void Die()
        {
            gameEventsQueue.AddGameEvent(new UnitDeathData(position));
        }

        public void Merge(Unit anotherUnit)
        {
            amount += anotherUnit.amount;
        }
    }

    public enum UnitPositionState
    {
        Ground,
        Flying,
        Buried,
        Floating
    }
}