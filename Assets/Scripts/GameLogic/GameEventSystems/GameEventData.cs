using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public abstract class GameEventData
    {
        public bool playerChoiceEvent = false;
        public GameEventType eventType;
    }
}