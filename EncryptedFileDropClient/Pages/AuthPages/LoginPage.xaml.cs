using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace EncryptedFileDropClient.Pages.AuthPages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {

        static readonly HttpClient client = new HttpClient();

        public Page1()
        {
            InitializeComponent();
        }

        private async void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            string userName = UsernameTextBox.Text;
            string passwordHash = PasswordTextBox.Password;

            var loginData = new
            {
                userName = userName,
                passwordHash = passwordHash
            };

            string json = JsonSerializer.Serialize(loginData);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync("https://localhost:7090/api/user/login", content);
                response.EnsureSuccessStatusCode();

                var mainWindow = Application.Current.MainWindow as MainWindow;
                if (mainWindow != null) 
                {
                    mainWindow.PageFrame.Content = new HomePages.HomePage();
                }

                string responseBody = await response.Content.ReadAsStringAsync();
                ApiTestTextBox.Text = responseBody;
                
            }
            catch (HttpRequestException ex)
            {
                ApiTestTextBox.Text = $"Error: {ex.Message}";
            }

        }

        // Register the user to the database.
        private async void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string userName = UsernameTextBox.Text;
            string passwordHash = PasswordTextBox.Password;

            var registerData = new
            {
                name = name,
                userName = userName,
                passwordHash = passwordHash
            };

            string json = JsonSerializer.Serialize(registerData);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using HttpResponseMessage response = await client.PostAsync("https://localhost:7090/api/user/register", content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                ApiTestTextBox.Text = responseBody;
            }
            catch (HttpRequestException ex)
            {
                ApiTestTextBox.Text = $"Error: {ex.Message}";
            }
        }

        // Method to test out API calls from client.
        private async void Button_ApiTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://localhost:7090/api/user");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                ApiTestTextBox.Text = responseBody;
            }
            catch (HttpRequestException ex)
            {
                ApiTestTextBox.Text = $"Error: {ex.Message}";
            }
        }

    }
}
