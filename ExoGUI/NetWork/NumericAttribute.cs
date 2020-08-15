using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoGUI.NetWork
{
    internal class NumericAttribute : Attribute
    {
        public int Number { get; set; }
        public NumericAttribute(int number)
        {
            Number = number;
        }
    }
}
