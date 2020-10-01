/*------------------------------------------------------------------------------
 * Name       : PositionVM.cs
 * Created By : Dhanej
 * Created On : 29 Sep 2020
 * Updated On : 29 Sep 2020
 * Purpose    : Class file for ViewModel PositionVM with list of MandateVM
 * Comments   :
-----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Rabobank.Training.ClassLibrary.ViewModels
{
    public class PositionVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public List<MandateVM> Mandates { get; set; }
    }
}
