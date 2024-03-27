using Model;
using System.Windows;
using System.Windows.Controls;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for AddLanguage.xaml
    /// </summary>
    public partial class AddLanguage : Page
    {
        public AddLanguage()
        {
            InitializeComponent();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv s = new YahelApiService.YahelApiSrv();
            Language Language = new Language();
            Language.LanguageName = LanguageNameTextBox.Text.ToString();
            s.InsertLanguage(Language);
        }
    }
}
