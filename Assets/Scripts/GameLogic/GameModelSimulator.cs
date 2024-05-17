using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameLogic;

public class GameModelSimulator : MonoBehaviour
{
    public GameModel gameModel;
    public GameEventType currentEvent;
    public GameEvent eventData;
    public BoardParameters boardParameters;

    // Start is called before the first frame update
    void Start()
    {
        gameModel = new GameModel(boardParameters);
    }

    // Update is called once per frame
    void Update()
    {
        currentEvent = gameModel.GetCurrentEvent().eventType;
    }
}
