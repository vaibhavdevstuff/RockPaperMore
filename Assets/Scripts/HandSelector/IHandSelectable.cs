using RockPaper.GameEnum;

namespace RockPaper.HandSelector
{
    public interface IHandSelectable
    {
        public HandType SelectedHand();
    }
}