/*------------------------------------------------------------------------------
 * Name       : FundProcessorUnitTest.cs
 * Created By : Dhanej
 * Created On : 28 Sep 2020qqqq
 * Updated On : 29 Sep 2020
 * Purpose    : Unit Test for Methods related to Fund Processor 
 * Comments   : 
-----------------------------------------------------------------------------*/
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;
using System.Linq;
using Rabobank.Training.ClassLibrary.Models;
using Rabobank.Training.ClassLibrary.ViewModels;
using Rabobank.Training.ClassLibrary.Interface;

namespace Rabobank.Training.ClassLibrary.Tests
{
    [TestClass]
    public class FundProcessorUnitTest
    {

        /// <summary>
        /// Unit test to check the read functionality working of FundsOfMandatesData xml file
        ///Implemented Test case : null and assign check
        ///Can also implement : Bad xml Check 
        ///                   : Invalid xml check ..
        /// </summary>

        [TestMethod]
        [DeploymentItem("TestData//FundsOfMandatesData.xml")]
        public void ReadAndValidateXMLFile()
        {
            IFundProcessor fundProcessor = new FundProcessor();
            string filePath = "..\\..\\..\\TestData\\FundsOfMandatesData.xml";

            var FundOfMandateFile = fundProcessor.ReadFundOfMandateFile(filePath);

            Assert.IsNotNull(FundOfMandateFile);
            Assert.IsInstanceOfType(FundOfMandateFile, typeof(List<FundOfMandates>));

            FundOfMandateFile.Should().NotBeNull();
            FundOfMandateFile.Should().BeAssignableTo(typeof(List<FundOfMandates>));

        }


        /// <summary>
        /// Unit test to get the List of static Portfolios
        ///Implemented Test case : null ,euqivalent and assign check
        ///Can also implement :  non euqivalent Check ..
        /// </summary>


        [TestMethod]
        public void ReturnStaticListOfPortfolios()
        {
            IFundProcessor fundProcessor = new FundProcessor();
            var portfolio = new PortfolioVM
            {
                Positions = new List<PositionVM>
                {
                     new PositionVM { Code="NL0000009165", Name="Heineken", Value=12345 },
                     new PositionVM { Code="NL0000287100", Name="Optimix Mix Fund", Value=23456 },
                     new PositionVM { Code="LU0035601805", Name="DP Global Strategy L High", Value=34567 },
                     new PositionVM { Code="NL0000292332", Name="Rabobank Core Aandelen Fonds T2", Value=45678 },
                     new PositionVM { Code="LU0042381250", Name="Morgan Stanley Invest US Gr Fnd", Value=56789 }
                }
            };

            fundProcessor.GetPortfolio().Should().NotBeNull().And.BeAssignableTo(typeof(PortfolioVM)).And.BeEquivalentTo(portfolio);
        }


        /// <summary>
        /// Unit test on calculation the mandates 
        ///Implemented Test case : null , type check
        /// </summary>

        [TestMethod]
        public void ValidateFundOfMandatesReturnsPositionVM()
        {
            IFundProcessor fundsProcessor = new FundProcessor();
            string filePath = "..\\..\\..\\TestData\\FundsOfMandatesData.xml";
            List<FundOfMandates> Result = fundsProcessor.ReadFundOfMandateFile(filePath);
            PortfolioVM portfolioVM = fundsProcessor.GetPortfolio();
            PositionVM positionVM = null;

            positionVM = fundsProcessor.GetCalculatedMandates(portfolioVM.Positions.ElementAt(1), Result.ElementAt(1));

            positionVM.Should().NotBeNull().And.BeOfType<PositionVM>();
        }
       

    }
}
