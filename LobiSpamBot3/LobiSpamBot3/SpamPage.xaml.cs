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
    /// SpamPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SpamPage : Page
    {
        BasicAPI basicapi;

        Group group;

        bool _isspamon = false;

        bool _shout = false;

        public SpamPage(BasicAPI api,Group g,bool shout = false)
        {
            InitializeComponent();
            basicapi = api;
            group = g;
            _shout = shout;
            GroupName.Text = group.name;
        }

        private void SpamStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isspamon)
            {
                StopSpam();
            }
            else
            {
                if (SpamMessage.Text.Length >= 255)
                {
                    MessageBox.Show("255文字以内です");
                    return;
                }

                object message;
                if (RandCheckBox.IsChecked == true)
                {
                    message = new Random();
                }
                else
                {
                    message = SpamMessage.Text;
                }
                StartSpam(message);
            }
        }

        private void SpamMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextCounter.Text = SpamMessage.Text.Length.ToString() + "文字";
        }

        public void StartSpam(object message)
        {
            _isspamon = true;
            EnableButton();
            Task.Run(async () => 
            {
                while (_isspamon)
                {
                    try
                    {
                        string chat;
                        if (message is string)
                        {
                            chat = message as string;
                        }else//Random
                        {
                            Random r = message as Random;
                            chat = r.Next().ToString();
                        }
                        await basicapi.MakeThread(group.uid,chat,_shout);
                        Dispatcher.Invoke(new Action(() =>
                        {
                            LogListView.Items.Add("SEND");
                            SpamCounter.Text = (int.Parse(SpamCounter.Text) + 1).ToString();
                        }));
                    }
                    catch (Exception e)
                    {
                        Dispatcher.Invoke(new Action(() => 
                        {
                            LogListView.Items.Add(e.Message);
                        }));
                    }
                    await Task.Delay(5000);
                }
            });
        }

        public void StopSpam()
        {
            _isspamon = false;
            DisableButton();
        }

        public void EnableButton()//Spam中のボタンの変化
        {
            RandCheckBox.IsEnabled = false;
            SpamMessage.IsEnabled = false;
            SpamStartButton.Content = "中止";
        }

        public void DisableButton()//Spam終了時の変化
        {
            RandCheckBox.IsEnabled = true;
            SpamMessage.IsEnabled = true;
            SpamStartButton.Content = "スパム開始";
        }

        public void AddLog(string str)
        {
            LogListView.Items.Add(str);
        }

        private void RandCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (RandCheckBox.IsChecked == true)
            {
                SpamMessage.IsEnabled = false;
            }else
            {
                SpamMessage.IsEnabled = true;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            StopSpam();
            NavigationService.GoBack();
        }
    }
}
