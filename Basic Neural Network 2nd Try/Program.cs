using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Neural_Network_2nd_Try
{
    class Program
    {
        static void Main(string[] args)
        {
            Neural_Network n = new Neural_Network(0.5, 3, 3, 3);
            n.query(new double[,] { { 1 }, { 0.5 }, { -1.5 } }).PrintSelf(); ;
        }
    }
}
