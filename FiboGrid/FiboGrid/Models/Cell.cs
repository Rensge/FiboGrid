using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiboGrid.Models
{
    public class Cell
    {
        public int Horizontal { get; set; }

        public int Vertical { get; set; }

        public int Value { get; set; }

        public bool Changed { get; set; }

        public bool Cleared { get; set; }
    }
}