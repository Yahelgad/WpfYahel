using Model;
using System.Windows;
using System.Windows.Controls;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for AddCountry.xaml
    /// </summary>
    public partial class AddCountry : Page
    {
        public AddCountry()
        {
            InitializeComponent();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv s = new YahelApiService.YahelApiSrv();
            Country country = new Country();
            country.CountryName = CountryNameTextBox.Text.ToString();
            s.InsertCountry(country);
        }
    }
}
