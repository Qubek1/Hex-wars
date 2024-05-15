using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class GameData
    {
        public Board board;
        public GameEventsSequence eventsSequence;

        public GameData(BoardParameters boardParameters) 
        {
            board = new Board(boardParameters);
            eventsSequence = new GameEventsSequence();
        }
    }
}