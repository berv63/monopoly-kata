using System.Threading.Tasks;
using Ardalis.Result;
using Monopoly.Accessors.Models;

namespace Monopoly.Managers.Interfaces
{
    public interface ITurnManager
    {
        Task TakePlayerTurn(int gameId);
    }
}