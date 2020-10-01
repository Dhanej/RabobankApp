/*------------------------------------------------------------------------------
 * Name       : MandateVM.cs
 * Created By : Dhanej
 * Created On : 29 Sep 2020
 * Updated On : 29 Sep 2020
 * Purpose    : Class file for ViewModel MandateVM
 * Comments   :
-----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Rabobank.Training.ClassLibrary.ViewModels
{
    public class MandateVM
    {
        public string Name { get; set; }
        public decimal Allocation { get; set; }
        public decimal Value { get; set; }
    }
}
