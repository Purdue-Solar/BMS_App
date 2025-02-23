using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.System;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Extensions.Logging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BMSManagerRebuilt.GUIOperation.NewTabNameWindow
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModuleNewTab : Window
    {
        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        /// <summary>
        /// Command Line Debug Logger Variable (not save in logfile)
        /// </summary>
        ILogger logger = factory.CreateLogger<MainWindow>();
        public ModuleNewTab()
        {
            this.InitializeComponent();
            AppWindow.Resize(new Windows.Graphics.SizeInt32(650, 250));
            AppWindow.Move(new Windows.Graphics.PointInt32(650, 250));
        }

        public string TabName = default;

        public void EnterPressed(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                logger.LogDebug("Enter key pressed");
                TabName = ModuleNumber.Text;
            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    logger.LogDebug("Button is clicked");
        //    try
        //    {
        //        TabName = ((TextBlock)TabNames.SelectedValue).Text;
        //    }
        //    catch (System.NullReferenceException exception)
        //    {

        //    }
        //}
    }
}
