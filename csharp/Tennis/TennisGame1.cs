using System;
using System.Diagnostics;

namespace Tennis
{
    class TennisGame1 : ITennisGame
    {
        private Player _playerOne;
        private Player _playerTwo;
        private readonly ScoreCalculatorService _scoreCalculatorService;

        public TennisGame1(string player1Name, string player2Name)
        {
            _playerOne = new Player(player1Name);
            _playerTwo = new Player(player2Name);
            _scoreCalculatorService = new ScoreCalculatorService();
        }

        public void WonPoint(string playerName)
        {
            if (playerName == _playerOne.Name)
            {
                _playerOne.AwardPoint();
            }

            if (playerName == _playerTwo.Name)
            {
                _playerTwo.AwardPoint(); 
            }

            Console.WriteLine("Invalid Player Name Provided");
        }

        public string GetScore()
        {
            return _scoreCalculatorService.GetScore(_playerOne, _playerTwo);
        }
        
        
    }
}

