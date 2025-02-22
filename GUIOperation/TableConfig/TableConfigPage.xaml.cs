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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BMSManagerRebuilt.GUIOperation
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TableConfigPage : Page
    {
        double[] xs = { 1, 2, 3, 4, 5 };
        double[] ys = { 1, 4, 9, 16, 25 };

        List<CSVRecord> CSVrecords = new List<CSVRecord>();
        public string CSVPath = "D:\\Projects\\App\\BMS-Manager-App\\Battery3.0.2Test.csv";

        public TableConfigPage()
        {
            this.InitializeComponent();
            ReadCSV();
            convert2Array();
            WinUIPlot.Plot.Add.Scatter(Voltage, Resistance);
            WinUIPlot.Refresh();
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
    }
}
