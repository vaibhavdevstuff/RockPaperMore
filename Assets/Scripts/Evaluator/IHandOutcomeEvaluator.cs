using RockPaper.GameEnum;

namespace RockPaper.Evaluator
{
    public interface IHandOutcomeEvaluator
    {
        RoundResult Evaluate(HandType playerHand, HandType opponentHand);
    }

}