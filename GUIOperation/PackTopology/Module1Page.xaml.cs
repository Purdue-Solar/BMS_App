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
using Windows.ApplicationModel.Store.Preview.InstallControl;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BMSManagerRebuilt.GUIOperation.PackTopology
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Module1Page : Page
    {
        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        static ILogger logger = factory.CreateLogger<MainWindow>();

        public static string TabName { get; set; }
        public static int offset;
        public static ushort setting { get; set; }
        public static ushort bit1 = 0x1;

        public Module1Page()
        {
            TabName = ModuleTabViewOperation.TabName;
            setting = default;
            logger.LogDebug("Setting = {setting}", setting);
            this.InitializeComponent();
        }

        public void UpdateSetting()
        {
            MainWindow.Module1Setting = setting;
            logger.LogDebug("Module 1: {module1}", MainWindow.Module1Setting);
        }

        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 11);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);

        }

        private void CheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 11);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 10);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox2_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 10);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox3_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 9);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox3_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 9);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox4_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 8);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox4_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 8);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox5_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 7);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox5_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 7);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox6_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 6);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox6_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 6);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox7_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 5);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox7_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 5);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox8_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 4);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox8_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 4);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox9_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 3);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox9_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 3);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox10_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 2);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox10_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 2);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox11_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 1);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox11_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 1);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox12_Checked(object sender, RoutedEventArgs e)
        {
            setting |= (ushort)(bit1 << 0);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void CheckBox12_Unchecked(object sender, RoutedEventArgs e)
        {
            setting &= (ushort)~(bit1 << 0);
            UpdateSetting();
            logger.LogDebug("Setting = {setting}", setting);
        }

        private void Module1CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Module1CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
