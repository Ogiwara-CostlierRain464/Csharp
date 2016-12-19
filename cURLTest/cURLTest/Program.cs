using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Web;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace cURLTest
{
    class Program
    {
        const string TOKEN = "Q3gvR1pxaFhlbWxJWFN0Y1ZsVmlJaW5HSk5zSXFFOWFhcU00SUdoVFFmZUVvaWh4SnQ1Yll5RHRMbEdPTDk1ZklhRVgvOUNoTEZudHZtYjZPNkk1Y1NpSnV3MC9DS3Vld3c4eXJMS0lNd2xoYnc3bzdhdEFjeVE0dGhVWE5JZ3M3TUQwNlRaKzFwUmdYeEZUTFg1aGgxa1JLY2tIT0Mrc3F5eVNIV0hPQ2NsOUF2eXBzaXJrV3F5YjZxOEZ4VmNPM0J6QnVlM3NrZkc0YkpzSVZ1VS9qbWhYc0RaYithR3Z5TlE0TjZuQ00zUHpLcXJEdGl0Y2lITGoxNS9OWTJPRjFyQzY3Tnh4c1hQdkZZaFNyek01ZU5ZSXhVRnFwU2YwY1J2UGZJWmFHL1Q3bHZpTkFldHRWTjhESjQrak92TXhTdlYxeVRKQiswVEduZUlsY0NIUC96dmtHL0JsZmh6YnNjNTAvMHo3NUgvcUsyVVRadm41MllvMURkWUREcmEyZjl6MVA3a2Y4VVlsZmNXeE1PMXlzQVk0eDZjcWVHRFB5dW5zM25xYTZQdnpVZkc0eUJabXJGcFZZRjV5VFJiL2dGK21qU2RkYXNQb2JwU2lkcGVsRjAxOHJuUHlzOFUxeGtQTGlKTU5PeGM9";
        string accesstoken = "";

        static void Main(string[] args)
        {
            var p = new Program();
            p.run();
            Console.Read();
        }

        public void run()
        {
            login();

            Thread.Sleep(1000);

            access();
            Thread.Sleep(1000);
            for (int i = 0; i <= 5;i++)
            {
                //login();
                //access();
                timer();
                System.Threading.Thread.Sleep(30000);
            }
            Logout();
        }

        public void Logout()
        {
            sendData("update", new Dictionary<string, string> {
            { "type","stop"},
            { "access_token",accesstoken }//TODO
            });
        }

        public void login()
        {
            Console.WriteLine("LOGIN");
            sendData("update",new Dictionary<string, string> {
                { "max","150"},
                { "now","30" },
                { "type","start" },
                { "server_token",TOKEN }
            });
        }

        public void access()
        {
            Console.WriteLine("ACCESS");
            sendData("push", new Dictionary<string, string> {
                { "state","1"},
                { "access_token",accesstoken }
            });
        }

        public void updatePlayers(string type)
        {
            sendData("update", new Dictionary<string, string> {
                { "max","20"},
                { "now","10" },
                { "type",type },
                { "access_token",TOKEN }
            });
        }

        public void timer()
        {
            Console.WriteLine("TIMER");
            sendData("update", new Dictionary<string, string> {
                { "max","20"},
                { "type","time" },
                { "access_token",accesstoken }
            });
        }

        async public void sendData(string endpoint,Dictionary<string,string> data)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "pmmp");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("Plugin:hXBsxY_P7_")));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");//AcceptはPropertyがNG
           // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
            //client.DefaultRequestHeaders.Connection.Clear();
            //client.DefaultRequestHeaders.ConnectionClose = true;


            var requestContent = new FormUrlEncodedContent(data);
            //requestContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");

            HttpResponseMessage respone = await client.PostAsync(
                    "http://api.pmmp.jp.net/" + endpoint, requestContent
                );

            HttpContent responseContent = respone.Content;

            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                string json =  await reader.ReadToEndAsync();
                Console.WriteLine(json);
                Result r = JsonConvert.DeserializeObject<Result>(json);
                accesstoken = r?.token?.Replace("'", "");
            }

            respone.Dispose();
            responseContent.Dispose();
            requestContent.Dispose();
            client.Dispose();
            

            //汚い…
            /*string url = "http://api.pmmp.jp.net/" + endpoint;
            WebRequest myReq = WebRequest.Create(url);
            string credentials = "Plugin:hXBsxY_P7_";
            CredentialCache mycache = new CredentialCache();
            myReq.Headers["Authorization"] = "Basic" + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
            WebResponse wr = myReq.GetResponse();
            Stream receiveStream = wr.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream,Encoding.UTF8);
            string content = reader.ReadToEnd();
            Console.WriteLine(content);
            var json = "[" + content + "]";
            var objects = JArray.Parse(json);
            foreach (JObject o in objects.Children<JObject>())
            {
                foreach (JProperty p in o.Properties())
                {
                    string name = p.Name;
                    string value = p.Value.ToString();
                    Console.Write(name + ": " + value);
                }
            }*/

            //汚い!!!
            /*Encoding enc = Encoding.GetEncoding("shift_jis");

            string param = "";
            foreach (string key in data.Keys)
                param += String.Format("{0}={1}&", key, data[key]);

            byte[] postDataBytes = Encoding.ASCII.GetBytes(param);

            WebRequest req = WebRequest.Create("http://api.pmmp.jp.net/" + endpoint);
            string credentials = "Plugin:hXBsxY_P7_";
            req.Headers["Authorization"] = "Basic" + Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));//USERPWD
            req.Headers["UserAgent"] = "Basic" + Convert.ToBase64String(Encoding.ASCII.GetBytes("pmmp"));
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = postDataBytes.Length;
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(postDataBytes,0,postDataBytes.Length);
            reqStream.Close();

            WebResponse res = req.GetResponse();
            Stream resStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(resStream,enc);
            string json = sr.ReadToEnd();
            Result r = JsonConvert.DeserializeObject<Result>(json);

            if (r?.token != null)
            {
                accesstoken = r?.token;
            }
            Console.WriteLine("TOKEN" + r?.token);
            Console.WriteLine(json);
            sr.Close();*/
        }
    }

    class Result
    {
        public string status { get; set; }
        public string msg { get; set; }
        public string token { get; set; }
    }
}
