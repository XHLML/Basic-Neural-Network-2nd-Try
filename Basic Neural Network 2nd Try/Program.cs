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
            Neural_Network n = new Neural_Network(0.1, 2, 4, 1);
            Random r = new Random();
            int i = 0;
           while (n.query(new double[,] { { 0 }, { 1 } }).data[0,0]<0.99|| n.query(new double[,] { { 1 }, { 0 } }).data[0, 0] < 0.99|| n.query(new double[,] { { 0 }, { 0 } }).data[0, 0] > 0.01|| n.query(new double[,] { { 1 }, { 1 } }).data[0, 0] > 0.01)
           // for (int i = 0; i < 1000000; i++)
            {
                i++;
                int l = r.Next(0, 4);
                if (l == 0)
                {
                    n.train(new double[,] { { 0 }, { 0 } }, new double[,] { { 0 } });
                }
                if (l == 1)
                {
                    n.train(new double[,] { { 1 }, { 0 } }, new double[,] { { 1 } });
                }
                if (l == 2)
                {
                    n.train(new double[,] { { 1 }, { 1 } }, new double[,] { { 0 } });
                }
                if (l == 3)
                {
                    n.train(new double[,] { { 0 }, { 1 } }, new double[,] { { 1 } });
                }
            }
            Console.WriteLine("It took "+i+" Tries!");
            while (true)
            {
                n.query(new double[,] { { int.Parse(Console.ReadLine()) }, { int.Parse(Console.ReadLine()) } }).PrintSelf();
            }
        
            
        }
    }
}
