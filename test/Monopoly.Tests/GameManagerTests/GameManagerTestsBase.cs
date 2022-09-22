using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Interfaces;
using Monopoly.Engines;
using Monopoly.Engines.Interfaces;
using Monopoly.Managers;
using Monopoly.Managers.Interfaces;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace Monopoly.Tests.GameManagerTests
{
    public class GameManagerTestsBase
    {
        protected MockHttpMessageHandler _mockHandler;
        protected Mock<IHttpClientFactory> _mockHttpClientFactory;
        
        protected Mock<ILogger<BoardEngine>> _engineLoggerMock;
        protected Mock<ILogger<GameManager>> _serviceLoggerMock;
        
        protected Mock<IMonopolyAccessor> _monopolyAccessorMock;
        protected IBoardEngine _boardEngine;
        protected IGameManager GameManager;
        
        [SetUp]
        protected void Init()
        {
            _mockHandler = new MockHttpMessageHandler();
            var mockClient = new HttpClient(_mockHandler);
            mockClient.BaseAddress = new Uri("https://test.monopoly.com");
            
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockHttpClientFactory.Setup(p => p.CreateClient(It.IsAny<string>())).Returns(mockClient);
            
            _engineLoggerMock = new Mock<ILogger<BoardEngine>>();
            _serviceLoggerMock = new Mock<ILogger<GameManager>>();

            _monopolyAccessorMock = new Mock<IMonopolyAccessor>();
            
            _boardEngine = new BoardEngine(_engineLoggerMock.Object);
            GameManager = new GameManager(_serviceLoggerMock.Object, _boardEngine, _monopolyAccessorMock.Object);
        }
    }
}