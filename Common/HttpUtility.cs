using System;
using System.IO;
using System.Net;
using System.Text;

namespace ClientApplication
{
    public static class HttpUtility
    {
        /// <summary>
        /// 
        /// </summary>
        public enum HttpMethod
        {
            Get,
            Set,
            Post,
            Delete,
            Put
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string GetResponse(string url, HttpMethod method)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new Exception(string.Format("Url is empty/Invalid."));
            }

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method.ToString().ToUpperInvariant();
            request.ContentType = Constants.AppContentType;
            var response = request.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding(Constants.EncodingUtf8);
            var responseStream = response.GetResponseStream();
            var content = string.Empty;
            if (responseStream != null)
            {
                using (var streamReader = new StreamReader(responseStream, enc))
                {
                    content = streamReader.ReadToEnd();
                }
            }

            response.Close();
            return content;
        }
    }
}
