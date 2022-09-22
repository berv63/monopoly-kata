using System.Threading.Tasks;
using Monopoly.Tests.TestHelpers;
using NUnit.Framework;

namespace Monopoly.Tests.GameManagerTests
{
    public class StartNewGameTests : GameManagerTestsBase
    {
        [Test]
        public async Task OnePlayer_NoSave()
        {
            //Arrange
            var playerCount = 1;

            //Act
            await GameManager.StartNewGame(playerCount);
            
            //Assert
            _monopolyAccessorMock.VerifySaveNotCalled();
        }
        
        [Test]
        public async Task TwoPlayer_ValidBoard()
        {
            //Arrange
            var playerCount = 2;

            //Act
            await GameManager.StartNewGame(playerCount);
            
            //Assert
            _monopolyAccessorMock.VerifySaveNewGame(playerCount);
        }
        
        [Test]
        public async Task EightPlayer_ValidBoard()
        {
            //Arrange
            var playerCount = 8;

            //Act
            await GameManager.StartNewGame(playerCount);
            
            //Assert
            _monopolyAccessorMock.VerifySaveNewGame(playerCount);
        }
        
        [Test]
        public async Task NinePlayer_NoSave()
        {
            //Arrange
            var playerCount = 9;
            
            //Act
            await GameManager.StartNewGame(playerCount);
            
            //Assert
            _monopolyAccessorMock.VerifySaveNotCalled();
        }
        
    }
}