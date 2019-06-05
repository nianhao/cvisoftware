using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace cvisoftware
{
    class delAll
    {
        public static string delAllConfig()
        {
            var delURLS = interfaceURL.getAllDelURL();
            string res = "";
            foreach(string key in delURLS.Keys)
            {
                string tmp = string.Format("\r\n****************\r\n{0}\r\n{1}\r\n*******************\n", key.Split('U')[0], Get(delURLS[key]as string ));
                res += tmp;
                Trace.WriteLine(tmp);
            }
            return res;
        }
        private static string Get(string url)
        {
            // throw new NotImplementedException();
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 200;
            //创建httprequest对象
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.Timeout = 10000;
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                return responseString;
            }
            catch (Exception e)
            {

                //throw;
                Trace.WriteLine(e.StackTrace);
                Trace.WriteLine(e.Message);
                return e.Message;
            }
        }
    }
}
