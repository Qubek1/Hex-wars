using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class GameEventsSequence
    {
        private Stack<GameEvent> gameEventsStack;
        private Stack<GameEvent> gameEventsTimeline;

        public GameEventsSequence(int stackInitialSize = 64)
        {
            gameEventsStack = new Stack<GameEvent>(stackInitialSize);
        }

        public GameEvent GetCurrentGameEvent()
        {
            return gameEventsStack.Peek();
        }

        public void EndCurrentGameEvent()
        {
            gameEventsTimeline.Push(gameEventsStack.Pop());
        }

        public void AddGameEvent(GameEvent gameEvent)
        {
            gameEventsStack.Push(gameEvent);
        }
    }
}