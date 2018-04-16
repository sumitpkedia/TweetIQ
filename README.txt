Client Application Details: - 

1>Application is written in C#
2>Application can be run directly from ClientApplication.exe.
3>Application has start and end date between 2016-2017
4>Results will be displayed as : - 
	a>Retrieving tweets from https://badapi.iqvia.io
	b>Total # of Tweets : - 11749
	c>CSV file with Tweets for 2016-2017 is 
		@ C:\Users\sumeetk\AppData\Local\Temp\\TweetData\CSVFile_20180415151058.csv

Design : - 3 approach (Implemented first and 2nd approach - 2nd is better performant and shared)
1> Keep dividing time in half everytime unless we get data less than 100 count
	a> Start and end itme - Get response between start and end date
	b> Keep dividing until we hit results count < 100
2> Keep dividing time in half and then find threshold 
	a> Start and end time - Get response between start and end date
	b> Keep dividing until we hit results count < 100
	c> Then build threshold and multiply threshold time by 2
	d> Try to find tweets count less than 100
	e> This approach is FASTER than above - as less calls will be made as in first approach.
3> Keep dividing time and then get data less than 100 from start time and also from end time.
	a> Start and end time - get response data
	b> Keep dividing unless we hit data less than 100
	c> Then run two call to REST from start and end time in parallel
	d> This will be multithreaded application running in parallel making calls to REST 

Assumptions: - 
	a> Date range is predefined. But can be configured.
	
Improvements (Performance/scale/Globalazation/Localization): - 
	a> Resource files can be used for strings/messages for localization and globalizaition support
	b> Logger files would help track history/activities
	c> Used dictionary to keep track of unique tweet id and its content. For bulk tweets 
	   we can directly write in csv or data base.
	d> Also we can check csv file size for many tweets - once size reached , data bene written
	   in new files.


