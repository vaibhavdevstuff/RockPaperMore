using RockPaper.GameEnum;

namespace RockPaper.HandSelector
{
    public class PlayerHandSelector : IHandSelectable
    {
        private HandType _current;

        public void SetInput(HandType hand)
        {
            _current = hand;
        }

        public HandType SelectedHand()
        {
            return _current;
        }
    }
}
