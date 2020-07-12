using LisMusic.accounts;
using LisMusic.accounts.domain;
using LisMusic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace LisMusic.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
       
        public LoginPage()
        {
            InitializeComponent();
        
        }

        private void GoToRegister(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegisterPage());
        }

        private void Login(object sender, RoutedEventArgs e)
        {

            try
            {
                if (ValidateEmptyFields())
                {
                    LoginRequest loginRequest = new LoginRequest() { user = TextBox_user.Text, password = PasswordBox_password.Password };
                    LoginResponse loginResponse = AccountRepository.LoginAccount(loginRequest);
                    SingletonSesion.SetSingletonSesion(loginResponse);
                    MainWindow main = new MainWindow();
                    main.Show();
                    Window.GetWindow(this).Close();
                    Console.WriteLine(SingletonSesion.GetSingletonSesion().access_token);
                } else
                {
                    MessageBox.Show("Empty fields");
                }
            }
           catch(Exception ex) 
            {
               MessageBox.Show(ex.Message);

            }
        }


        public bool ValidateEmptyFields()
        {
            bool areValids = true;

            if (String.IsNullOrEmpty(TextBox_user.Text))
            {
                areValids = false;
            } else if (String.IsNullOrEmpty(PasswordBox_password.Password))
            {
                areValids = false;
            }

            return areValids;
        }


    }
}
