using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ClientApplication
{
    public static class Utilties
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static IEnumerable<TweetModel> DeSerializeObjectFromJson(string jsonData)
        {
            IEnumerable<TweetModel> deserializedObject;

            //Convert JSON Data to Object 
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonData)))
            {
                //Instantiate DataContractJsonSerializer and Cast data in memory stream to Object
                var ser = new DataContractJsonSerializer(typeof(IEnumerable<TweetModel>));
                memoryStream.Position = 0;
                deserializedObject = (IEnumerable<TweetModel>)ser.ReadObject(memoryStream);
            }

            return deserializedObject;
        }
        
    }
}
