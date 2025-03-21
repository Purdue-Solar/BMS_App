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
    public partial class VoltageResistanceConfigPage : Page
    {
        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        static ILogger logger = factory.CreateLogger<MainWindow>();

        public static string TabName;


        List<CSVRecord> CSVrecords = new List<CSVRecord>();
        public string CSVPath;

        public VoltageResistanceConfigPage()
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
                AddedScatter();
                WinUIPlot.Plot.Axes.AutoScale();
                WinUIPlot.Refresh();
            }
        }

        public void AddedScatter()
        {
            WinUIPlot.Plot.Add.Scatter(Voltage, Resistance);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinUIPlot.Refresh();
            logger.LogDebug("Refresh Button Clicked");
        }

        private void VoltageResCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.VoltageResTableActivate = true;
        }

        private void VoltageResCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.VoltageResTableActivate = false;
        }

        //GUI Config
        //Color Initilization
        SolidColorBrush greenBrush = new(Microsoft.UI.Colors.LightGreen);
        SolidColorBrush redBrush = new(Microsoft.UI.Colors.Red);

        private void VoltResLength_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainWindow.ReLength = int.Parse(BoxVoltResLength.Text);
                    logger.LogDebug("Store ReLength Value: {iso}", MainWindow.ReLength);
                    VoltResLengthInd.Text = "✓";
                    VoltResLengthInd.Foreground = greenBrush;
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

        private void VoltResLength_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltResLengthInd.Text = "X";
            VoltResLengthInd.Foreground = redBrush;
        }

        private void VoltResCurrents_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainWindow.ReCurrent = int.Parse(BoxVoltResCurrents.Text);
                    logger.LogDebug("Store ReCurrent Value: {iso}", MainWindow.ReCurrent);
                    VoltResCurrentsInd.Text = "✓";
                    VoltResCurrentsInd.Foreground = greenBrush;
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

        private void VoltResCurrents_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltResCurrentsInd.Text = "X";
            VoltResCurrentsInd.Foreground = redBrush;
        }

        private void VoltResStartVoltage_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainWindow.ReStartVoltage = int.Parse(BoxVoltResStartVoltage.Text);
                    logger.LogDebug("Store ReStartVoltage Value: {iso}", MainWindow.ReStartVoltage);
                    VoltResStartVoltageInd.Text = "✓";
                    VoltResStartVoltageInd.Foreground = greenBrush;
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

        private void VoltResStartVoltage_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltResStartVoltageInd.Text = "X";
            VoltResStartVoltageInd.Foreground = redBrush;
        }

        private void VoltResSteps_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainWindow.ReSteps = int.Parse(BoxVoltResSteps.Text);
                    logger.LogDebug("Store ReSteps Value: {iso}", MainWindow.ReSteps);
                    VoltResStepsInd.Text = "✓";
                    VoltResStepsInd.Foreground = greenBrush;
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

        private void VoltResSteps_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltResStepsInd.Text = "X";
            VoltResStepsInd.Foreground = redBrush;
        }
    }
}