using System;
using NUnit.Framework;

namespace Tennis
{
    [TestFixture( 0,  0, "Love-All", "0-0")]
    [TestFixture( 1,  1, "Fifteen-All", "0-0")]
    [TestFixture( 2,  2, "Thirty-All", "0-0")]
    [TestFixture( 3,  3, "Deuce", "0-0")]
    [TestFixture( 4,  4, "Deuce", "0-0")]
    [TestFixture( 1,  0, "Fifteen-Love", "0-0")]
    [TestFixture( 0,  1, "Love-Fifteen", "0-0")]
    [TestFixture( 2,  0, "Thirty-Love", "0-0")]
    [TestFixture( 0,  2, "Love-Thirty", "0-0")]
    [TestFixture( 3,  0, "Forty-Love", "0-0")]
    [TestFixture( 0,  3, "Love-Forty", "0-0")]
    [TestFixture( 4,  0, "Love-All", "1-0")]
    [TestFixture( 0,  4, "Love-All", "0-1")]
    [TestFixture( 2,  1, "Thirty-Fifteen", "0-0")]
    [TestFixture( 1,  2, "Fifteen-Thirty", "0-0")]
    [TestFixture( 3,  1, "Forty-Fifteen", "0-0")]
    [TestFixture( 1,  3, "Fifteen-Forty", "0-0")]
    [TestFixture( 4,  1, "Love-All", "1-0")]
    [TestFixture( 1,  4, "Love-All", "0-1")]
    [TestFixture( 3,  2, "Forty-Thirty", "0-0")]
    [TestFixture( 2,  3, "Thirty-Forty", "0-0")]
    [TestFixture( 4,  2, "Love-All", "1-0")]
    [TestFixture( 2,  4, "Love-All", "0-1")]
    [TestFixture( 4,  3, "Advantage player1", "0-0")]
    [TestFixture( 3,  4, "Advantage player2", "0-0")]
    [TestFixture( 5,  4, "Advantage player1", "0-0")]
    [TestFixture( 4,  5, "Advantage player2", "0-0")]
    [TestFixture(15, 14, "Advantage player1", "0-0")]
    [TestFixture(14, 15, "Advantage player2", "0-0")]
    [TestFixture( 6,  4, "Love-All", "1-0")]
    [TestFixture( 4,  6, "Love-All", "0-1")]
    [TestFixture(16, 14, "Love-All", "1-0")]
    [TestFixture(14, 16, "Love-All", "0-1")]
    public class TennisTests
    {
        private readonly int _player1Score;
        private readonly int _player2Score;
        private readonly string _expectedScore;
        private readonly string _expectedGameWins;

        public TennisTests(int player1Score, int player2Score, string expectedScore, string expectedGameWins)
        {
            _player1Score = player1Score;
            _player2Score = player2Score;
            _expectedScore = expectedScore;
            _expectedGameWins = expectedGameWins;
        }

        [Test]
        public void CheckTennisGame1()
        {
            var game = new TennisGame1("player1", "player2");
            CheckAllScores(game);
        }

        private void CheckAllScores(ITennisGame game)
        {
            var highestScore = Math.Max(this._player1Score, this._player2Score);
            for (var i = 0; i < highestScore; i++)
            {
                if (i < this._player1Score)
                    game.WonPoint("player1");
                if (i < this._player2Score)
                    game.WonPoint("player2");
            }
            Assert.AreEqual(_expectedScore, game.GetScore());
            Assert.AreEqual(_expectedGameWins, game.GetGameWins());
        }

    }

    [TestFixture]
    public class ExampleGameTennisTest
    {
        [Test]
        public void CheckGame1()
        {
            var game = new TennisGame1("player1", "player2");
            RealisticTennisGame(game);
        }

        private void RealisticTennisGame(ITennisGame game)
        {
            string[] points = { "player1", "player1", "player2", "player2", "player1", "player1" };
            string[] expectedScores = { "Fifteen-Love", "Thirty-Love", "Thirty-Fifteen", "Thirty-All", "Forty-Thirty", "Love-All" };
            for (var i = 0; i < 6; i++)
            {
                game.WonPoint(points[i]);
                Assert.AreEqual(expectedScores[i], game.GetScore());
            }
            Assert.AreEqual("1-0", game.GetGameWins());
        }
    }

}

