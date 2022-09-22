using System.Threading.Tasks;
using Ardalis.Result;
using Monopoly.Accessors.Models;

namespace Monopoly.Managers.Interfaces
{
    public interface IGameManager
    {
        Task<BoardState> StartNewGame(int playerCount);
    }
}