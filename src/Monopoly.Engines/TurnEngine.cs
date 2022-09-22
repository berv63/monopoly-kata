using System.Linq;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Models;
using Monopoly.Engines.Interfaces;
using Monopoly.Shared.Configuration;
using Monopoly.Shared.Enums;

namespace Monopoly.Engines
{
    public class TurnEngine : ITurnEngine
    {
        private ILogger<TurnEngine> _logger;
        private readonly BaseConfiguration _configuration;

        public TurnEngine(ILogger<TurnEngine> logger, BaseConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Player GetCurrentPlayer(BoardState boardState)
        {
            return boardState.Players.First(x => x.PlayerNumber == boardState.PlayerTurn);
        }

        public LocationEnum GetPlayerNewLocation(Player currentPlayer, DiceRoll diceRoll)
        {
            return (LocationEnum)(((int)currentPlayer.CurrentLocation + diceRoll.DieRoll1 + diceRoll.DieRoll2) % 40);
        }

        public int GetNextPlayerTurn(BoardState boardState)
        {
            return (boardState.PlayerTurn) % (boardState.Players.Count) + 1;
        }
    }
}