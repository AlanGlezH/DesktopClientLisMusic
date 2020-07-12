using LisMusic.accounts;
using LisMusic.accounts.domain;
using System;
using System.Windows;
using System.Windows.Controls;

namespace LisMusic.Views
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoginPage());
        }

        private bool ValidateEmptyFields()
        {
            bool areValids = true;
            if (String.IsNullOrEmpty(TextBox_first_name.Text))
            {
                areValids = false;
            }
            else if (String.IsNullOrEmpty(TextBox_last_name.Text))
            {
                areValids = false;
            }
            else if (String.IsNullOrEmpty(TextBox_email.Text))
            {
                areValids = false;
            }
            else if (RadioButton_female.IsChecked == false && RadioButton_male.IsChecked == false)
            {
                areValids = false;
            }
            else if (String.IsNullOrEmpty(DatePicker_birthday.Text))
            {
                areValids = false;
            }
            else if (String.IsNullOrEmpty(PasswordBox_password.Password))
            {
                areValids = false;
            }
            else if (String.IsNullOrEmpty(PasswordBox_confirm_password.Password))
            {
                areValids = false;
            }

            return areValids;
        }

        private bool ValidateSamePasswords()
        {
            bool areSame = true;

            if (!PasswordBox_password.Password.Equals(PasswordBox_confirm_password.Password))
            {
                areSame = false;
            }
            return areSame;

        }

        private void Button_register_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateEmptyFields())
            {
                MessageBox.Show("Empty fields");
            }
            else if (!ValidateSamePasswords())
            {
                MessageBox.Show("Please enter the same password");
            }
            else
            {
                try
                {
                    Account account = new Account()
                    {
                        birthday = DatePicker_birthday.SelectedDate.Value.ToString("yyyy-MM-dd"),
                        contentCreator = false,
                        cover = "defaultAccountCover.jpg",
                        created = DateTime.Now.ToString("yyyy-MM-dd"),
                        email = TextBox_email.Text,
                        firstName = TextBox_first_name.Text,
                        gender = AsignGender(),
                        lastName = TextBox_last_name.Text,
                        password = PasswordBox_password.Password,
                        typeRegister = "System",
                        updated = null,
                        userName = TextBox_username.Text

                    };

                    AccountRepository.RegisterAccount(account);
                    MessageBox.Show("Account successfully registered");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }  
        }

        public String AsignGender()
        {
            string gender = null;
            if (RadioButton_female.IsChecked.Value)
            {
                gender = "Female";
            } else if (RadioButton_male.IsChecked.Value)
            {
                gender = "Male";
            }
            return gender;
        } 
    }

    
}