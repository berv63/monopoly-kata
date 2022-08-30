using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors;
using Monopoly.Accessors.Interfaces;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace MonopolyAccessorTests.MonopolyAccessorTests
{
    public class MonopolyAccessorTestBase : AccessorTestBase
    {
        protected Mock<ILogger<MonopolyAccessor>> _accessorLoggerMock;
        protected IMonopolyAccessor _monopolyAccessor;

        [SetUp]
        protected void Init()
        {
            _mockHandler = new MockHttpMessageHandler();
            var mockClient = new HttpClient(_mockHandler);
            mockClient.BaseAddress = new Uri("https://test.monopoly.com");

            _accessorLoggerMock = new Mock<ILogger<MonopolyAccessor>>();
            
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockHttpClientFactory.Setup(p => p.CreateClient(It.IsAny<string>())).Returns(mockClient);
            
            _monopolyAccessor = new MonopolyAccessor(_mockHttpClientFactory.Object, _accessorLoggerMock.Object);
        }
    }
}