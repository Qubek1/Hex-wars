using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public abstract class ChoiceGameEventExecution : GameEventExecution
    {
        public abstract bool ValidateChosenEvent(GameEventData gameEventData);

        public override void Execute(GameEventData gameEventData) { }
    }
}