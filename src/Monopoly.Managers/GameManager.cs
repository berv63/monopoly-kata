using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Interfaces;
using Monopoly.Accessors.Models;
using Monopoly.Engines.Interfaces;
using Monopoly.Managers.Interfaces;

namespace Monopoly.Managers
{
    public class GameManager: IGameManager
    {
        private readonly ILogger<GameManager> _logger;
        private readonly IBoardEngine _boardEngine;
        private readonly IMonopolyAccessor _monopolyAccessor;

        public GameManager(ILogger<GameManager> logger, IBoardEngine boardEngine, IMonopolyAccessor monopolyAccessor)
        {
            _logger = logger;
            _boardEngine = boardEngine;
            _monopolyAccessor = monopolyAccessor;
        }

        public async Task<BoardState> StartNewGame(int playerCount)
        {
            if (playerCount >= 2 && playerCount <= 8)
            {
                var boardStateRequest = _boardEngine.CreateNewGame(playerCount);
                await _monopolyAccessor.SaveBoardState(boardStateRequest);
                return boardStateRequest.BoardState;
            }

            return null;
        }
    }
}
