using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Monopoly.Accessors.Helpers;
using Monopoly.Accessors.Models;
using MonopolyAccessorTests.TestHelpers;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace MonopolyAccessorTests.MonopolyAccessorTests
{
    public class GetBoardStateTests : MonopolyAccessorTestBase
    {
        [Test]
        public async Task MonopolyApi_Error()
        {
            //Arrange
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent("Unable to start game with 1 player.");
            _mockHandler.When("https://test.monopoly.com/game").Respond(_ => response);

            //Act
            var result = await _monopolyAccessor.GetBoardState(TestConstants.GameId);

            //Assert
            Assert.AreEqual(null, result);
        }
        
        [Test]
        public async Task MonopolyApi_Success()
        {
            //Arrange
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = (new BoardState{PlayerTurn = 2}).SerializeRequest();
            _mockHandler.When("https://test.monopoly.com/game").Respond(_ => response);
            
            //Act
            var result = await _monopolyAccessor.GetBoardState(TestConstants.GameId);

            //Assert
            Assert.AreEqual(2, result.PlayerTurn);
        }
    }
}