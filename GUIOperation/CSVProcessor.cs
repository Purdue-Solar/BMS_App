using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel.Store;

namespace BMSManagerRebuilt.GUIOperation
{
    public class CSVProcessor
    {

        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        static ILogger logger = factory.CreateLogger<MainWindow>();

        public static void ReadCSV(string path, out List<CSVRecord> record)
        {
            //Initialized output variables
            List<CSVRecord> tempRecord = new List<CSVRecord>();
            using (var Reader = new StreamReader(path)) //There is no encoding so do not add any encoder.
            using (var csv = new CsvReader(Reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                logger.LogDebug("Started Reading");
                while (csv.Read())
                {
                    logger.LogDebug("Reading");
                    var newRecord = new CSVRecord
                    {
                        Time = csv.GetField<int>("Time"),
                        Current = csv.GetField<double>("Current"),
                        Resistance = csv.GetField<double>("Resistance"),
                        Voltage = csv.GetField<double>("Voltage")
                    };
                    logger.LogDebug("Time is {time}", newRecord.Time);
                    logger.LogDebug("Current is {current}", newRecord.Current);
                    logger.LogDebug("Resitance is {resistance}", newRecord.Resistance);
                    logger.LogDebug("Voltage is {voltage}", newRecord.Voltage);
                    tempRecord.Add(newRecord);
                }
            }
            record = tempRecord;
            logger.LogDebug("Finish Reading record");
        }
    }

    public class CSVRecord
    {
        public int Time { get; set; }
        public double Current { get; set; }
        public double Resistance { get; set; }
        public double Voltage { get; set; }
    }
}
