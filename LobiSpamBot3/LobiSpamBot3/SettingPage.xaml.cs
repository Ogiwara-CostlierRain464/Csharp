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
using LobiAPI.Json;

namespace LobiSpamBot3
{
    /// <summary>
    /// SettingPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingPage : Page//設定変更は即座に
    {
        BasicAPI basicapi;

        public SettingPage(BasicAPI api)
        {
            InitializeComponent();
            basicapi = api;
            LoadUserData();
            var settings = new Settings();
            settings.Load();
            ShoutButton.IsChecked = settings.ShoutInPrivateGroup;
        }

        async void LoadUserData()
        {
            Me me = await Task.Run(() => 
            {
                return basicapi.GetMe();
            });
            BitmapImage image = new BitmapImage(new Uri(me.cover));
            BackImage.Source = image;
            BitmapImage icon = new BitmapImage(new Uri(me.icon));
            IconImage.ImageSource = icon;
            Name.Text = me.name;
            PublicGroupsCount.Text = me.public_groups.Length.ToString();
            FollowCounter.Text = me.contacts_count.ToString();
            FollowerCounter.Text = me.followers_count.ToString();
            DescriptionTextBlock.Text = me.description;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ShoutButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = new Settings();
            settings.Load();
            settings.ShoutInPrivateGroup = (bool)ShoutButton.IsChecked;
            settings.Save();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var s = new Settings();
            s.Delete();
            var login = new LoginPage();
            NavigationService.Navigate(login);
        }
    }
}
