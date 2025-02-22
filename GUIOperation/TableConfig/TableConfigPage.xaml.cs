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

        public TableConfigPage()
        {
            this.InitializeComponent();
            WinUIPlot1.Plot.Add.Scatter(xs, ys);
            WinUIPlot1.Refresh();
        }
    }
}
