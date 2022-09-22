using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Interfaces;
using Monopoly.Engines;
using Monopoly.Engines.Interfaces;
using Monopoly.Managers;
using Monopoly.Managers.Interfaces;
using Monopoly.Shared.Configuration;
using Moq;
using NUnit.Framework;

namespace Monopoly.Tests.TurnManagerTests
{
    public class TurnManagerTestsBase
    {
        protected BaseConfiguration _config;
        
        protected Mock<IMonopolyAccessor> _monopolyRepositoryMock;

        protected Mock<ILogger<RollEngine>> _rollEngineLoggerMock;
        protected IRollEngine _rollEngine;
        
        protected Mock<ILogger<TurnEngine>> _turnEngineLoggerMock;
        protected ITurnEngine _turnEngine;
        
        protected Mock<ILogger<TurnManager>> _managerLoggerMock;
        protected ITurnManager TurnManager;
        
        [SetUp]
        protected void Init()
        {
            _config = new BaseConfiguration();
            _monopolyRepositoryMock = new Mock<IMonopolyAccessor>();
            
            _rollEngineLoggerMock = new Mock<ILogger<RollEngine>>();
            _rollEngine = new RollEngine(_rollEngineLoggerMock.Object, _config);
            
            _turnEngineLoggerMock = new Mock<ILogger<TurnEngine>>();
            _turnEngine = new TurnEngine(_turnEngineLoggerMock.Object, _config);
            
            _managerLoggerMock = new Mock<ILogger<TurnManager>>();
            
            TurnManager = new TurnManager(_managerLoggerMock.Object, _rollEngine, _turnEngine, _monopolyRepositoryMock.Object);
        }
    }
}