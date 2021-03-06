﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

using System.Net.Http;

using MiNET.Utils;

using Newtonsoft.Json;

namespace ServerList
{
    /// <summary>
    /// 全体的な流れ
    /// Login -> accesstoken取得 -> 一定間隔でUpDateTimeを送信 -> 終了時にLogout
    /// </summary>
    class ServerListAPI
    {
        Class1 plugin;
        /// <summary>
        /// このtokenは、ダウンロードしたServerList.pharのServerListAPI.phpのメンバ変数、"Token"に記入してあります。
        /// このtokenは、「RasisTeam」サーバーのものです
        /// </summary>
        string token = "Q3gvR1pxaFhlbWxJWFN0Y1ZsVmlJaW5HSk5zSXFFOWFhcU00SUdoVFFmZUVvaWh4SnQ1Yll5RHRMbEdPTDk1ZklhRVgvOUNoTEZudHZtYjZPNkk1Y1NpSnV3MC9DS3Vld3c4eXJMS0lNd2xoYnc3bzdhdEFjeVE0dGhVWE5JZ3M3TUQwNlRaKzFwUmdYeEZUTFg1aGgxa1JLY2tIT0Mrc3F5eVNIV0hPQ2NsOUF2eXBzaXJrV3F5YjZxOEZ4VmNPM0J6QnVlM3NrZkc0YkpzSVZ1VS9qbWhYc0RaYithR3Z5TlE0TjZuQ00zUHpLcXJEdGl0Y2lITGoxNS9OWTJPRjFyQzY3Tnh4c1hQdkZZaFNyek01ZU5ZSXhVRnFwU2YwY1J2UGZJWmFHL1Q3bHZpTkFldHRWTjhESjQrak92TXhTdlYxeVRKQiswVEduZUlsY0NIUC96dmtHL0JsZmh6YnNjNTAvMHo3NUgvcUsyVVRadm41MllvMURkWUREcmEyZjl6MVA3a2Y4VVlsZmNXeE1PMXlzQVk0eDZjcWVHRFB5dW5zM25xYTZQdnpVZkc0eUJabXJGcFZZRjV5VFJiL2dGK21qU2RkYXNQb2JwU2lkcGVsRjAxOHJuUHlzOFUxeGtQTGlKTU5PeGM9";

        string accesstoken = "";

        public ServerListAPI(Class1 p)
        {
            plugin = p;
        }

        /// <summary>
        /// Debugビルド時に通信内容をstdoutに送る
        /// </summary>
        /// <param name="content"></param>
        [Conditional("DEBUG")]
        public void Log(string content)
        {
            Console.WriteLine(">>" +  content);
        }
        /// <summary>
        /// Login
        /// </summary>
        public async Task Login()
        {
            await SendData("update", new Dictionary<string, string> {
            { "max",Config.GetProperty("MaxNumberOfPlayers",20).ToString()},
            { "now","0"},
            { "type","start" },
            { "server_token",token }
            });

            Log("Login(login->access)");

            await SendData("push", new Dictionary<string, string> {
            { "state","1"},
            { "access_token",accesstoken }
            });
            Log("Logined");
        }
       

        public async Task Logout()
        {
            await SendData("update", new Dictionary<string, string> {
            { "type","stop"},
            { "access_token",accesstoken }//TODO
            });
        }

        public async Task Event(string status)
        {
            status = status == "on" ? "event" : "normal";
            await SendData("update", new Dictionary<string, string> {
            { "type",status},
            { "access_token",accesstoken }//TODO
            });
        }

        public async Task UpDateTime()
        {
            await SendData("update", new Dictionary<string, string> {
            { "max",Config.GetProperty("MaxNumberOfPlayers",20).ToString()},
            { "type","time"},
            { "access_token",accesstoken }//TODO
            });
            Log("UpDateTime");
        }


        /// <summary>
        /// プレイヤー入退出時に、人数の変更を適用します。
        /// </summary>
        /// <param name="type"></param>
        public async Task UpDatePlayers(string type){
            await SendData("update", new Dictionary<string, string> {
            { "max",Config.GetProperty("MaxNumberOfPlayers",20).ToString()},
            { "now",plugin.Server.UserManager.Users.ToList().Count.ToString() },
            { "type",type},
            { "access_token",accesstoken }//TODO
            });
        }
        /// <summary>
        /// 指定したendpointにdataをポストし、accesstokenを更新します。
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="data"></param>
        async public Task<string> SendData(string endpoint, Dictionary<string, string> data)
        {
            using (var client = new HttpClient())
            {
                var requestContent = new FormUrlEncodedContent(data);

                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "pmmp");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("Plugin:hXBsxY_P7_")));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");

                HttpResponseMessage respone = await client.PostAsync(
                    "http://api.pmmp.jp.net/" + endpoint, requestContent
                );

                HttpContent responseContent = respone.Content;

                using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
                {
                    string json = await reader.ReadToEndAsync();
                    Log(json);
                    Result r =  JsonConvert.DeserializeObject<Result>(json);
                    accesstoken = r?.token?.Replace("'","");
                    return r.msg;
                }
            }
        }
    }

    /// <summary>
    /// Json シリアライズ用クラス
    /// </summary>
    class Result
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string token { get; set; }
    }
}
