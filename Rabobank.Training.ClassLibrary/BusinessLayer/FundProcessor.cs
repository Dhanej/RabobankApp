/*------------------------------------------------------------------------------
 * Name       : FundProcessor.cs
 * Created By : Dhanej
 * Created On : 28 Sep 2020
 * Updated On : 29 Sep 2020
 * Purpose    : Methods related to Fund Processor 
 * Comments   : Reading FundsOfMandates XML file
                Getting Portfolio and Mandates Calculation
                Updating PositionVM based on Mandates

-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Serialization;
using Rabobank.Training.ClassLibrary.Models;
using Rabobank.Training.ClassLibrary.ViewModels;
using Rabobank.Training.ClassLibrary.Interface;

namespace Rabobank.Training.ClassLibrary
{
    public class FundProcessor : IFundProcessor
    {


        /// <summary>
        ///  This Method Reads XML file 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>

        public List<FundOfMandates> ReadFundOfMandateFile(string fileName)
        {
            try
            {
                List<FundOfMandates> lstFundofMandates = new List<FundOfMandates>();
                StreamReader xmlFileStream = null;
                FundsOfMandatesData objFundsOfMandatesData = null;
                XmlSerializer serializer = new XmlSerializer(typeof(FundsOfMandatesData));

                using (xmlFileStream = new StreamReader(fileName))
                {
                    objFundsOfMandatesData = (FundsOfMandatesData)serializer.Deserialize(xmlFileStream);
                }

                if (objFundsOfMandatesData != null && objFundsOfMandatesData.FundsOfMandates.Length > 0)
                {
                    lstFundofMandates = objFundsOfMandatesData.FundsOfMandates.ToList();
                }
                else
                {
                    throw new Exception("Invalid file.");
                }

                return lstFundofMandates;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// GetPortfolio Method to get Portfolio and return a static PortfolioVM object  
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public PortfolioVM GetPortfolio()
        {
            PortfolioVM portfolio = null;
            try
            {
                portfolio = new PortfolioVM
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

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return portfolio;
        }



        /// <summary>
        ///Method to calculate the Mandates position under the fundofmandates
        /// </summary>
        /// <param name="position"></param>
        /// <param name="fundOfmandates"></param>
        /// <returns></returns>
        public PositionVM GetCalculatedMandates(PositionVM position, FundOfMandates fundOfmandates)
        {
            try
            {
                if (position.Code == fundOfmandates.InstrumentCode && fundOfmandates.Mandates != null && fundOfmandates.Mandates.Length > 0)
                {
                    position.Mandates = new List<MandateVM>();
                    position.Mandates.AddRange
                     (
                                fundOfmandates.Mandates.ToList().Select(x => new MandateVM
                                {
                                    Allocation = x.Allocation / 100,
                                    Name = x.MandateName,
                                    Value = Math.Round((position.Value * x.Allocation) / 100)
                                })
                     );

                    if (fundOfmandates.LiquidityAllocation > 0)
                    {
                        var newMandate = new MandateVM
                        {
                            Allocation = fundOfmandates.LiquidityAllocation / 100,
                            Name = "Liquidity",
                            Value = Math.Round((position.Value * fundOfmandates.LiquidityAllocation) / 100),

                        };

                        position.Mandates.Add(newMandate);
                    }
                }
                return position;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        ///  Method to get the updated portfolio after the Calculated Mandates  
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public PortfolioVM GetUpdatedPortfolio(string fileName)
        {
            PortfolioVM portfolioVM = null;
            List<FundOfMandates> mandates = null;
            IFundProcessor fundProcessor = new FundProcessor();

            portfolioVM = fundProcessor.GetPortfolio();
            mandates = fundProcessor.ReadFundOfMandateFile(fileName);

            portfolioVM.Positions.ForEach(position =>
            {
                mandates.ForEach(fundofmandate =>
                {
                    position = fundProcessor.GetCalculatedMandates(position, fundofmandate);
                });
            });

            return portfolioVM;
        }

    }


}
