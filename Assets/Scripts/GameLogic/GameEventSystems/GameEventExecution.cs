using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventExecution
{
    public abstract void Execute(GameEventData gameEventData);

    public abstract void Undo(GameEventData gameEventData);
}
