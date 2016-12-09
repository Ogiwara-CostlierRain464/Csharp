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
    /// GroupListPage.xaml の相互作用ロジック
    /// </summary>
    public partial class GroupListPage : Page
    {
        public static GroupListPage instance;

        public BasicAPI basicapi;

        List<GroupControl> publicgroupcontrol = new List<GroupControl>();

        List<GroupControl> privategroupcontrol = new List<GroupControl>();

        public static GroupListPage getInstance()
        {
            return instance;
        }

        public GroupListPage(BasicAPI api)
        {
            InitializeComponent();
            basicapi = api;
            LoadPublicGroups();
            /*for (int i = 0;i <= 20; i++)
            {
                var g = new GroupControl();
                GroupList.Children.Add(g);
            }*/
            instance = this;
        }

        private void GroupScopeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;//success
            if (box == null) return;
            switch (box.SelectedIndex) {
                case 0:
                    LoadPublicGroups();
                    break;
                case 1:
                    LoadPrivateGroups();
                    break;
            }
        }

        async void LoadPublicGroups()
        {
            if (basicapi == null) return;

            GroupMessage.Text = "読み込み中…";
            GroupList.Children.Clear();

            if (publicgroupcontrol.Count == 0)//読み込み
            {
                PublicGroups[] groups = await Task.Run(() => { return basicapi.GetPublicGroupList(); });
                foreach (Group g in groups[0].items)
                {
                    GroupControl c = new GroupControl(g);
                    publicgroupcontrol.Add(c);
                }
            }

            foreach (GroupControl c in publicgroupcontrol)
            {
                GroupList.Children.Add(c);
            }

            GroupMessage.Text = "グループを選択";
        }

        async void LoadPrivateGroups()
        {
            if (basicapi == null) return;

            GroupMessage.Text = "読み込み中…";
            GroupList.Children.Clear();

            if (privategroupcontrol.Count == 0)//読み込み
            {
                PrivateGroups[] groups = await Task.Run(() => { return basicapi.GetPrivateGroupList(); });
                foreach (Group g in groups[0].items)
                {
                    GroupControl c = new GroupControl(g);
                    privategroupcontrol.Add(c);
                }
            }

            foreach (GroupControl c in privategroupcontrol)
            {
                GroupList.Children.Add(c);
            }

            GroupMessage.Text = "グループを選択";
        }

        public void NavigateSpamPage(Group g)
        {
            Settings settings = new Settings();
            settings.Load();
            bool shout;
            //bool shout = GroupScopeComboBox.Text == "公開グループ" ? false : true;
            if (GroupScopeComboBox.Text != "公開グループ")
            {
                shout = settings.ShoutInPrivateGroup ? true : false;
            }else
            {
                shout = false;
            }
            SpamPage s = new SpamPage(basicapi,g,shout);
            NavigationService.Navigate(s);
        }

        private void SettingButton_Click(object sender, RoutedEventArgs e)
        {
            var s = new SettingPage(basicapi);
            NavigationService.Navigate(s);
        }
    }
}
