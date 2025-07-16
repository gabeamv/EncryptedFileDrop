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
using System.Security.Cryptography;
using EncryptedFileDropClient.DTOs;

namespace EncryptedFileDropClient.Pages.AuthPages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class LoginPage : Page
    {

        static readonly HttpClient client = new HttpClient();


        public LoginPage()
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

            // Anonymous class for user model data.
            var registerData = new
            {
                name = name,
                userName = userName,
                passwordHash = passwordHash
            };

            // Serialize the data into JSON format, as well as use it to create an HttpContent object of StringContent to send. 
            string json = JsonSerializer.Serialize(registerData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Send API request and await a response.
                HttpResponseMessage responseRegister = await client.PostAsync("https://localhost:7090/api/user/register", content);
                responseRegister.EnsureSuccessStatusCode();
                string response = await responseRegister.Content.ReadAsStringAsync();

                // Deserialize the response of registering to get the id of the user that was just added.
                //UserLoginResponse userLoginDto = JsonSerializer.Deserialize<UserLoginResponse>(response);

                // Pass the id of the user to the KeyGen function to generate their key.
                HttpResponseMessage responseKey = await KeyGen();
                responseKey.EnsureSuccessStatusCode();
                response = await responseKey.Content.ReadAsStringAsync();

                ApiTestTextBox.Text = response;
            }
            catch (HttpRequestException ex)
            {
                ApiTestTextBox.Text = $"Error: {ex.Message}";
            }
        }

        private async Task<HttpResponseMessage> KeyGen()
        {
            // RSA Key-Pair generation and export.
            RSA rsa = RSA.Create(2048);
            string publicKeyPem = rsa.ExportSubjectPublicKeyInfoPem();
            string privateKeyPem = rsa.ExportRSAPrivateKeyPem();

            var userKeyData = new
            {
                publicKey = publicKeyPem
            };

            string json = JsonSerializer.Serialize(userKeyData);

            var publicKeyContent = new StringContent(json, Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PostAsync("https://localhost:7090/api/key", publicKeyContent);
            return response;
           
        }

        // Method to test out API calls from client.
        private async void Button_ApiTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using HttpResponseMessage response = await client.GetAsync("https://localhost:7090/api/user");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                ApiTestTextBox.Text = responseBody;
            }
            catch (HttpRequestException ex)
            {
                ApiTestTextBox.Text = $"Error: {ex.Message}";
            }
        }

    }
}
