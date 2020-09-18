using NUnit.Framework;

namespace Tennis
{
    public class ScoreCalculatorServiceTests
    {
        private readonly ScoreCalculatorService _scoreCalculatorService = new ScoreCalculatorService();
        private Player _playerOne;
        private Player _playerTwo;

        [SetUp]
        public void SetUp()
        {
            _playerOne = new Player("player1");
            _playerTwo = new Player("player2");
        }

        [Test]
        public void GetScoreShouldReturnLoveAllForANewGameWithNoGamePoints()
        {
            var result = _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            Assert.AreEqual($"{Score.Love}-{Score.TieSuffix}", result);
        }

        [Test]
        public void GetScoreShouldReturnDeuceGivenAtLeastThreePointsPerPlayerAndATie()
        {
            _playerOne.GamePoints = 5;
            _playerTwo.GamePoints = 5;
            var result = _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            Assert.AreEqual($"{Score.Deuce}", result);
        }

        [Test]
        public void
            GetScoreShouldReturnScoreTextSuffixedWithAllGivenAtLessThanThreePointsPerPlayerAndATie()
        {
            _playerOne.GamePoints = 2;
            _playerTwo.GamePoints = 2;
            var result = _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            Assert.AreEqual($"{Score.Thirty}-{Score.TieSuffix}", result);
        }

        [Test]
        public void GetScoreShouldReturnAdvantageSuffixedWithPlayerNameGivenAdvantageToAPlayer()
        {
            _playerOne.GamePoints = 4;
            _playerTwo.GamePoints = 3;
            var result = _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            Assert.AreEqual($"{Score.Advantage} {_playerOne.Name}", result);
        }

        [Test]
        public void GetScoreShouldReturnNormalScoreGivenPlayersHaveNoAdvantageTieOrWin()
        {
            _playerOne.GamePoints = 1;
            _playerTwo.GamePoints = 3;
            var result = _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            Assert.AreEqual($"{Score.Fifteen}-{Score.Forty}", result);
        }
        
        [Test]
        public void GetScoreShouldIncrementPlayersGameWinsByOneGivenAPlayerHasWonAGame()
        {
            _playerOne.GamePoints = 6;
            _playerTwo.GamePoints = 4;
            _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            Assert.AreEqual(1,  _playerOne.GameWins);
        }
        
        [Test]
        public void GetScoreShouldIncrementPlayersGameWinsByThreeGivenAPlayerHasWonThreeGames()
        {
            _playerOne.GamePoints = 6;
            _playerTwo.GamePoints = 4;
            _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            _playerOne.GamePoints = 6;
            _playerTwo.GamePoints = 4;
            _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            _playerOne.GamePoints = 6;
            _playerTwo.GamePoints = 4;
            _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            Assert.AreEqual(3,  _playerOne.GameWins);
        }
        
        [Test]
        public void GetScoreShouldResetAllPlayersGamePointsToZeroGivenAPlayerHasWonAGame()
        {
            _playerOne.GamePoints = 6;
            _playerTwo.GamePoints = 4;
            _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
            Assert.AreEqual(0,  _playerOne.GamePoints);
            Assert.AreEqual(0,  _playerTwo.GamePoints);
        }
    }
}