using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace TestCostumeControls
{
    class MaskTextBox : TextBox
    {
        public string _NullText = "Enter Value";

        public string NullText
        {
            get
            {
                return _NullText;
            }
            set
            {
                _NullText = value;
            }
        }
        public MaskType Mask { get; set; }

        public MaskTextBox()
        {
            this.Mask = MaskType.Default;
            this.GotFocus += new RoutedEventHandler(MaskTextBox_GotFocus);
            this.LostFocus += new RoutedEventHandler(MaskTextBox_LostFocus);
            //this.TextChanged += new TextChangedEventHandler(MaskTextBox_TextChanged);
            this.Initialized += new EventHandler(MaskTextBox_Initialized);
            this.KeyDown += new System.Windows.Input.KeyEventHandler(MaskTextBox_KeyDown);
            this.KeyUp += new KeyEventHandler(MaskTextBox_KeyUp);
        }

        void MaskTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = NullText;
                this.Foreground = Brushes.LightGray;
            }
            else
                this.Foreground = Brushes.Black;
        }
        void MaskTextBox_Initialized(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = NullText;
                this.Foreground = Brushes.LightGray;
            }
        }
        void MaskTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.Text.Contains(this.NullText))
                this.Clear();
            if (this.Mask == MaskType.Numeric)
            {
                if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.NumPad0 && e.Key == Key.NumPad9))
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            if (this.Mask == MaskType.String)
            {
                if (e.Key >= Key.A && e.Key <= Key.Z || e.Key == Key.Space)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }
        void MaskTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.Text != NullText)
            {
                this.Foreground = Brushes.Black;
            }
            e.Handled = true;
        }
        private void MaskTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (this.Text == NullText)
            {
                this.Clear();
                this.Foreground = Brushes.LightGray;
            }
            else
                this.Foreground = Brushes.Black;
        }
        private void MaskTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.Text = (this.Text == string.Empty ? NullText : this.Text);
            if (this.Text != NullText)
            {
                this.Foreground = Brushes.Black;
            }
            else
                this.Foreground = Brushes.LightGray;
        }
    }
    public enum MaskType
    {
        Default = 0,
        String = 1,
        Numeric = 2
    }
}