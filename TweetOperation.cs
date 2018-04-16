using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ClientApplication.Interface;
using System.Text.RegularExpressions;

namespace ClientApplication
{
    public class TweetOperation : ITweetOperation
    {
        /// <summary>
        /// Start time 
        /// </summary>
        public string startTime { get; set; }

        /// <summary>
        /// End time
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        /// Dictionary to keep track of tweets
        /// </summary>
        public Dictionary<string, TweetModel> tweetData { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sT"></param>
        /// <param name="eT"></param>
        public TweetOperation(string sT, string eT)
        {
            this.startTime = sT;
            this.endTime = eT;
            tweetData = new Dictionary<string, TweetModel>(); 
        }

        /// <summary>
        /// Get all tweets
        /// </summary>
        public void GetAllTweets()
        {
            var results = new List<TweetModel>();
            DateTime dtStartTime = Convert.ToDateTime(this.startTime);
            DateTime dtEndTime = Convert.ToDateTime(this.endTime);
            double thresholdTime = 0;

            try
            {
                do
                {
                    while ((dtStartTime < dtEndTime))
                    {
                        results = GetResponse(dtStartTime.ToString(),
                                                dtEndTime.ToString()).ToList();
                        if (results.Count() >= 100)
                        {
                            TimeSpan t1 = dtEndTime.Subtract(dtStartTime);
                            double midTime = t1.TotalSeconds / 2;
                            dtEndTime = dtStartTime.AddSeconds(midTime);
                        }
                        else
                        {
                            // Get threshold time for result count < 100
                            thresholdTime = dtEndTime.Subtract(dtStartTime).TotalSeconds;
                            break;
                        }
                    }

                    foreach (var result in results)
                    {
                        // Make sure unique entries
                        if (!tweetData.ContainsKey(result.id))
                        {
                            tweetData.Add(result.id, result);
                        }
                    }

                    dtStartTime = dtEndTime;

                    // Next call to REST API is with twice threshold to reduce call to API
                    dtEndTime = dtEndTime.AddSeconds(2 * thresholdTime);

                } while (dtStartTime < Convert.ToDateTime(this.endTime));
            }
            catch (Exception ex)
            {
                throw new Exception(
                    string.Format("GetAllTweets API fails with error {0}", ex.Message));
            }
        }

        /// <summary>
        /// Write data to CSV file
        /// </summary>
        public void WriteTweetDataToCSVFile()
        {
            try
            {
                string tweetDataFilePath = Path.GetTempPath() + Constants.TweetFileDataFolder;

                if (!File.Exists(tweetDataFilePath))
                    Directory.CreateDirectory(tweetDataFilePath);

                string csvdownloadPath =
                    Path.Combine(tweetDataFilePath,
                    string.Format("CSVFile_{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmss")));

                using (FileStream File_Stream =
                    new FileStream(csvdownloadPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter FileWriter = new StreamWriter(File_Stream))
                    {
                        foreach (var entry in tweetData)
                        {
                            // Write Data to CSV file
                            string[] str = { entry.Value.id, entry.Value.stamp, entry.Value.text };
                            FileWriter.Write(string.Format("{0},", str[0]));
                            FileWriter.Write(string.Format("{0},", str[1]));

                            // Trim line break and tabs from text
                            FileWriter.Write(string.Format("{0}",
                                    Regex.Replace(str[2].Trim(), @"\t|\n|\r", "")));
                            FileWriter.WriteLine();
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Total # of Tweets : - {0}", tweetData.Count);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("CSV file with Tweets for 2016-2017 is @ {0}", csvdownloadPath);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    string.Format("WriteDataToCSVFile API fails with error {0}", ex.Message));
            }
        }

        private IEnumerable<TweetModel> GetResponse(string startDate, string endDate)
        {
            string url = Constants.Url + string.Format("startDate={0}&endDate={1}",
                                                            startDate,
                                                            endDate);
            // Get Http response from REST api
            var response =
                HttpUtility.GetResponse(
                    url,
                    HttpUtility.HttpMethod.Get);

            // Deserialize tweet data
            var results = Utilties.DeSerializeObjectFromJson(response);

            return results;
        }
    }
}
