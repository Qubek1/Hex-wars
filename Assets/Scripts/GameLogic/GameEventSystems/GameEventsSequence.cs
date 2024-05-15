using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class GameEventsSequence
    {
        private Stack<GameEventData> gameEventsStack;
        private Stack<GameEventData> gameEventsTimeline;

        public GameEventsSequence(int stackInitialSize = 64)
        {
            gameEventsStack = new Stack<GameEventData>(stackInitialSize);
        }

        public GameEventData GetCurrentGameEvent()
        {
            return gameEventsStack.Peek();
        }

        public void EndCurrentGameEvent()
        {
            gameEventsTimeline.Push(gameEventsStack.Pop());
        }

        public void AddGameEvent(GameEventData gameEvent)
        {
            gameEventsStack.Push(gameEvent);
        }
    }
}