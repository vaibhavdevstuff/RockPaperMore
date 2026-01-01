using System;
using RockPaper.GameEnum;
using RockPaper.StaticHelper;

namespace RockPaper.HandSelector
{
    public class ComputerHandSelector : IHandSelectable
    {
        private readonly HandType[] _hands = (HandType[])Enum.GetValues(typeof(HandType));

        public HandType SelectedHand()
        {
            var handType = _hands[UnityEngine.Random.Range(0, _hands.Length)];
            
            GameEvent.OnComputerHandSelected?.Invoke(handType);
            
            return handType;
        }
    }
}