using Model;
using System.Windows;
using System.Windows.Controls;
using YahelApiService;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for AddArtist.xaml
    /// </summary>
    public partial class AddArtist : Page
    {
        List<string> PersonStrings = new List<string>();
        PersonList pList = new PersonList();
        Person Per = null;
        public AddArtist()
        {
            InitializeComponent();
            GetPerson();
        }
        public async Task GetPerson()
        {
            await GetAllPeople();
            this.PersonNameComboBox.ItemsSource = null;
            this.PersonNameComboBox.ItemsSource = pList.Select(x => x.FullName);

        }
        public async Task GetAllPeople()
        {
            YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
            pList = await yApi.GetPerson();

        }
        private void AddButton(object sender, RoutedEventArgs e)
        {
            YahelApiService.YahelApiSrv y = new YahelApiSrv();
            Artists a = new Artists();
            a.FirstName1 = pList.Find(x => x.FullName == PersonNameComboBox.SelectedItem as string).FirstName1;
            a.LastName1 = pList.Find(x => x.FullName == PersonNameComboBox.SelectedItem as string).LastName1;

            a.Debut = DebutTextBox.Text.ToString();
            y.InsertArtist(a);
        }

    }
}
