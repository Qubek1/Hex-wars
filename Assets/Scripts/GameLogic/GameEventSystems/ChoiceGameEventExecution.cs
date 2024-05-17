using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public abstract class ChoiceGameEventExecution : GameEventExecution
    {
        public abstract bool ValidateChosenEvent(GameEvent gameEventData);

        public override void Execute(GameEvent gameEventData) { }
    }
}