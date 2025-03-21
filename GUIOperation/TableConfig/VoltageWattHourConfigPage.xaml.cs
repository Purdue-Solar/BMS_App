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
using Windows.System;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using BMSManagerRebuilt.GUIOperation.TableConfig;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BMSManagerRebuilt.GUIOperation
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class VoltageWattHourConfigPage : Page
    {
        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        static ILogger logger = factory.CreateLogger<MainWindow>();

        public static string TabName;

        List<CSVRecord> CSVrecords = new List<CSVRecord>();
        public string CSVPath;

        public VoltageWattHourConfigPage()
        {
            this.InitializeComponent();
            TabName = TableTabViewOperation.TabName;
            TableTitle.Text = TabName;
            logger.LogDebug("TableView TabName = {TabName}", TableTabViewOperation.TabName);
            logger.LogDebug("Page initialized | TabName = {TabName}", TabName);
        }

        // Array Conversion
        List<int> Time = new List<int>();
        List<double> Current = new List<double>();
        public List<double> Resistance { get; set; } = new List<double>();
        public List<double> Voltage { get; set; } = new List<double>();
        public List<double> WattHour { get; set; } = new();


        private void convert2Array()
        {
            int index = 0;
            while (index < CSVrecords.Count())
            {
                Time.Add(CSVrecords[index].Time);
                Current.Add(CSVrecords[index].Current);
                Resistance.Add(CSVrecords[index].Resistance);
                Voltage.Add(CSVrecords[index].Voltage);
                WattHour.Add(Current[index] * Voltage[index] * Time[index] / 3600);
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
                AddedScatter();
                WinUIPlot.Plot.Axes.AutoScale();
                WinUIPlot.Refresh();
            }
        }

        public void AddedScatter()
        {
            WinUIPlot.Plot.Add.Scatter(Voltage, WattHour);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinUIPlot.Refresh();
        }

        private void VoltageWattCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.VoltageWatTableActivate = true;
        }

        private void VoltageWattCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.VoltageWatTableActivate = false;
        }

        //GUI Config
        //Color Initilization
        SolidColorBrush greenBrush = new(Microsoft.UI.Colors.LightGreen);
        SolidColorBrush redBrush = new(Microsoft.UI.Colors.Red);

        private void VoltWattLength_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainWindow.WattLength = int.Parse(BoxVoltWattLength.Text);
                    logger.LogDebug("Store WattLength Value: {iso}", MainWindow.WattLength);
                    VoltWattLengthInd.Text = "✓";
                    VoltWattLengthInd.Foreground = greenBrush;
                }
                catch (FormatException exception)
                {
                    logger.LogDebug("Can't convert to int");
                }
                catch (OverflowException oexception)
                {
                    logger.LogDebug("Overflow");
                }
            }
        }

        private void VoltWattLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltWattLengthInd.Text = "X";
            VoltWattLengthInd.Foreground = redBrush;
        }

        private void VoltWattCurrents_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainWindow.WattCurrent = int.Parse(BoxVoltWattCurrents.Text);
                    logger.LogDebug("Store WattCurrent Value: {iso}", MainWindow.WattCurrent);
                    VoltWattCurrentsInd.Text = "✓";
                    VoltWattCurrentsInd.Foreground = greenBrush;
                }
                catch (FormatException exception)
                {
                    logger.LogDebug("Can't convert to int");
                }
                catch (OverflowException oexception)
                {
                    logger.LogDebug("Overflow");
                }
            }
        }

        private void VoltWattCurrents_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltWattCurrentsInd.Text = "X";
            VoltWattCurrentsInd.Foreground = redBrush;
        }

        private void VoltWattStartVoltage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainWindow.WattStartVoltage = int.Parse(BoxVoltWattStartVoltage.Text);
                    logger.LogDebug("Store WattStartVoltage Value: {iso}", MainWindow.WattStartVoltage);
                    VoltWattStartVoltageInd.Text = "✓";
                    VoltWattStartVoltageInd.Foreground = greenBrush;
                }
                catch (FormatException exception)
                {
                    logger.LogDebug("Can't convert to int");
                }
                catch (OverflowException oexception)
                {
                    logger.LogDebug("Overflow");
                }
            }
        }

        private void VoltWattStartVoltage_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltWattStartVoltageInd.Text = "X";
            VoltWattStartVoltageInd.Foreground = redBrush;
        }

        private void VoltWattSteps_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainWindow.WattSteps = int.Parse(BoxVoltWattSteps.Text);
                    logger.LogDebug("Store WattSteps Value: {iso}", MainWindow.WattSteps);
                    VoltWattStepsInd.Text = "✓";
                    VoltWattStepsInd.Foreground = greenBrush;
                }
                catch (FormatException exception)
                {
                    logger.LogDebug("Can't convert to int");
                }
                catch (OverflowException oexception)
                {
                    logger.LogDebug("Overflow");
                }
            }
        }

        private void VoltWattSteps_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltWattStepsInd.Text = "X";
            VoltWattStepsInd.Foreground = redBrush;
        }
    }
}