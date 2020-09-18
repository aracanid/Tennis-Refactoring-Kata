using System.Collections.Generic;

namespace Tennis
{
    public class ScoreCalculatorService
    {
        private readonly Dictionary<int, string> _scoreTexts = new Dictionary<int, string>()
        {
            {0, Score.Love},
            {1, Score.Fifteen},
            {2, Score.Thirty},
            {3, Score.Forty}
        };

        public string GetScore(Player playerOne, Player playerTwo)
        {
            if (playerOne.IsTied(playerTwo))
            {
                return playerOne.GamePoints >= GamePointsThreshold.Deuce ? Score.Deuce : BuildTieText(playerOne);
            }

            if (playerOne.HasAdvantage(playerTwo) || playerTwo.HasAdvantage(playerOne))
            {
                return playerOne.HasAdvantage(playerTwo)
                    ? BuildAdvantageText(playerOne)
                    : BuildAdvantageText(playerTwo);
            }
            
            if (playerOne.IsWinner(playerTwo) || playerTwo.IsWinner(playerOne))
            {
                if (playerOne.IsWinner(playerTwo))
                {
                    playerOne.AwardGameWin();
                }
                
                if (playerTwo.IsWinner(playerOne))
                {
                    playerTwo.AwardGameWin();
                }
                
                ResetAllPlayersGamePoints(playerOne, playerTwo);
                return BuildTieText(playerOne);
            }

            return BuildNormalScoreText(playerOne, playerTwo);
        }

        private string BuildTieText(Player player)
        {
            return $"{_scoreTexts[player.GamePoints]}-{Score.TieSuffix}";
        }

        private string BuildAdvantageText(Player player)
        {
            return $"{Score.Advantage} {player.Name}";
        }

        private string BuildNormalScoreText(Player playerOne, Player playerTwo)
        {
            var playerOneScoreText = _scoreTexts[playerOne.GamePoints];
            var playerTwoScoreText = _scoreTexts[playerTwo.GamePoints];
            return $"{playerOneScoreText}-{playerTwoScoreText}";
        }

        private void ResetAllPlayersGamePoints(Player playerOne, Player playerTwo)
        {
            playerOne.ResetGamePoints();
            playerTwo.ResetGamePoints();
        }
    }
}