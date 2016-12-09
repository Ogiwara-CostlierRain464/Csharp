using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

namespace LobiSpamBot3
{
    class Settings
    {
        const string FileName = "spamsettings.json";

        public string MailAddress { get; set; }
        public string Password { get; set; }
        public bool ShoutInPrivateGroup { get; set; }

        public bool Exists()
        {
            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());

            return dir.GetFiles().Any(f => f.Name.Equals(FileName));//Linq
        }

        public void Load()
        {
            if (!Exists()) return;
            using (StreamReader r = new StreamReader(FileName))
            {
                string j = r.ReadToEnd();
                Settings s =  JsonConvert.DeserializeObject<Settings>(j);
                MailAddress = s.MailAddress;
                Password = s.Password;
                ShoutInPrivateGroup = s.ShoutInPrivateGroup;
            }
        }

        public void Save()
        {
            using (StreamWriter w = new StreamWriter(FileName))
            {
                w.WriteLine(JsonConvert.SerializeObject(this));
            }
        }

        public void Delete()
        {
            File.Delete(FileName);
        }
    }

    /*class Settings//インスタンス化
    {
        public void Save()
        {
            using (StreamWriter w = new StreamWriter(FileName))
            {
                var s = new Setting
                {
                    MailAddress = MailAddress,
                    Password = Password,
                    ShoutInPrivateGroup = Shout
                };
                w.WriteLine(s);
            }
        }

        void Create(string mail,string pass)
        {
            var s = new Setting
            {
                MailAddress = mail,
                Password = pass,
                ShoutInPrivateGroup = true
            };
            var json = JsonConvert.SerializeObject(s);
            System.Diagnostics.Debug.WriteLine(json);
        }
    }*/
}
