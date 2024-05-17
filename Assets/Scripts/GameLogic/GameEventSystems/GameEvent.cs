using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public abstract class GameEvent
    {
        public bool playerChoiceEvent = false;
        public abstract void Execute(GameData gameData);
        public abstract void Undo(GameData gameData);
    }
}