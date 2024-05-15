using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    [CreateAssetMenu(fileName = "BoardParameters", menuName = "ScriptableObjects/Board/BoardParameters", order = 1)]
    public class BoardParameters : ScriptableObject
    {
        public int boardRadius;
        public HeightMapParameters heightMapParameters;
        public List<float> heightLevels;
    }
}