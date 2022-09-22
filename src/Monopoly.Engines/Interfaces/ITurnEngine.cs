using Monopoly.Accessors.Models;
using Monopoly.Shared.Enums;

namespace Monopoly.Engines.Interfaces
{
    public interface ITurnEngine
    {
        LocationEnum GetPlayerNewLocation(BoardState boardState, DiceRoll diceRoll);
        int GetNextPlayerTurn(BoardState boardState);
    }
}