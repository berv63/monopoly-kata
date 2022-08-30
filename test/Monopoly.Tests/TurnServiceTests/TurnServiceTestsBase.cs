using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Monopoly.Accessors;
using Monopoly.Accessors.Interfaces;
using Monopoly.Engines;
using Monopoly.Engines.Interfaces;
using Monopoly.Managers;
using Monopoly.Managers.Interfaces;
using Moq;
using NUnit.Framework;
using RichardSzalay.MockHttp;

namespace Monopoly.Tests.TurnServiceTests
{
    public class TurnServiceTestsBase
    {
        protected Mock<ILogger<BoardEngine>> _engineLoggerMock;
        protected Mock<ILogger<TurnManager>> _serviceLoggerMock;
        
        protected Mock<IMonopolyAccessor> _monopolyRepositoryMock;
        protected ITurnManager TurnManager;
        
        [SetUp]
        protected void Init()
        {
            _engineLoggerMock = new Mock<ILogger<BoardEngine>>();
            _serviceLoggerMock = new Mock<ILogger<TurnManager>>();
            
            _monopolyRepositoryMock = new Mock<IMonopolyAccessor>();
            TurnManager = new TurnManager(_serviceLoggerMock.Object, _monopolyRepositoryMock.Object);
        }
    }
}