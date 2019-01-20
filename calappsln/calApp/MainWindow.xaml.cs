using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;


namespace calApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            values = new List<int>();
            this.DataContext = this;
        }

        private List<int> values;

        private void btn9_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "9"; 
        }

        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "8";
        }

        private void btn7_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "7";
        }

        private void btnDiv_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += " / ";
        }

        private void btnMul_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += " * ";
        }

        private void btn6_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "6";
        }

        private void btn5_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "5";
        }

        private void btn4_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "4";
        }

        private void btnMinus_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += " - ";
        }

        private void btnAdd_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += " + ";
        }

        private void btn3_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "3";
        }
        private void btn2_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "2";
        }

        private void btn1_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "1";
        }

        private void btn0_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += "0";
        }

        private void btnClr_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text = "";
        }

        private void btnBksp_click(object sender, RoutedEventArgs e)
        {
            string str = txtbox.Text;
            if (str == "") return;

            ArrayList numstr = new ArrayList(str.Split(new char[] { ' ' }));

            while (numstr.Count > 0 && numstr[numstr.Count - 1] == "")
                numstr.RemoveAt(numstr.Count-1);

            String str2 = "";
            if (numstr.Count > 0)
            {
                numstr.RemoveAt(numstr.Count - 1);
                str2 = String.Join(" ", numstr.ToArray());
                str2 += " ";
            }

            if (str2 != " ")
                txtbox.Text = str2;
            else
                txtbox.Text = "";
        }

        private void btnBrktOpen_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += " ( ";
        }
        private void btnBrktClose_click(object sender, RoutedEventArgs e)
        {
            txtbox.Text += " ) ";
        }

        private void btnEqual_click(object sender, RoutedEventArgs e)
        {
            string str = txtbox.Text;
            if (str == "") return;

            ArrayList numstr = new ArrayList(str.Split(new char[] {' '}));

            infixToposfixConverter(ref numstr);
            double value = posfixProcessor(numstr);

            txtbox.Text += " = ";
            txtbox.Text += value.ToString();
        }

        private double posfixProcessor(ArrayList inputstr)
        {
            double value = 0;
            Stack<String> stk = new Stack<string>();

            foreach (string str in inputstr)
            {
                if (str == "")
                    continue;

                if (str.All(char.IsDigit))
                {
                    stk.Push(str);
                }
                else if(stk.Count() >= 2)
                {
                    double num2 = Convert.ToDouble(stk.Pop());
                    double num1 = Convert.ToDouble(stk.Pop());

                    value = GetValue(num1,num2, str);
                    stk.Push(value.ToString());
                }
            }
            return Convert.ToDouble(stk.Pop());
        }

        private double GetValue(double num1, double num2, string str)
        { 
            switch(str)
            {
                case "+": return num1 + num2;
                case "-": return num1 - num2;
                case "*": return num1 * num2;
                case "/": return num1 / num2;
            }

            return 0;
        }

        private void infixToposfixConverter(ref ArrayList input)
        {
            ArrayList posfix = new ArrayList();
            Stack<string> tmpStk = new Stack<string>();

            foreach(string str in input)
            {
                if (str == "")
                    continue;

                if (str.All(char.IsDigit))
                {
                    posfix.Add(str);
                }
                else 
                {
                    if (tmpStk.Count == 0)
                    {
                        tmpStk.Push(str);
                    }
                    else
                    {
                        if (str == "(")
                        {
                            tmpStk.Push(str);
                            continue;
                        }
                        else if (str == ")")
                        {
                            while ( (tmpStk.Count != 0) && (tmpStk.Peek() != "(") )
                            {
                                posfix.Add(tmpStk.Pop());
                            }
                            tmpStk.Pop();
                            continue;
                        }

                        while ( tmpStk.Count != 0 && priority(str) < priority(tmpStk.Peek()) )
                        {
                            posfix.Add(tmpStk.Pop());
                        }
                        tmpStk.Push(str);
                    }
                }
            }

            while (tmpStk.Count != 0)
            {
                posfix.Add(tmpStk.Pop());
            }

            input.Clear();
            input = posfix;
        }

        private int priority(string opr)
        {
            int pr = 0;
            switch(opr)
            {
                case "+": return 1;
                case "-": return 1;
                case "*": return 2;
                case "/": return 2;
                case "(": return 0;
            }
            return pr;
        }
    }
}
