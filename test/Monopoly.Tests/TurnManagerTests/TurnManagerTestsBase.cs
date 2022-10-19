using Microsoft.Extensions.Logging;
using Monopoly.Accessors.Interfaces;
using Monopoly.Engines;
using Monopoly.Engines.Interfaces;
using Monopoly.Managers;
using Monopoly.Managers.Interfaces;
using Monopoly.Shared.Configuration;
using Monopoly.Shared.Enums;
using Moq;
using NUnit.Framework;

namespace Monopoly.Tests.TurnManagerTests
{
    public class TurnManagerTestsBase
    {
        protected BaseConfiguration _config;
        
        protected Mock<IMonopolyAccessor> _monopolyRepositoryMock;

        protected Mock<ILogger<RollEngine>> _rollEngineLoggerMock;
        protected Mock<IRollEngine> _rollEngineMock;
        
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
            _rollEngineMock = new Mock<IRollEngine>();
            
            _turnEngineLoggerMock = new Mock<ILogger<TurnEngine>>();
            _turnEngine = new TurnEngine(_turnEngineLoggerMock.Object, _config);
            
            _managerLoggerMock = new Mock<ILogger<TurnManager>>();
            
            TurnManager = new TurnManager(_managerLoggerMock.Object, _rollEngineMock.Object, _turnEngine, _monopolyRepositoryMock.Object);
        }

        protected void SetupRollDice(int roll1, int roll2)
        {
            _rollEngineMock.Setup(x => x.RollDice()).Returns(new DiceRoll {DieRoll1 = roll1, DieRoll2 = roll2});
        }
    }
}