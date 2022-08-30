using System.Threading.Tasks;
using Monopoly.Accessors.Models;

namespace Monopoly.Accessors.Interfaces
{
    public interface IMonopolyAccessor
    {
        Task<BoardState> GetBoardState(int gameId);
        Task<bool> SaveBoardState(SaveBoardStateRequest stateRequest);
    }
}