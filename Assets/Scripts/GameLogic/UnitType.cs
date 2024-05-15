using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Unit", order = 1)]
    public class UnitType : ScriptableObject
    {
        public int hp;
        public int attackRange;
        public int damage;
        public int maxAmount;
        public int moveRange;
        public UnitPositionState unitPositionState;
    }
}