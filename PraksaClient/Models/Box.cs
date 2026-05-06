using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PraksaClient.Models
{
    public class Box : IComparable
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public int CompareTo(object obj)
        {
            int value1 = ((Box)obj).Height - Height;
            int value2 = ((Box)obj).Width - Width;
            if (value1 == 0 && value2 == 0) return 0;
            if (value1 < 0 || value2 < 0) return -1;
            if (value1 > 0 && value2 > 0) return 1;
            else return 0;
        }
    }
}
