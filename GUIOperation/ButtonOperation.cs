using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot.WinUI;

namespace BMSManagerRebuilt.GUIOperation
{
    class ButtonOperation
    {
        /// <summary>
        /// Configure Button Operation(Interrupts)
        /// </summary>
        /// <param name = "sender" > Will contained data about the XAML processor that triggered it</param>
        /// <param name = "e" ></ param >
        public static void ExportData(object sender, string text)
        {

        }

        /// <summary>
        /// Transition Button Operation (Interrupts)
        /// </summary>
        /// <param name="sender">Will contained data about the XAML processor that triggered it</param>
        /// <param name="e"></param>
        public static void ImportData(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="plot"></param>
        public static void RefreshGraph(object sender, WinUIPlot plot)
        {
            plot.Refresh();
        }
    }
}
