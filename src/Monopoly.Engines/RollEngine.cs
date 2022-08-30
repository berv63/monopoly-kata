using System;
using Microsoft.Extensions.Logging;
using Monopoly.Engines.Interfaces;
using Monopoly.Shared.Configuration;
using Monopoly.Shared.Enums;

namespace Monopoly.Engines
{
    public class RollEngine : IRollEngine
    {
        private ILogger<RollEngine> _logger;
        private readonly BaseConfiguration _configuration;

        public RollEngine(ILogger<RollEngine> logger, BaseConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public DiceRoll RollDice()
        {
            var rand = new Random();
            return new DiceRoll
            {
                DieRoll1 = rand.Next(1, 7),
                DieRoll2 = rand.Next(1, 7)
            };
        }
    }
}