using Model;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using YahelApiService;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private readonly Regex phoneRegex = new Regex(@"05\d{8}$");
        private readonly Regex gmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@gmail\.com$");
        List<string> subscriptionStrings = new List<string>();
        SubscriptionTypeList stList = new SubscriptionTypeList();
        CountryList cList = new CountryList();
        List<string> countryStrings = new List<string>();
        List<string> languageStrings = new List<string>();
        LanguageList lList = new LanguageList();
        Language lan = null;
        Country con = null;
        SubscriptionType sub = null;
        public ProfilePage()
        {
            InitializeComponent();
            GetData();


        }
        public async Task GetData()
        {
            YahelApiService.YahelApiSrv api = new YahelApiService.YahelApiSrv();
            Model.UsersList uList = await api.GetUsers();
            Users usr = uList.Find(x => x.Id == Login.LoggedInUsers.Id);
            this.FirstNameTextBox.Text = usr.FirstName1;
            this.LastNameTextBox.Text = usr.LastName1;
            this.BirthYearTextBox.Text = usr.BirthYear;
            this.PhoneTextBox.Text = usr.Phone1;

            this.CountryTextBox.Text = usr.Country1.CountryName;
            this.GmailNameTextBox.Text = usr.Gmail1;
            this.LanguageNameComboBox.Text = usr.Language1.LanguageName;
            this.SubscriptionNameTextBox.Text = usr.Subscription1.SubscriptionName;
            GetCountry();
        }
        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            if (!phoneRegex.IsMatch(PhoneTextBox.Text.ToString()))
            {
                MessageBox.Show("Phone number is not correct");
                return;
            }
            if (!gmailRegex.IsMatch(GmailNameTextBox.Text.ToString()))
            {
                MessageBox.Show("Gmail is not correct");
                return;
            }
            YahelApiService.YahelApiSrv s = new YahelApiService.YahelApiSrv();
            Login.LoggedInUsers.FirstName1 = FirstNameTextBox.Text.ToString();
            Login.LoggedInUsers.LastName1 = LastNameTextBox.Text.ToString();
            Login.LoggedInUsers.Gmail1 = GmailNameTextBox.Text.ToString();
            Login.LoggedInUsers.BirthYear = BirthYearTextBox.Text.ToString();
            Login.LoggedInUsers.Language1 = lList.Find(x => x.LanguageName == LanguageNameComboBox.SelectedItem);
            Login.LoggedInUsers.Country1 = cList.Find(x => x.CountryName == CountryTextBox.SelectedItem);
            if (Login.LoggedInUsers.IsManager)
            {
                Login.LoggedInUsers.IsManager = true;
            }
            else
            {
                Login.LoggedInUsers.IsManager = false;
            }
            Login.LoggedInUsers.Subscription1 = stList.Find(x => x.SubscriptionName == SubscriptionNameTextBox.SelectedItem);
            s.UpdateUser(Login.LoggedInUsers);
        }
        public async Task GetCountry()
        {
            await GetAllCountries();
            this.CountryTextBox.ItemsSource = null;
            this.CountryTextBox.ItemsSource = cList.Select(x => x.CountryName);
            GetSubscription();
        }
        public async Task GetAllCountries()
        {
            YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
            cList = await yApi.GetCountries();

        }
        public async Task GetSubscription()
        {
            await GetAllSubscriptions();
            this.SubscriptionNameTextBox.ItemsSource = null;
            this.SubscriptionNameTextBox.ItemsSource = stList.Select(x => x.SubscriptionName);
            GetLanguages();
        }
        public async Task GetAllSubscriptions()
        {

            YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
            stList = await yApi.GetSubscriptionsTypes();


        }
        public async Task GetLanguages()
        {
            await GetAllLanguages();
            this.LanguageNameComboBox.ItemsSource = null;
            this.LanguageNameComboBox.ItemsSource = lList.Select(x => x.LanguageName);

        }
        public async Task GetAllLanguages()
        {
            try
            {
                YahelApiService.YahelApiSrv yApi = new YahelApiSrv();
                lList = await yApi.GetLanguages();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

    }
}
