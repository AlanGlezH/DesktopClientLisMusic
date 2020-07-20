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
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {

        public AccountPage()
        {
            InitializeComponent();
            LoadProfile();
        }

        private void Button_back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }

        private void LoadProfile()
        {
            Account account = SingletonSesion.GetSingletonSesion().account;
            TextBlock_name_account.Text = account.ToString();
            TextBlock_email_account.Text = account.email;
            TextBlock_username_account.Text = account.userName;
            TextBlock_birthday_account.Text = account.birthday;
            TextBlock_gender_account.Text = account.gender;
            string contentCreator;
            Console.WriteLine(account.contentCreator);
            if (account.contentCreator)
            {
                contentCreator = "Yes";
                Button_be_contentCreator.Visibility = Visibility.Hidden;
                
            }
            else
            {
                contentCreator = "No";
            }
            TextBlock_content_creator_account.Text = contentCreator;
        }

        private void Button_be_contentCreator_Click(object sender, RoutedEventArgs e)
        {
            FloatingWindow floating = new FloatingWindow(new CreateArtistPage());
            floating.ShowDialog();
        }
    }
}
