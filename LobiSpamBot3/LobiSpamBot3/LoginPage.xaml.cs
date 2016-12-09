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

using LobiAPI;

namespace LobiSpamBot3
{
    /// <summary>
    /// LoginPage.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            var settings = new Settings();
            if (settings.Exists())
            {
                settings.Load();
                MailAddress.Text = settings.MailAddress;
                Password.Password = settings.Password;
                LoginButton_Click(new Button(),new RoutedEventArgs());
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login(MailAddress.Text,Password.Password);
        }

        async void Login(string mail,string password)
        {
            LoginButton.Content = "ログイン中…";
            if (String.IsNullOrEmpty(mail) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("未記入の項目があります");
                LoginButton.Content = "Lobiにログイン";
                return;
            }

            Settings settings = new Settings();
            settings.Load();
            var api = new BasicAPI();
            try
            {
                var result = await Task.Run(() => { return api.Login(mail, password); });
                if (result)
                {
                    settings.MailAddress = MailAddress.Text;
                    settings.Password = Password.Password;
                    settings.ShoutInPrivateGroup = true;
                    settings.Save();

                    var group = new GroupListPage(api);
                    NavigationService.Navigate(group);
                }
                else
                {
                    MessageBox.Show("ログインに失敗しました");
                    LoginButton.Content = "Lobiにログイン";
                    return;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                MessageBox.Show("エラーが発生しました :" + e.Message + "\nこのエラーは大抵、サーバーへのアクセスが一時的に禁止されていることが原因です");
            }
            catch (Exception e)
            {
                MessageBox.Show("想定外のエラー :" + e.Message);
            }
        }
    }
}
