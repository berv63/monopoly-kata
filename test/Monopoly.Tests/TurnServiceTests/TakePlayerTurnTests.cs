using System.Threading.Tasks;
using Monopoly.Accessors.Models;
using Monopoly.Tests.TestHelpers;
using Moq;
using NUnit.Framework;

namespace Monopoly.Tests.TurnServiceTests
{
    public class TakePlayerTurnTests : TurnServiceTestsBase
    {
        [Test]
        public async Task TakeTurn_GetsBoardState()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);

            //Act
            await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            _monopolyRepositoryMock.Verify(x => x.GetBoardState(TestConstants.GameId), Times.Once);
        }
        
        //player advances
        [Test]
        public async Task TakeTurn_NewGame_PlayerAdvances_Normal()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.Players[0].CurrentLocation = LocationEnum.Go;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);

            //Act
            await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            _monopolyRepositoryMock.VerifyNotPlayerLocation(1, LocationEnum.Go);
        }
        
        [Test]
        public async Task TakeTurn_PlayerAdvances_PastGo_Wrap()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.Players[0].CurrentLocation = LocationEnum.Boardwalk;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);

            //Act
            await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            _monopolyRepositoryMock.VerifyNotPlayerLocation(1, LocationEnum.Boardwalk);
        }
        
        //player turn change
        [Test]
        public async Task TakeTurn_NewGame_NextPlayerTurn()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.PlayerTurn = 1;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);
            
            //Act
            await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            _monopolyRepositoryMock.VerifyPlayerTurn(2);
        }
        
        [Test]
        public async Task TakeTurn_LastPlayer_FirstPlayerTurn()
        {
            //Arrange
            var boardState = BoardStateHelpers.GetNewBoardState();
            boardState.PlayerTurn = 2;
            _monopolyRepositoryMock.Setup(x => x.GetBoardState(TestConstants.GameId)).ReturnsAsync(boardState);
                
            //Act
            await TurnManager.TakePlayerTurn(TestConstants.GameId);
            
            //Assert
            _monopolyRepositoryMock.VerifyPlayerTurn(1);
        }
    }
}