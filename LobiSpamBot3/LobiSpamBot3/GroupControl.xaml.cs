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

using LobiAPI.Json;

namespace LobiSpamBot3
{
    /// <summary>
    /// GroupControl.xaml の相互作用ロジック
    /// </summary>
    public partial class GroupControl : UserControl
    {
        Group group;

        public GroupControl(Group g)
        {
            InitializeComponent();
            group = g;
            GroupName.Text = group.name;
            //System.Diagnostics.Debug.WriteLine("icon" + group.icon);
            LoadIcon(group.icon);
        }

        void LoadIcon(string url)
        {
            BitmapImage image = new BitmapImage(new Uri(url));
            GroupIcon.Source = image;

        }

        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            GroupListPage.getInstance().NavigateSpamPage(group);
        }
    }
}
