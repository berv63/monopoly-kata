using System.Collections.Generic;
using System.Linq;
using Monopoly.Accessors.Interfaces;
using Monopoly.Accessors.Models;
using Moq;

namespace Monopoly.Tests.TestHelpers
{
    public static class BoardStateHelpers
    {
        public static BoardState GetNewBoardState()
        {
            return new BoardState
            {
                PlayerTurn = 1,
                Players = new List<Player>
                {
                    new Player
                    {
                        PlayerNumber = 1,
                        CashOnHand = 1500,
                        CurrentLocation = LocationEnum.Go
                    },
                    new Player
                    {
                        PlayerNumber = 2,
                        CashOnHand = 1500,
                        CurrentLocation = LocationEnum.Go
                    }
                },
            };
        }
        
        public static void VerifySaveNotCalled(this Mock<IMonopolyAccessor> accessorMock)
        {
            accessorMock.Verify(x => x.SaveBoardState(It.IsAny<SaveBoardStateRequest>()), Times.Never);
        }
        
        public static void VerifySaveNewGame(this Mock<IMonopolyAccessor> accessorMock, int playerCount)
        {
            accessorMock.Verify(x => x.SaveBoardState(It.Is<SaveBoardStateRequest>(y => 
                y.GameId == null &&
                y.BoardState.PlayerTurn == 1 &&
                y.BoardState.Players.Count == playerCount && 
                y.BoardState.Players.All(z => 
                    z.CurrentLocation == LocationEnum.Go && 
                    z.CashOnHand == 1500
                )
            )), Times.Once);
        }
        
        public static void VerifySavePlayerTurn(this Mock<IMonopolyAccessor> accessorMock, int playerNumber)
        {
            accessorMock.Verify(x => x.SaveBoardState(It.Is<SaveBoardStateRequest>(y => y.BoardState.PlayerTurn == playerNumber)), Times.Once);
        }
        
        public static void VerifySaveNotPlayerLocation(this Mock<IMonopolyAccessor> accessorMock, int playerNumber, LocationEnum previousPlayerLocation)
        {
            accessorMock.Verify(x => x.SaveBoardState(It.Is<SaveBoardStateRequest>(y => y.BoardState.Players.First(z => z.PlayerNumber == playerNumber).CurrentLocation != previousPlayerLocation)), Times.Once);
        }
    }
}