﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace QingTing.Fm
{
    public class HttpHelper {
        public static async Task<int> GetWebCode(String url) {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
                var o = (await hwr.GetResponseAsync()) as HttpWebResponse;
                return (int)o.StatusCode;
            }
            catch { return 404; }
        }
        public static async Task<string> GetWebForCodingAsync(string url) {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            hwr.Accept="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            hwr.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
            hwr.Headers.Add("Cache-Control", "max-age=0");
            hwr.KeepAlive = true;
            hwr.Headers.Add("Cookie", "sid=5129400e-3ae5-4d8f-b4c6-55a2d8dcf866; c=access-token%3Dtrue%2Ccoding-cli%3Dfalse%2Ccoding-ocd-pages%3Dtrue%2Ccoding-ocd%3Dtrue%2Ccoding-owas%3Dtrue%2Cdepot-sharing%3Dtrue%2Cmarkdown-graph%3Dtrue%2Cnew-home%3Dtrue%2Cqc-v2%3Dtrue%2Ctask-comment%3Dfalse%2Ctask-history%3Dtrue%2Cvip%3Dtrue%2Czip-download%3Dtrue%2C5d095651; exp=89cd78c2; frontlog_sample_rate=1");
            hwr.Host="coding.net";
            hwr.Referer=url;
            hwr.Headers.Add("Upgrade-Insecure-Requests", "1");
            hwr.UserAgent="Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
            hwr.Timeout = 20000;
            StreamReader sr = new StreamReader((await hwr.GetResponseAsync()).GetResponseStream(), Encoding.UTF8);
            var st = await sr.ReadToEndAsync();
            sr.Dispose();
            return st;
        }
        public static async Task<string> GetWebAsync(string url,Encoding e=null)
        {
                if (e == null)
                    e = Encoding.UTF8;
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
                hwr.Timeout = 20000;
                var o = await hwr.GetResponseAsync();
                StreamReader sr = new StreamReader(o.GetResponseStream(),e);
                var st = await sr.ReadToEndAsync();
                sr.Dispose();
                return st;
        }
        public static string GetWeb(string url, Encoding e = null)
        {
            if (e == null)
                e = Encoding.UTF8;
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            hwr.Timeout = 20000;
            var o = hwr.GetResponse();
            StreamReader sr = new StreamReader(o.GetResponseStream(), e);
            var st = sr.ReadToEnd();
            sr.Dispose();
            return st;
        }
        public static WebHeaderCollection GetWebHeader_MKBlog() {
            var whc = new WebHeaderCollection();
            whc.Add("Accept", "text/javascript, application/javascript, application/ecmascript, application/x-ecmascript, */*; q=0.01");
            whc.Add("Content-Type", "application/x-www-form-urlencoded");
            whc.Add("Cookie", "Hm_lvt_6e8dac14399b608f633394093523542e=1522910113; Hm_lpvt_6e8dac14399b608f633394093523542e=1522910122; Hm_lvt_ea4269d8a00e95fdb9ee61e3041a8f98=1522910125; Hm_lpvt_ea4269d8a00e95fdb9ee61e3041a8f98=1522910125");
            whc.Add("Host", "lab.mkblog.cn");
            whc.Add("Origin", "http://lab.mkblog.cn");
            whc.Add("Referer", "http://lab.mkblog.cn/music/");
            whc.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3325.181 Safari/537.36");
            return whc;
        }
      
        public static string PostWeb(string url, string data,WebHeaderCollection Header=null)
        {
            byte[] postData = Encoding.UTF8.GetBytes(data);
            WebClient webClient = new WebClient();
            if (Header != null)
                webClient.Headers = Header;
            byte[] responseData = webClient.UploadData(url, "POST", postData);
            webClient.Dispose();
            return Encoding.UTF8.GetString(responseData);
        }
      
        public static async Task HttpDownloadFileAsync(string url, string path)
        {
            HttpWebRequest hwr = WebRequest.Create(url) as HttpWebRequest;
            hwr.Accept="text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            hwr.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9");
            hwr.Headers.Add("Cache-Control", "max-age=0");
            hwr.KeepAlive = true;
            hwr.Referer = url;
            hwr.Headers.Add("Upgrade-Insecure-Requests", "1");
            hwr.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
            hwr.Timeout = 20000;
            HttpWebResponse response = await hwr.GetResponseAsync() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            Stream stream = new FileStream(path, FileMode.Create,FileAccess.ReadWrite);
            byte[] bArr = new byte[1024];
            int size = await responseStream.ReadAsync(bArr, 0, bArr.Length);
            while (size > 0)
            {
                await stream.WriteAsync(bArr, 0, size);
                size = await responseStream.ReadAsync(bArr, 0, bArr.Length);
            }
            stream.Close();
            responseStream.Close();
        }
        
        
    }
}
