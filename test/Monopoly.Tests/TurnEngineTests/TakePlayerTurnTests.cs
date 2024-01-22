using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Models;
using Monopoly.Engines;
using Monopoly.Engines.Interfaces;
using Monopoly.Shared.Configuration;
using Monopoly.Shared.Enums;
using Monopoly.Tests.TestHelpers;
using Moq;
using NUnit.Framework;

namespace Monopoly.Tests.TurnEngineTests
{
    public class TakePlayerTurnTests
    {
        protected BaseConfiguration _config;

        protected Mock<ILogger<TurnEngine>> _turnEngineLoggerMock;
        protected ITurnEngine _turnEngine;

        [SetUp]
        protected void Init()
        {
            _config = new BaseConfiguration();
            
            _turnEngineLoggerMock = new Mock<ILogger<TurnEngine>>();
            _turnEngine = new TurnEngine(_turnEngineLoggerMock.Object, _config);            
        }
        
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        [TestCase(4, 4)]
        [TestCase(5, 5)]
        [TestCase(6, 6)]
        public void CurrentPlayer1_RollsDoubles_ReturnsPlayer1(int dieRoll1, int dieRoll2)
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.PlayerTurn = 1;
            var diceRoll = new DiceRoll
            {
                DieRoll1 = dieRoll1,
                DieRoll2 = dieRoll2
            };
            
            //Act
            var result = _turnEngine.GetNextPlayerTurn(boardState, diceRoll);
            
            //Assert
            Assert.AreEqual(1, result);
        }
        
        [Test]
        public void CurrentPlayer1_DoesntRollDoubles_ReturnsPlayer2()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.PlayerTurn = 1;
            var diceRoll = new DiceRoll
            {
                DieRoll1 = 2,
                DieRoll2 = 3
            };
            
            //Act
            var result = _turnEngine.GetNextPlayerTurn(boardState, diceRoll);
            
            //Assert
            Assert.AreEqual(2, result);
        }
    }
}