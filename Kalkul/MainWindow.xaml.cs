using System;
using System.Windows;
using System.Globalization;
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
            CultureInfo.CurrentCulture = new CultureInfo("cs-CZ");
            CultureInfo.CurrentUICulture = new CultureInfo("cs-CZ");
        }

        // Funkce pro zadávání čísel
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string number = (string)((System.Windows.Controls.Button)sender).Content;
            HandleNumberClick(number);
        }


        // Verze pro klávesnici
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


        // Funkce pro zadávání operátorů (+, -, *, /)
        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                string operatorClicked = button.Tag.ToString();
                HandleOperatorClick(operatorClicked); // Zavoláme interní metodu
            }
        }
        private void HandleOperatorClick(string operatorClicked)
        {
            if (!isOperatorClicked && !isEqualPressed)
            {
                // Uložíme aktuální hodnotu do proměnné
                currentValue = double.Parse(ResultDisplay.Text);
                PreviousOperationDisplay.Text = $"{currentValue} {operatorClicked}";
            }
            else if (isEqualPressed)
            {
                // Pokud bylo předtím stisknuto "=", zobrazíme nový operátor
                PreviousOperationDisplay.Text = $"{currentValue} {operatorClicked}";
            }

            // Uložíme aktuální operátor
            currentOperator = operatorClicked;
            isOperatorClicked = true;
            isEqualPressed = false;
        }



        // Funkce pro výpočet výsledku
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

        // Funkce pro tlačítko "C" (clear)
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = "0";
            PreviousOperationDisplay.Text = "";
            currentValue = 0;
            currentOperator = "";
        }

        // Funkce pro desetinnou tečku
        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (!ResultDisplay.Text.Contains(","))
            {
                ResultDisplay.Text += ",";
            }
        }

        // Funkce pro procenta
        private void Percent_Click(object sender, RoutedEventArgs e)
        {
            double value = double.Parse(ResultDisplay.Text) / 100;
            PreviousOperationDisplay.Text = $"{ResultDisplay.Text} %";
            ResultDisplay.Text = value.ToString();
        }

        // Funkce pro druhou odmocninu
        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            double value = Math.Sqrt(double.Parse(ResultDisplay.Text));
            PreviousOperationDisplay.Text = $"√({ResultDisplay.Text})";
            ResultDisplay.Text = value.ToString();
        }

        // Funkce pro druhou mocninu
        private void Square_Click(object sender, RoutedEventArgs e)
        {
            double value = Math.Pow(double.Parse(ResultDisplay.Text), 2);
            PreviousOperationDisplay.Text = $"{ResultDisplay.Text}²";
            ResultDisplay.Text = value.ToString();
        }
        private void CubeRoot_Click(object sender, RoutedEventArgs e)
        {
            double value = Math.Pow(double.Parse(ResultDisplay.Text), 1.0 / 3.0);
            PreviousOperationDisplay.Text = $"³√({ResultDisplay.Text})";
            ResultDisplay.Text = value.ToString();
        }

        private void Cube_Click(object sender, RoutedEventArgs e)
        {
            double value = Math.Pow(double.Parse(ResultDisplay.Text), 3);
            PreviousOperationDisplay.Text = $"{ResultDisplay.Text}³";
            ResultDisplay.Text = value.ToString();
        }

        private void Factorial_Click(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(ResultDisplay.Text);
            int result = 1;
            for (int i = 1; i <= number; i++)
            {
                result *= i;
            }
            PreviousOperationDisplay.Text = $"{ResultDisplay.Text}!";
            ResultDisplay.Text = result.ToString();
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            double value = Math.Log10(double.Parse(ResultDisplay.Text));
            PreviousOperationDisplay.Text = $"log({ResultDisplay.Text})";
            ResultDisplay.Text = value.ToString();
        }


        // Funkce pro klávesnici
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
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
                case Key.Add: // NumPad +
                    HandleOperatorClick("+");
                    break;
                case Key.Subtract: // NumPad -
                    HandleOperatorClick("-");
                    break;
                case Key.Multiply: // NumPad *
                    HandleOperatorClick("×");
                    break;
                case Key.Divide: // NumPad /
                    HandleOperatorClick("÷");
                    break;

                // Operátory pro hlavní klávesnici
                case Key.OemPlus: // +
                    HandleOperatorClick("+");
                    break;
                case Key.OemMinus: // -
                    HandleOperatorClick("-");
                    break;
                case Key.OemQuestion: // /
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

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            // Odstraní poslední znak z aktuální hodnoty
            if (ResultDisplay.Text.Length > 1)
            {
                ResultDisplay.Text = ResultDisplay.Text.Substring(0, ResultDisplay.Text.Length - 1);
            }
            else
            {
                ResultDisplay.Text = "0";
            }
        }

        private void ToggleSign_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultDisplay.Text, out double value))
            {
                value = -value;
                ResultDisplay.Text = value.ToString();
            }
        }

        private void ClearAll_Click(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = "0";
            PreviousOperationDisplay.Text = "";
            currentValue = 0;
            currentOperator = "";
        }

        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            ResultDisplay.Text = "0";
        }
    }
}
