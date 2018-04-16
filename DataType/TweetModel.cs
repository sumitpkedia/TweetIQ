using System.Runtime.Serialization;
using ClientApplication.Interface;

namespace ClientApplication
{
    [DataContract]
    public class TweetModel : ITweetModel
    {
        [DataMember]
        public string id { get; set;}

        [DataMember]
        public string stamp { get; set; }

        [DataMember]
        public string text { get; set; }
    }
}
