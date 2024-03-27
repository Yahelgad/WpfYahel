using Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfYahel
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public static Users LoggedInUsers { get; set; }
        public Login()
        {
            InitializeComponent();
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());
        }
        public async Task<Users> SpecificUser(string gmail, string phone)
        {
            YahelApiService.YahelApiSrv apiService = new YahelApiService.YahelApiSrv();
            var userList = await apiService.GetUsers();
            return userList.Find(x => x.Gmail1 == gmail && x.Phone1 == phone);
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string gmail = gmailText.Text;
                string phone = phoneNumberText.Text;
                Users user = await SpecificUser(gmail, phone);
                if (user == null)
                {
                    MessageBox.Show("Invalid Gmail or Phone Number. Please try again", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                LoggedInUsers = user;
                if (!user.IsManager)
                {
                    NavigationService.Navigate(new HomePage());
                }
                else
                {
                    NavigationService.Navigate(new AdminHomePage());
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillButton_Click(object sender, RoutedEventArgs e)
        {
            this.gmailText.Text = "ori1111@gmail.com";
            this.phoneNumberText.Text = "0505584972";
        }
    }
}
