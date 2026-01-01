using System;
using RockPaper.GameEnum;
using UnityEngine;

namespace RockPaper.StaticHelper
{
    public static class GameEvent
    {
        public static Action<HandType> OnComputerHandSelected;
        
        public static Action OnResetGame;
        
        public static Action<RoundResult> OnRoundEnd;
    }
}
