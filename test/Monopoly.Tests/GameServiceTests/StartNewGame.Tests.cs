using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Monopoly.Tests.TestHelpers;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace Monopoly.Tests.GameServiceTests
{
    public class StartNewGameTests : GameServiceTestsBase
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
            _monopolyAccessorMock.VerifyNewGame(playerCount);
        }
        
        [Test]
        public async Task EightPlayer_ValidBoard()
        {
            //Arrange
            var playerCount = 8;

            //Act
            await GameManager.StartNewGame(playerCount);
            
            //Assert
            _monopolyAccessorMock.VerifyNewGame(playerCount);
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
        
        
        //Client assert
        [Test]
        public async Task Retain_ClientAssert()
        {
            //Arrange
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent("Unable to start game with 1 player.");
            var request = _mockHandler.When("https://test.monopoly.com/game").Respond(_ => response);

            //Act
            await GameManager.StartNewGame(1);
            
            //Assert
        }
    }
}