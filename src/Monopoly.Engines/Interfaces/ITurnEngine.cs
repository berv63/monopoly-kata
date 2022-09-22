using Monopoly.Accessors.Models;
using Monopoly.Shared.Enums;

namespace Monopoly.Engines.Interfaces
{
    public interface ITurnEngine
    {
        void UpdatePlayerLocation(BoardState boardState, DiceRoll diceRoll);
        void UpdatePlayerTurn(BoardState boardState);
    }
}