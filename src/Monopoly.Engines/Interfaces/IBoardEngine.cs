using Monopoly.Accessors.Models;

namespace Monopoly.Engines.Interfaces
{
    public interface IBoardEngine
    {
        SaveBoardStateRequest CreateNewGame(int playerCount);
    }
}