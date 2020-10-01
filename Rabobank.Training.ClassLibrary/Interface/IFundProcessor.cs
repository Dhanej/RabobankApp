/*------------------------------------------------------------------------------
 * Name       : IFundProcessor.cs
 * Created By : Dhanej
 * Created On : 28 Sep 2020
 * Updated On : 29 Sep 2020
 * Purpose    : Interface related to Fund Processor methods
 * Comments   : Added ReadFundOfMandatesFile   
                Added GetPortfolio 
                Added GetCalculatedMandates
                Added GetUpdatedPortfolio
-----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using Rabobank.Training.ClassLibrary.Models;
using Rabobank.Training.ClassLibrary.ViewModels;

namespace Rabobank.Training.ClassLibrary.Interface
{
    public interface IFundProcessor
    {
        List<FundOfMandates> ReadFundOfMandateFile(string fileName);
        PortfolioVM GetPortfolio();
        PositionVM GetCalculatedMandates(PositionVM position, FundOfMandates fundOfmandates);
        PortfolioVM GetUpdatedPortfolio(string fileName);
    }
}
