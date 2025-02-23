using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using ScottPlot.WinUI;
using ScottPlot;
using CsvHelper;
using Windows.Networking.Connectivity;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Windows.Storage.Pickers;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BMSManagerRebuilt.GUIOperation
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TableConfigPage : Page
    {
        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        static ILogger logger = factory.CreateLogger<MainWindow>();

        List<CSVRecord> CSVrecords = new List<CSVRecord>();
        public string CSVPath;

        public TableConfigPage()
        {
            this.InitializeComponent();
        }

        // Array Conversion

        List<int> Time = new List<int>();
        List<double> Current = new List<double>();
        List<double> Resistance = new List<double>();
        List<double> Voltage = new List<double>();

        private void convert2Array()
        {
            int index = 0;
            while (index < CSVrecords.Count())
            {
                Time.Add(CSVrecords[index].Time);
                Current.Add(CSVrecords[index].Current);
                Resistance.Add(CSVrecords[index].Resistance);
                Voltage.Add(CSVrecords[index].Voltage);
                index += 1;
            }
        }

        //CSV Wrapper;
        public void ReadCSV()
        {
            CSVProcessor.ReadCSV(CSVPath, out CSVrecords);
        }

        //Button Operation
        public async void ImportButton(object sender, RoutedEventArgs e)
        {
            logger.LogDebug("Import Button Clicked");
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            logger.LogDebug("picker Created");
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.FileTypeFilter.Add(".csv");
            logger.LogDebug("picker Configured");

            nint windowHandle = WindowNative.GetWindowHandle(App.m_window);
            InitializeWithWindow.Initialize(picker, windowHandle);

            Windows.Storage.StorageFile CSVFile = await picker.PickSingleFileAsync();

            if (CSVFile != null)
            {
                CSVPath = CSVFile.Path;
                logger.LogDebug("Imported {CSVPath}", CSVPath);
                ReadCSV();
                convert2Array();
                WinUIPlot.Plot.Axes.AutoScale();
                WinUIPlot.Refresh();
            }
        }
    }
}
