using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace model1
{
    public partial class Form1 : Form
    {
        private Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private double GenerateCPT(double n)
        {

            double sum = 0, x;

            for (var i = 0; i < n; i++)
            {
                x = rnd.NextDouble();
                sum += x;
            }

            return sum - 5.5;
        }

        private double GenerateTransf(double n)
        {
            double sum = 0, x;
            for (var i = 0; i < n; i++)
            {
                x = rnd.NextDouble();
                sum += Math.Abs(2 * x - 1);
            }
            double tmp = Math.Sqrt(3 / (n * sum)) + 0.5;
            return tmp;
        }

        private void PrintChart(double[] arr , int intervalCount, int numberChart)
        {
            int N = arr.Length;
            double qqqq = arr.Sum() / arr.Length;

            var ordered = arr.OrderBy(selector => selector).ToList();

            var differenceCRT = arr.Max() - arr.Min();
            var intervalSize = differenceCRT / intervalCount;

            var heightArray = new int[intervalCount];


            foreach (var value in ordered)
            {
                int index = (int)((value - ordered[0]) / intervalSize);
                heightArray[Math.Min(index, intervalCount - 1)]++;
            }
            for (var i = 0; i < intervalCount; i++)
            {
                if (numberChart == 1)
                    this.chart1.Series["CPT"].Points.AddXY(Math.Round(ordered[0] + (i) * intervalSize, 3).ToString() + "\n - \n" + Math.Round(ordered[0] + (i + 1) * intervalSize, 3).ToString(), (double) heightArray[i] / N);
                else
                    this.chart2.Series["Transf"].Points.AddXY(Math.Round(ordered[0] + (i) * intervalSize, 3).ToString() + "\n - \n" + Math.Round(ordered[0] + (i + 1) * intervalSize, 3).ToString(), (double) heightArray[i] / N);
            }

            Criterion(heightArray, 1 / (double)intervalCount, intervalCount, N,numberChart);
        }

        private void Criterion(int[] m, double P,int k,int N,int numberChart)
        {
            double sum = 0;
            for(var i = 0; i < k; ++i)
            {
                sum += Math.Pow(m[i] - N * P, 2) / N * P;
            }
            if (numberChart == 1)
                this.textBox1.Text = sum.ToString();
            else
                this.textBox2.Text = sum.ToString();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            int N = 1000;
            int intervalCount = 10;

            var arr = new double[N];

            chart1.Series.Clear();
            chart2.Series.Clear();

            chart1.Series.Add("CPT"); 
            chart2.Series.Add("Transf");
            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = GenerateCPT(12);
            }
            PrintChart(arr, intervalCount, 1);

            for (var i = 0; i < arr.Length; i++)
            {
                arr[i] = GenerateTransf(1000);
            }
            PrintChart(arr, intervalCount, 2);

        }
    }

}
