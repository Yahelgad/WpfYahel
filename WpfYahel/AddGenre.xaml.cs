using Model;
using System.Windows;
using System.Windows.Controls;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for AddGenre.xaml
    /// </summary>
    public partial class AddGenre : Page
    {
        public AddGenre()
        {
            InitializeComponent();

        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv s = new YahelApiService.YahelApiSrv();
            Jenre jenre = new Jenre();
            jenre.JenreName1 = GenreNameTextBox.Text.ToString();
            s.InsertJenre(jenre);
        }
    }
}
