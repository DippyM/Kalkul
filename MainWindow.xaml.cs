using System;
using System.Windows;
using System.Windows.Input;

namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
        private double currentValue = 0;  // Aktuální hodnota
        private string currentOperator = "";  // Uložený operátor
        private bool isOperatorClicked = false;  // Kontrola, zda byl operátor vybrán
        private bool isEqualPressed = false;  // Kontrola, zda bylo stisknuto "="

        public MainWindow()
        {
            InitializeComponent();
        }

        // Funkce pro zadávání čísel
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string number = (string)((System.Windows.Controls.Button)sender).Tag;

            if (isOperatorClicked || isEqualPressed || ResultDisplay.Text == "0")
            {
                ResultDisplay.Text = number;
                isOperatorClicked = false;
                isEqualPressed = false;
            }
            else
            {
                ResultDisplay.Text += number;
            }
        }

        // Funkce pro zadávání operátorů (+, -, *, /)
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                string operatorClicked = button.Tag.ToString();

                if (!isOperatorClicked && !isEqualPressed)
                {
                    currentValue = double.Parse(ResultDisplay.Text);
                    PreviousOperationDisplay.Text = $"{currentValue} {operatorClicked}";
                }
                else if (isEqualPressed)
                {
                    PreviousOperationDisplay.Text = $"{currentValue} {operatorClicked}";
                }

                currentOperator = operatorClicked;
                isOperatorClicked = true;
                isEqualPressed = false;
            }
        }

        // Výpočet výsledku
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            double secondValue;
            if (!double.TryParse(ResultDisplay.Text, out secondValue))
            {
                ResultDisplay.Text = "Invalid Input";
                return;
            }

            switch (currentOperator)
            {
                case "+":
                    ResultDisplay.Text = (currentValue + secondValue).ToString();
                    break;
                case "-":
                    ResultDisplay.Text = (currentValue - secondValue).ToString();
                    break;
                case "×":
                    ResultDisplay.Text = (currentValue * secondValue).ToString();
                    break;
                case "÷":
                    if (secondValue != 0)
                    {
                        ResultDisplay.Text = (currentValue / secondValue).ToString();
                    }
                    else
                    {
                        ResultDisplay.Text = "Error";
                    }
                    break;
            }

            PreviousOperationDisplay.Text = $"{currentValue} {currentOperator} {secondValue} =";
            currentValue = double.Parse(ResultDisplay.Text);
            isEqualPressed = true;
        }

        // Mazání celé kalkulačky (Clear)
        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = "0";
            PreviousOperationDisplay.Text = "";
            currentValue = 0;
            currentOperator = "";
        }

        // Mazání posledního vstupu
        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = "0";
        }

        // Backspace pro odstranění posledního znaku
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (ResultDisplay.Text.Length > 1)
            {
                ResultDisplay.Text = ResultDisplay.Text.Substring(0, ResultDisplay.Text.Length - 1);
            }
            else
            {
                ResultDisplay.Text = "0";
            }
        }

        // Změna znaménka
        private void ToggleSign_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                value = -value;
                ResultDisplay.Text = value.ToString();
            }
        }

        // Desetinná tečka
        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (!ResultDisplay.Text.Contains("."))
            {
                ResultDisplay.Text += ".";
            }
        }

        // Procenta
        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(ResultDisplay.Text) / 100;
            PreviousOperationDisplay.Text = $"{ResultDisplay.Text} %";
            ResultDisplay.Text = value.ToString();
        }

        // Druhá odmocnina
        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            double value = Math.Sqrt(double.Parse(ResultDisplay.Text));
            PreviousOperationDisplay.Text = $"√({ResultDisplay.Text})";
            ResultDisplay.Text = value.ToString();
        }

        // Druhá mocnina
        private void Square_Click(object sender, RoutedEventArgs e)
        {
            double value = Math.Pow(double.Parse(ResultDisplay.Text), 2);
            PreviousOperationDisplay.Text = $"{ResultDisplay.Text}²";
            ResultDisplay.Text = value.ToString();
        }

        // Zpracování vstupu z klávesnice
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                // Čísla
                case Key.D0:
                case Key.NumPad0:
                    HandleNumberClick("0");
                    break;
                case Key.D1:
                case Key.NumPad1:
                    HandleNumberClick("1");
                    break;
                case Key.D2:
                case Key.NumPad2:
                    HandleNumberClick("2");
                    break;
                case Key.D3:
                case Key.NumPad3:
                    HandleNumberClick("3");
                    break;
                case Key.D4:
                case Key.NumPad4:
                    HandleNumberClick("4");
                    break;
                case Key.D5:
                case Key.NumPad5:
                    HandleNumberClick("5");
                    break;
                case Key.D6:
                case Key.NumPad6:
                    HandleNumberClick("6");
                    break;
                case Key.D7:
                case Key.NumPad7:
                    HandleNumberClick("7");
                    break;
                case Key.D8:
                case Key.NumPad8:
                    HandleNumberClick("8");
                    break;
                case Key.D9:
                case Key.NumPad9:
                    HandleNumberClick("9");
                    break;

                // Operátory pro numerickou klávesnici
                case Key.Add:
                    HandleOperatorClick("+");
                    break;
                case Key.Subtract:
                    HandleOperatorClick("-");
                    break;
                case Key.Multiply:
                    HandleOperatorClick("×");
                    break;
                case Key.Divide:
                    HandleOperatorClick("÷");
                    break;

                // Operátory pro hlavní klávesnici
                case Key.OemPlus:
                    HandleOperatorClick("+");
                    break;
                case Key.OemMinus:
                    HandleOperatorClick("-");
                    break;
                case Key.OemQuestion:
                    HandleOperatorClick("÷");
                    break;

                // Desetinná tečka
                case Key.Decimal:
                case Key.OemComma:
                case Key.OemPeriod:
                    Dot_Click(null, null);
                    break;

                // Rovná se
                case Key.Enter:
                    Equal_Click(null, null);
                    break;

                // Backspace pro mazání
                case Key.Back:
                    Backspace_Click(null, null);
                    break;

                case Key.Escape:
                    ClearAll_Click(null, null);
                    break;
            }
        }

        private void HandleNumberClick(string number)
        {
            if (isOperatorClicked || isEqualPressed || ResultDisplay.Text == "0")
            {
                ResultDisplay.Text = number;
                isOperatorClicked = false;
                isEqualPressed = false;
            }
            else
            {
                ResultDisplay.Text += number;
            }
        }

        private void HandleOperatorClick(string operatorClicked)
        {
            if (!isOperatorClicked && !isEqualPressed)
            {
                currentValue = double.Parse(ResultDisplay.Text);
                PreviousOperationDisplay.Text = $"{currentValue} {operatorClicked}";
            }
            else if (isEqualPressed)
            {
                PreviousOperationDisplay.Text = $"{currentValue} {operatorClicked}";
            }

            currentOperator = operatorClicked;
            isOperatorClicked = true;
            isEqualPressed = false;
        }
    }
}
