using System.Linq;
using System.Threading.Tasks;
using Monopoly.Accessors.Models;
using Monopoly.Tests.TestHelpers;
using Moq;
using NUnit.Framework;

namespace Monopoly.Tests.TurnManagerTests
{
    public class TakePlayerTurnTests : TurnManagerTestsBase
    {
        //player advances
        [Test]
        public async Task TakeTurn_NewGame_PlayerAdvances_Normal()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.Players[0].CurrentLocation = LocationEnum.Go;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);
            SetupRollDice(2, 3);
            
            //Act
            var result = await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            _monopolyRepositoryMock.VerifySaveNotPlayerLocation(1, LocationEnum.Go);
            Assert.AreEqual(LocationEnum.ReadingRailroad, result.Players.First(x => x.PlayerNumber == 1).CurrentLocation);
        }
        
        [Test]
        public async Task TakeTurn_PlayerAdvances_PastGo_Wrap()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.Players[0].CurrentLocation = LocationEnum.Boardwalk;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);
            SetupRollDice(2, 3);
            
            //Act
            var result = await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            _monopolyRepositoryMock.VerifySaveNotPlayerLocation(1, LocationEnum.Boardwalk);
            Assert.AreNotEqual(LocationEnum.Boardwalk, result.Players.First(x => x.PlayerNumber == 1).CurrentLocation);
        }
        
        //player turn change
        [Test]
        public async Task TakeTurn_NewGame_NextPlayerTurn()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.PlayerTurn = 1;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);
            SetupRollDice(2, 3);
            
            //Act
            var result = await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            _monopolyRepositoryMock.VerifySavePlayerTurn(2);
            Assert.AreEqual(2, result.PlayerTurn);
        }
        
        [Test]
        public async Task TakeTurn_LastPlayer_FirstPlayerTurn()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.PlayerTurn = 2;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);
            SetupRollDice(2, 3);
                
            //Act
            var result = await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            _monopolyRepositoryMock.VerifySavePlayerTurn(1);
            Assert.AreEqual(1, result.PlayerTurn);
        }

        [Test]
        public async Task TakeTurn_FirstPlayer_RollsDoubles_GoesAgain()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.PlayerTurn = 1;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);
            SetupRollDice(1, 1);
                
            //Act
            var result = await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            //_monopolyRepositoryMock.VerifySavePlayerTurn(1);
            Assert.AreEqual(1, result.PlayerTurn);
        }

        [Test]
        public async Task TakeTurn_FirstPlayerInJail_DoesntRollDoubles_StaysInJail()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.PlayerTurn = 1;
            boardState.Players[0].IsInJail = true;
            boardState.Players[0].CurrentLocation = LocationEnum.Jail;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);
            SetupRollDice(1, 2);
                
            //Act
            var result = await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            //_monopolyRepositoryMock.VerifySavePlayerTurn(1);
            Assert.AreEqual(true, result.Players[0].IsInJail);
            Assert.AreEqual(LocationEnum.Jail, boardState.Players[0].CurrentLocation);
        }
    }
}