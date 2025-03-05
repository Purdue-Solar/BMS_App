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
using BMSManagerRebuilt.GUIOperation.TableConfig;
using Microsoft.Extensions.Logging;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BMSManagerRebuilt.GUIOperation.PackTopology
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModulePages : Page
    {
        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        static ILogger logger = factory.CreateLogger<MainWindow>();

        public static string TabName { get; set; }
        public static int offset;
        public static Int16 setting { get; set; }

        public ModulePages()
        {
            TabName = ModuleTabViewOperation.TabName;
            setting = default;
            logger.LogDebug("Setting = {setting}", setting);
            ConfigureOffset();
            this.InitializeComponent();
        }

        public void ConfigureOffset()
        {
            if (TabName == "MODULE 2")
            {
                offset = 1;
            }
            else if (TabName == "MODULE 3")
            {
                offset = 2;
            }
            else
            {
                offset = 0;
            }
        }

        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            setting |= 0x1;
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1);
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            setting |= 0x1 << 1;
        }

        private void CheckBox2_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 1);
        }

        private void CheckBox3_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 2);
        }

        private void CheckBox3_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 2);
        }

        private void CheckBox4_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 3);
        }

        private void CheckBox4_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 3);
        }

        private void CheckBox5_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 4);
        }

        private void CheckBox5_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 4);
        }

        private void CheckBox6_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 5);
        }

        private void CheckBox6_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 5);
        }

        private void CheckBox7_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 6);
        }

        private void CheckBox7_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 6);
        }

        private void CheckBox8_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 7);
        }

        private void CheckBox8_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 7);
        }

        private void CheckBox9_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 8);
        }

        private void CheckBox9_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 8);
        }

        private void CheckBox10_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 9);
        }

        private void CheckBox10_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 9);
        }

        private void CheckBox11_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 10);
        }

        private void CheckBox11_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 10);
        }

        private void CheckBox12_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (0x1 << 11);
        }

        private void CheckBox12_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= ~(0x1 << 11);
        }


    }
}
