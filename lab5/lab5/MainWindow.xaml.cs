using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
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

        private void convert_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Items.Clear();
                foreach (var item in currencies.GetCurrencyNames())
                {
                    Items.Add(item);
                }

                NotifyPropertyChanged(nameof(Items));
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
