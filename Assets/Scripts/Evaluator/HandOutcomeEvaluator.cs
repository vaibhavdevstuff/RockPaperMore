using RockPaper.Data;
using RockPaper.GameEnum;

namespace RockPaper.Evaluator
{
    public class HandOutcomeEvaluator : IHandOutcomeEvaluator
    {
        private readonly HandDatabaseSO _db;

        public HandOutcomeEvaluator(HandDatabaseSO db)
        {
            _db = db;
        }

        public RoundResult Evaluate(HandType player, HandType opponent)
        {
            if (player == opponent)
                return RoundResult.Draw;

            var playerDef = _db.Get(player);

            return playerDef.HandBeats.Contains(opponent)
                ? RoundResult.Win
                : RoundResult.Lose;
        }
    }
}