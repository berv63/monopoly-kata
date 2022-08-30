using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Models;
using Monopoly.Engines.Interfaces;

namespace Monopoly.Engines
{
    public class BoardEngine : IBoardEngine
    {
        private ILogger<BoardEngine> _logger;

        public BoardEngine(ILogger<BoardEngine> logger)
        {
            _logger = logger;
        }

        public SaveBoardStateRequest CreateNewGame(int playerCount)
        {
            return new SaveBoardStateRequest
            {
                GameId = null,
                BoardState = new BoardState
                {
                    Players = CreateNewPlayers(playerCount) 
                }
            }; 
        }

        private List<Player> CreateNewPlayers(int playerCount)
        {
            var result = new List<Player>();
            for (int i = 0; i < playerCount; i++)
            {
                result.Add(CreateNewPlayer(i));
            }

            return result;
        }

        private Player CreateNewPlayer(int playerNumber)
        {
            return new Player
            {
                PlayerNumber = playerNumber,
                CashOnHand = 1500,
                
                CurrentLocation = LocationEnum.Go,
                /*IsInJail = false,
                    
                ConsecutiveDoublesRolls = 0,
                ConsecutiveJailRolls = 0,
                    
                OwnedChanceCards = new List<ChanceCard>(),
                OwnedCommunityChestCards = new List<CommunityChestCard>()*/
            };
        }

        public SaveBoardStateRequest BuildSaveBoardStateRequest(int gameId, BoardState state)
        {
            var req = new SaveBoardStateRequest
            {
                GameId = gameId,
                BoardState = state
            };

            return req;
        }
    }
}