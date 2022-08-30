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
    public class SaveBoardStateTests : MonopolyAccessorTestBase
    {
        [Test]
        public async Task MonopolyApi_Error()
        {
            //Arrange
            var obj = new SaveBoardStateRequest
            {
                GameId = null,
                BoardState = new BoardState()
            };
            
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = (new BoardState{PlayerTurn = 2}).SerializeRequest();
            _mockHandler.When("https://test.monopoly.com/game").WithContent(await obj.SerializeRequest().ReadAsStringAsync()).Respond(_ => response);
            
            //Act
            var result = await _monopolyAccessor.SaveBoardState(obj);

            //Assert
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public async Task MonopolyApi_Success()
        {
            //Arrange
            var obj = new SaveBoardStateRequest
            {
                GameId = null,
                BoardState = new BoardState()
            };
            
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            _mockHandler.When("https://test.monopoly.com/game").WithContent(await obj.SerializeRequest().ReadAsStringAsync()).Respond(_ => response);
            
            //Act
            var result = await _monopolyAccessor.SaveBoardState(obj);

            //Assert
            Assert.AreEqual(true, result);
        }
    }
}