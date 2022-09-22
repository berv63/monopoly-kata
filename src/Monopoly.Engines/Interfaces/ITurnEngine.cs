using Monopoly.Accessors.Models;
using Monopoly.Shared.Enums;

namespace Monopoly.Engines.Interfaces
{
    public interface ITurnEngine
    {
        int GetNextPlayerTurn(BoardState boardState);
        Player GetCurrentPlayer(BoardState boardState);
        LocationEnum GetPlayerNewLocation(Player currentPlayer, DiceRoll diceRoll);
    }
}