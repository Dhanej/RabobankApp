/*------------------------------------------------------------------------------
 * Name       : PortfolioVM.cs
 * Created By : Dhanej
 * Created On : 29 Sep 2020
 * Updated On : 29 Sep 2020
 * Purpose    : Class file for ViewModel PortfolioVM which contain list of Positions
 * Comments   : 
-----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;


namespace Rabobank.Training.ClassLibrary.ViewModels
{
    public class PortfolioVM
    {
        public List<PositionVM> Positions { get; set; }
    }
}
