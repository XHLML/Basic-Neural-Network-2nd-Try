using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Neural_Network_2nd_Try
{
    class Matrix_Math
    {
        private static Random r = new Random();
        private int rows, cols;
        public double[,] data;

        public Matrix_Math(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            data = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int l = 0; l < cols; l++)
                {
                    data[i, l] = 0;
                }
            }
        }
        public Matrix_Math(double[,] data)
        {
            this.data = data;
            this.rows = data.GetLength(0);
            this.cols = data.GetLength(1);
        }
        public Matrix_Math(Matrix_Math n)
        {
            this.cols = n.cols;
            this.rows = n.rows;
            for (int i = 0; i < rows; i++)
            {
                for (int l = 0; l < cols; l++)
                {
                    this.data[i, l] = n.data[i, l];
                }
            }
        }

        public static void PrintMatrix(Matrix_Math m)
        {
            string s = "[";
            for (int i = 0; i < m.rows; i++)
            {
                for (int l = 0; l < m.cols; l++)
                {
                    if (m.data[i, l] >= 0)
                        s += ' ';
                    s += m.data[i, l];
                    if (l < m.cols - 1)
                        s += " , ";
                }
                if (i < m.rows - 1)
                    s += "\n ";
            }
            Console.WriteLine(s + "]\n");
        }
        public void PrintSelf()
        {
            PrintMatrix(this);
        }

        public static Matrix_Math Randomise(Matrix_Math m, int n)
        {
            for (int i = 0; i < m.rows; i++)
            {
                for (int l = 0; l < m.cols; l++)
                {
                    m.data[i, l] = (double)r.Next(-n * 1000000, n * 1000000 + 1) / 10000000;
                }
            }
            return m;
        }
        public void RandomiseSelf(int n)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int l = 0; l < cols; l++)
                {
                    data[i, l] = (double) r.Next(-n * 1000000, n* 1000000+ 1) / 10000000;
                }
            }
        }

        public void multiplySelf(double n)
        {
            Matrix_Math temp = new Matrix_Math(this.rows, this.cols);
            for (int i = 0; i < rows; i++)
            {
                for (int l = 0; l < cols; l++)
                {
                    data[i, l] *= n;
                }
            }
        }
        public void multiplySelf(Matrix_Math n)
        {
            //dot product
            if (this.cols != n.rows)
            {
                Console.WriteLine("Columns of A must match rows of B");
            } 
            Matrix_Math temp = new Matrix_Math(this.rows, n.cols);
            for (int i = 0; i < rows; i++)
            {
                for (int l = 0; l < cols; l++)
                {
                    double sum = 0;
                    for (int j = 0; j < cols; j++)
                    {
                        sum += this.data[i, j] * n.data[j, l];
                    }
                    temp.data[i, l] = sum;
                }
            }
            this.data = temp.data;
            this.cols = temp.cols;
            this.rows = temp.rows;
        }
        public void multiplySelfHadamard(Matrix_Math n)
        {
                Matrix_Math Result = new Matrix_Math(this.rows, this.cols);
                for (int i = 0; i < this.rows; i++)
                {
                    for (int l = 0; l < this.cols; l++)
                    {
                        this.data[i, l] *= n.data[i, l];
                    }
                }
        }
        public void AddSelf(Matrix_Math m2)
        {
            Matrix_Math Result = new Matrix_Math(this.rows, this.cols);
            for (int i = 0; i < this.rows; i++)
            {
                for (int l = 0; l < this.cols; l++)
                {
                    this.data[i, l] += m2.data[i, l];
                }
            }
        }
        public void SubtractSelf(double n)
        {
            Matrix_Math Result = new Matrix_Math(this.rows, this.cols);
            for (int i = 0; i < this.rows; i++)
            {
                for (int l = 0; l < this.cols; l++)
                {
                    this.data[i, l] -= n;
                }
            }
        }
        public void SubtractSelf(Matrix_Math m2)
        {
            Matrix_Math Result = new Matrix_Math(this.rows, this.cols);
            for (int i = 0; i < this.rows; i++)
            {
                for (int l = 0; l < this.cols; l++)
                {
                    this.data[i, l] -= m2.data[i, l];
                }
            }
        }
        public void AddSelf(double n)
        {
            Matrix_Math Result = new Matrix_Math(this.rows, this.cols);
            for (int i = 0; i < this.rows; i++)
            {
                for (int l = 0; l < this.cols; l++)
                {
                    this.data[i, l] += n;
                }
            }
        }
        public void TransposeSelf()
        {
            Matrix_Math temp = Transpose(this);
            this.cols = temp.cols;
            this.rows = temp.rows;
            this.data = temp.data;
        }

        public delegate double MyFunction(double x,int rows, int cols);
        public static Matrix_Math Map(Matrix_Math m1, MyFunction f)
        {
            Matrix_Math Result = new Matrix_Math(m1.rows, m1.cols);
            for (int i = 0; i < m1.rows; i++)
            {
                for (int l = 0; l < m1.cols; l++)
                {
                    Result.data[i, l] = f(m1.data[i, l],m1.rows,m1.cols);
                }
            }
            return Result;
        }
        public void MapSelf(MyFunction f)
        {
            Matrix_Math temp = new Matrix_Math(this.rows, this.cols);
            for (int i = 0; i < rows; i++)
            {
                for (int l = 0; l < cols; l++)
                {
                    data[i, l] = f(data[i,l],rows,cols);
                }
            }
        }

        public static Matrix_Math multiply(Matrix_Math m1, Matrix_Math m2)
        {
            //dot product
            if (m1.cols != m2.rows)
            {
                Console.WriteLine("Columns of A must match rows of B");
                return null;
            }
            Matrix_Math temp = new Matrix_Math(m1.rows, m2.cols);
            for (int i = 0; i < m1.rows; i++)
            {
                for (int l = 0; l < m2.cols; l++)
                {
                    double sum = 0;
                    for (int j = 0; j < m1.cols; j++)
                    {
                        sum += m1.data[i, j] * m2.data[j, l];
                    }
                    temp.data[i, l] = sum;
                }
            }
            return temp;
        }
        public static Matrix_Math multiply(Matrix_Math m1, double n)
        {
            Matrix_Math Result = new Matrix_Math(m1.rows, m1.cols);
            for (int i = 0; i < m1.rows; i++)
            {
                for (int l = 0; l < m1.cols; l++)
                {
                    Result.data[i, l] = m1.data[i, l] * n;
                }
            }
            return Result;
        }
        public static Matrix_Math Add(Matrix_Math m, double n)
        {
            Matrix_Math Result = new Matrix_Math(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++)
            {
                for (int l = 0; l < m.cols; l++)
                {
                    Result.data[i, l] = m.data[i, l] + n;
                }
            }
            return Result;
        }
        public static Matrix_Math Add(Matrix_Math m1, Matrix_Math m2)
        {
            Matrix_Math Result = new Matrix_Math(m1.rows, m1.cols);
            for (int i = 0; i < m1.rows; i++)
            {
                for (int l = 0; l < m1.cols; l++)
                {
                    Result.data[i, l] = m1.data[i, l] + m2.data[i, l];
                }
            }
            return Result;
        }

        public static Matrix_Math MultiplyHadamard(Matrix_Math m1, Matrix_Math m2)
        {
            Matrix_Math Result = new Matrix_Math(m1.rows, m1.cols);
            for (int i = 0; i < m1.rows; i++)
            {
                for (int l = 0; l < m1.cols; l++)
                {
                    Result.data[i, l] = m1.data[i, l] * m2.data[i, l];
                }
            }
            return Result;
        }

        public static Matrix_Math Subtract(Matrix_Math m, double n)
        {
            Matrix_Math Result = new Matrix_Math(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++)
            {
                for (int l = 0; l < m.cols; l++)
                {
                    Result.data[i, l] = m.data[i, l] - n;
                }
            }
            return Result;
        }
        public static Matrix_Math Subtract(Matrix_Math m1, Matrix_Math m2)
        {
            Matrix_Math Result = new Matrix_Math(m1.rows, m1.cols);
            for (int i = 0; i < m1.rows; i++)
            {
                for (int l = 0; l < m1.cols; l++)
                {
                    Result.data[i, l] = m1.data[i, l] - m2.data[i, l];
                }
            }
            return Result;
        }
        public static Matrix_Math Transpose(Matrix_Math m1)
        {
            Matrix_Math Result = new Matrix_Math(m1.cols, m1.rows);
            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m1.cols; j++)
                {
                    Result.data[j, i] = m1.data[i, j];
                }
            }
            return Result;
        }


    }
}
