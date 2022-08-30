using System.Threading.Tasks;
using Ardalis.Result;
using Monopoly.Accessors.Models;

namespace Monopoly.Managers.Interfaces
{
    public interface IGameManager
    {
        Task StartNewGame(int playerCount);
    }
}