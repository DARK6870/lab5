using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace lab5
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Items { get; set; }

        Currencies currencies = new Currencies();

        private void ChangeCourse()
        {
            MessageBoxResult result = MessageBox.Show("Use the course for card transfers? (Yes - OK, No - Cancel)", "Currency type", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                currencies.AddCardCurrencyData();
                type_label.Content = "Course type: card transfers";
            }
            else
            {
                currencies.AddMaibData();
                type_label.Content = "Course type: MAIB";
            }
            if (Items != null)
            {
                Items.Clear();
                foreach (var item in currencies.GetCurrencyNames())
                {
                    Items.Add(item);
                }
            }
            else
            {
                Items = new ObservableCollection<string>(currencies.GetCurrencyNames());
            }

            NotifyPropertyChanged(nameof(Items));
        }

        public MainWindow()
        {
            InitializeComponent();
            ChangeCourse();
            DataContext = this;
        }

        private void ShowData(CurrencyData data)
        {
            couse_label.Content = Math.Round(data.price, 2).ToString();
            buy_label.Content = Math.Round(data.buy, 2).ToString();
            sell_label.Content = Math.Round(data.sell, 2).ToString();
            currency_image.Source = new BitmapImage(new Uri($"pack://application:,,,/img/{data.name}.png"));
        }

        private void convert_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double amount = Convert.ToDouble(amount_tb.Text);
                string from = from_tb.Text;
                string to = to_tb.Text;

                if (from != to)
                {
                    CurrencyData data = new CurrencyData();
                    if (to == "MDL")
                    {
                        data = currencies.ConvertToMDL(from, amount);
                    }
                    else if (from == "MDL")
                    {
                        data = currencies.ConvertFromMDL(to, amount);
                    }
                    else
                    {
                        data = currencies.ConvertToMDL(from, amount);
                        data = currencies.ConvertFromMDL(to, data.price);
                    }
                    data.name = to;
                    ShowData(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            ChangeCourse();
        }

        private void amount_tb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]*(\,[0-9]*)?$");
        }
    }
}
