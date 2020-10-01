using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rabobank.Training.ClassLibrary.ViewModels;
using Rabobank.Training.ClassLibrary.Interface;


namespace Rabobank.Training.WebApp.Controllers
{

    /// <summary>
    /// Controller to retive the Portfolio position
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IFundProcessor portfolioProcessor = null;
        private readonly IConfiguration config;
        private readonly ILoggerFactory loggerFactory;

        public PortfolioController(IFundProcessor processor, IConfiguration config, ILoggerFactory loggerfactory)
        {
            portfolioProcessor = processor;
            this.config = config;
            this.loggerFactory = loggerfactory;
        }

        [HttpGet]
        public PositionVM[] Get()
        {
            PositionVM[] positions = null;
            try
            {
                var fundsFilePath = config["FundsOfMandatesFile"];
                var portfolioViewModel = portfolioProcessor.GetUpdatedPortfolio(fundsFilePath);

                if (portfolioViewModel == null || portfolioViewModel.Positions == null || portfolioViewModel.Positions.Count == 0)
                {
                    throw new Exception("No Valid Portfolio found.");
                }

                positions = portfolioViewModel.Positions.ToArray();
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger("Portfolio Logger");
                logger.LogError(e, "Error occered while retrieving Portfolio", null);
                throw e;
            }

            return positions;
        }

    }

}
