using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Passing default parameters for 2016 and 2017 data
            // Can be Configurable passed to args
            string startDate = "2015-12-31"; //args[0];
            string endDate = "2018-01-01"; //args[1];

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Retrieving TWEETS from https://badapi.iqvia.io......");

            TweetOperation tweetOp = new TweetOperation(startDate, endDate);
            
            // Get all tweets
            tweetOp.GetAllTweets();

            // Write tweets to CSV file
            tweetOp.WriteTweetDataToCSVFile();

            Console.ReadLine();
        }
        
    }
}
