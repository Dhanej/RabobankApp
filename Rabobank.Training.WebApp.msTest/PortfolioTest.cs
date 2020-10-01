using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Rabobank.Training.ClassLibrary.ViewModels;
using Rabobank.Training.ClassLibrary.Interface;
using Rabobank.Training.WebApp.Controllers;

namespace Rabobank.Training.WebApp.msTest
{
    [TestClass]
    public class PortfolioTest
    {
        private IFundProcessor portfolioProcessor = null;
        private IConfiguration config;
        private ILoggerFactory loggerFactory;

        [TestMethod]
        public void ShouldReturnValidCountPositionFromPortfolio()
        {
            portfolioProcessor = Substitute.For<IFundProcessor>();
            config = Substitute.For<IConfiguration>();
            loggerFactory = Substitute.For<ILoggerFactory>();
            config["FundsOfMandatesFile"] = "..\\..\\..\\TestData\\FundsOfMandatesData.xml";
            loggerFactory.ClearReceivedCalls();

            var testPortfolio = new PortfolioVM
            {
                Positions = new List<PositionVM> {

                    
                     new PositionVM { Code="NL0000009165", Name="Heineken", Value=12345 },
                     new PositionVM { Code="NL0000287100", Name="Optimix Mix Fund", Value=23456 },
                     new PositionVM { Code="LU0035601805", Name="DP Global Strategy L High", Value=34567 },
                     new PositionVM { Code="NL0000292332", Name="Rabobank Core Aandelen Fonds T2", Value=45678 },
                     new PositionVM { Code="LU0042381250", Name="Morgan Stanley Invest US Gr Fnd", Value=56789 },
                 
                }
            };

            portfolioProcessor.GetUpdatedPortfolio(Arg.Any<string>()).Returns(testPortfolio);

            PortfolioController controller = new PortfolioController(portfolioProcessor, config, loggerFactory);
            var result = controller.Get();
            result.Should().HaveCount(c => c == 5, "GetPortfolio returns the count which matches with input count");

        }
     }
}
