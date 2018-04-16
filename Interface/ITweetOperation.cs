using System;
using System.Collections.Generic;

namespace ClientApplication.Interface
{
    public interface ITweetOperation
    {
        /// <summary>
        /// 
        /// </summary>
        string startTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string endTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, TweetModel> tweetData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        void GetAllTweets();

        /// <summary>
        /// 
        /// </summary>
        void WriteTweetDataToCSVFile();
    }
}
