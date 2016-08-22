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

namespace AWM.Controls
{
    /// <summary>
    /// Interaction logic for TestTextBox.xaml
    /// </summary>
    public partial class TestTextBox : UserControl
    {
        public TestTextBox()
        {
            InitializeComponent();
            Min = 0.0m;
            Max = 0.0m;
            Value = 0.0m;
            SimpleText.Text = "0.0";
        }

        public decimal Value { get; set; }
        private bool HasPeriod { get; set; }
        public decimal Max { get; set; }
        public decimal Min { get; set; }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text[0]) && 
                        e.Text[0] != '.' &&
                        e.Text[0] != '-' || 
                        (HasPeriod && e.Text[0] == '.');

            if (e.Text[0] == '\r')
            {
                e.Handled = Validate();
            }
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            HasPeriod = SimpleText.Text.Contains('.');
        }

        private bool Validate()
        {
            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("^[-+]?[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?$");
            bool handled = !re.IsMatch(SimpleText.Text);

            if (!handled)
            {
                Value = Convert.ToDecimal(SimpleText.Text);
                if (Min != Max)
                {
                    Value = Math.Max(Min, Math.Min(Max, Value));
                    SimpleText.Text = Value.ToString();
                }
            }
            else
            {
                SimpleText.Text = Value.ToString();
            }

            return handled;
        }

        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            e.Handled = Validate();
        }

        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            SimpleText.SelectAll();
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SimpleText.SelectAll();
        }
    }
}
