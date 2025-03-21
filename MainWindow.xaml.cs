using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Input;
using Windows.Security.Cryptography.Certificates;
using Windows.Media.Devices;
using Windows.UI.Core;
using Windows.System;
using Microsoft.UI.Xaml.Automation.Peers;
using Windows.Devices.SerialCommunication;
using Windows.Devices.Enumeration;
using System.ComponentModel;
using System.Threading;
using Microsoft.Extensions.Logging;
using CsvHelper;
using System.Runtime.InteropServices;
using PSR.BMS.Configuration;
using ScottPlot.WinUI;
using ScottPlot;
using BMSManagerRebuilt.GUIOperation.TableConfig;
using BMSManagerRebuilt.GUIOperation;
using Microsoft.Windows.ApplicationModel.DynamicDependency;
using System.Runtime.Serialization.Formatters.Binary;
using System.Buffers.Binary;
using System.Globalization;
using System.Data;
using Windows.ApplicationModel.Contacts;
using PSR.BMS.Configuration.Tables;
using Windows.UI.WebUI;
using Windows.UI;
using Microsoft.UI;
using ABI.Windows.UI;
using WinRT.Interop;
using System.ComponentModel.DataAnnotations;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BMSManagerRebuilt
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {

        public static PSR.BMS.Configuration.ConfigurationWriter configuration = new();

        //Ioslation Limits
        public static bool IsoActivate { get; set; }
        public static int IsolationRes { get; set; }
        public static int IsolationResWarning { get; set; }

        //Current Warning
        public static bool CurrentWarnActivate { get; set; }
        public static int CurrentWarnMaxCharging { get; set; }
        public static int CurrentWarnMaxChargingTemp { get; set; }
        public static int CurrentWarnMaxDischarging { get; set; }
        public static int CurrentWarnMaxDischargingTemp { get; set; }

        //Current Limits
        public static bool CurrentLimitsActivate { get; set; }
        public static int CurrentMaxCharging { get; set; }
        public static int CurrentMaxChargingTemp { get; set; }
        public static int CurrentMaxDischarging { get; set; }
        public static int CurrentMaxDischargingTemp { get; set; }
        public static int CurrentMaxPulse { get; set; }
        public static int CurrentMaxPulseDuration { get; set; }

        //Voltage Limits
        public static bool VoltageActivate { get; set; }
        public static int VoltageMaxCell { get; set; }
        public static int VoltageMinCell { get; set; }
        public static int VoltageMaxCellCharging { get; set; }
        public static int VoltageMaxPack { get; set; }
        public static int VoltageMinPack { get; set; }
        public static int VoltageMaxPackCharging { get; set; }

        //Voltage Warning
        public static bool VoltageWarnActivate { get; set; }
        public static int VoltageMaxCellWarning { get; set; }
        public static int VoltageMinCellWarning { get; set; }
        public static int VoltageMaxCellChargingWarning { get; set; }
        public static int VoltageMaxPackWarning { get; set; }
        public static int VoltageMinPackWarning { get; set; }
        public static int VoltageMaxPackChargingWarning { get; set; }

        //PreCharge Delay
        public static bool PreChargeDelayActivate { get; set; }
        public static float PreChargeDelay { get; set; }
        //Contactor PWM Frequency
        public static bool PWMActivate { get; set; }
        public static uint ContactorPwmFrequency { get; set; }

        //PreCharge
        public static bool PreChargeActivate { get; set; }
        public static PSR.BMS.Configuration.Tables.ContactorConfiguration.Contactor.StateFlags PreChargeFlag { get; set; }
        public static byte PreChargeIndex { get; set; }
        public static ushort PreChargeHoldDelay { get; set; }
        public static float PreChargeHoldDuty { get; set; }
        public static float PreChargePullInDuty { get; set; }


        //Charge
        public static bool ChargeActivate { get; set; }
        public static PSR.BMS.Configuration.Tables.ContactorConfiguration.Contactor.StateFlags ChargeFlag { get; set; }
        public static byte ChargeIndex { get; set; }
        public static ushort ChargeHoldDelay { get; set; }
        public static float ChargeHoldDuty { get; set; }
        public static float ChargePullInDuty { get; set; }


        //Main Low Side
        public static bool MainLowSideActivate { get; set; }
        public static PSR.BMS.Configuration.Tables.ContactorConfiguration.Contactor.StateFlags MainLowFlag { get; set; }
        public static byte MainLowIndex { get; set; }
        public static ushort MainLowHoldDelay { get; set; }
        public static float MainLowHoldDuty { get; set; }
        public static float MainLowPullInDuty { get; set; }

        //Main High Side
        public static bool MainHighSideActivate { get; set; }
        public static PSR.BMS.Configuration.Tables.ContactorConfiguration.Contactor.StateFlags MainHighFlag { get; set; }
        public static byte MainHighIndex { get; set; }
        public static ushort MainHighHoldDelay { get; set; }
        public static float MainHighHoldDuty { get; set; }
        public static float MainHighPullInDuty { get; set; }


        //Voltage Watt Hour Table
        public static bool VoltageWatTableActivate { get; set; }
        public static int WattLength { get; set; }
        public static int WattCurrent { get; set; }
        public static int WattStartVoltage { get; set; }
        public static int WattSteps { get; set; }
        public static float[] WattHoursArray { get; set; }

        //Voltage Resistance Table
        public static bool VoltageResTableActivate { get; set; }
        public static int ReLength { get; set; }
        public static int ReCurrent { get; set; }
        public static int ReStartVoltage { get; set; }
        public static int ReSteps { get; set; }
        public static float[] ResitanceArray { get; set; }

        //Pack topology
        public static bool PackTopoActivate { get; set; }
        public static byte ParallelCells { get; set; }
        public static byte SeriesCells { get; set; }
        public static byte Reserved { get; set; }
        public static byte CellGroups { get; set; }

        //Module 1
        public static bool Module1Activate { get; set; }
        public static ushort Module1Setting { get; set; }
        

        //Module 2
        public static bool Module2Activate { get; set; }
        public static ushort Module2Setting { get; set; }
        
        //Module 3
        public static bool Module3Activate { get; set; }
        public static ushort Module3Setting { get; set; }

        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        ILogger logger = factory.CreateLogger<MainWindow>();

        public MainWindow()
        {
            this.InitializeComponent();

            //Deactivate all section
            IsoActivate = false;
            CurrentWarnActivate = false;
            CurrentLimitsActivate = false;
            VoltageActivate = false;
            VoltageWarnActivate = false;
            MainHighSideActivate = false;
            MainLowSideActivate = false;
            ChargeActivate = false;
            PreChargeActivate = false;
            PreChargeDelayActivate = false;
            PWMActivate = false;
            PackTopoActivate = false;
            Module1Activate = false;
            Module2Activate = false;
            Module3Activate = false;
        }

        //Button Operation 
        public void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            //Isolation Limits
            if (IsoActivate)
            {
                logger.LogDebug("Isolation Config Added");
                PSR.BMS.Configuration.Tables.IsolationLimits isolation = new();
                isolation.IsolationResistance = IsolationRes;
                isolation.IsolationResistanceWarning = IsolationResWarning;
                configuration.Add(isolation);
            }

            //Current Limits
            if (CurrentLimitsActivate)
            {
                logger.LogDebug("Current Limits Config Added");
                PSR.BMS.Configuration.Tables.CurrentLimits currentLimits = new();
                currentLimits.MaxCharging = CurrentMaxCharging;
                currentLimits.MaxChargingTemperature = CurrentMaxChargingTemp;
                currentLimits.MaxDischarging = CurrentMaxDischarging;
                currentLimits.MaxDischargingTemperature = CurrentMaxDischargingTemp;
                currentLimits.MaxPulseDischarge = CurrentMaxPulse;
                currentLimits.MaxPulseDischargeDuration = CurrentMaxPulseDuration;
                configuration.Add(currentLimits);
            }

            //Current Warning Limits
            if (CurrentWarnActivate)
            {
                logger.LogDebug("Current Warning Config Added");
                PSR.BMS.Configuration.Tables.CurrentWarningLimits currentWarningLimits = new();
                currentWarningLimits.MaxChargingWarning = CurrentWarnMaxCharging;
                currentWarningLimits.MaxChargingTemperatureWarning = CurrentWarnMaxChargingTemp;
                currentWarningLimits.MaxDischargingWarning = CurrentWarnMaxDischarging;
                currentWarningLimits.MaxDischargingTemperatureWarning = CurrentWarnMaxDischargingTemp;
                configuration.Add(currentWarningLimits);
            }    

            //Voltage Warning Limits
            if (VoltageWarnActivate)
            {
                logger.LogDebug("Voltage Warning Config Added");
                PSR.BMS.Configuration.Tables.VoltageWarningLimits voltageWarningLimits = new();
                voltageWarningLimits.MaxCellWarning = VoltageMaxCellWarning;
                voltageWarningLimits.MinCellWarning = VoltageMinCellWarning;
                voltageWarningLimits.MaxCellWarningCharging = VoltageMaxCellChargingWarning;
                voltageWarningLimits.MaxPackWarning = VoltageMaxPackWarning;
                voltageWarningLimits.MinPackWarning = VoltageMinPackWarning;
                voltageWarningLimits.MaxPackWarningCharging = VoltageMaxPackCharging;
                configuration.Add(voltageWarningLimits);
            }

            //Voltage Limits
            if (VoltageActivate)
            {
                logger.LogDebug("Voltage Limits Config Added");
                PSR.BMS.Configuration.Tables.VoltageLimits voltageLimits = new();
                voltageLimits.MaxCellVoltage = VoltageMaxCell;
                voltageLimits.MinCellVoltage = VoltageMinCell;
                voltageLimits.MaxCellVoltageCharging = VoltageMaxCellCharging;
                voltageLimits.MaxPackVoltage = VoltageMaxPack;
                voltageLimits.MinPackVoltage = VoltageMinPack;
                voltageLimits.MaxPackVoltageCharging = VoltageMaxPackCharging;
                configuration.Add(voltageLimits);
            }

            //Contactor Configuration
            PSR.BMS.Configuration.Tables.ContactorConfiguration contactor = new();
            if (MainHighSideActivate)
            {
                logger.LogDebug("Main High Side Contactor Config Added");
                int writtenLow = 0;
                contactor.MainLowSide.TryWrite([MainLowIndex, (byte)MainLowFlag, (byte)MainLowHoldDelay, (byte)MainLowHoldDuty, (byte)MainLowPullInDuty], out writtenLow);
            }

            if (MainLowSideActivate)
            {
                logger.LogDebug("Main Low Side Contactor Config Added");
                int writtenHigh = 0;
                contactor.MainHighSide.TryWrite([MainHighIndex, (byte)MainHighFlag, (byte)MainHighHoldDelay, (byte)MainHighHoldDuty, (byte)MainHighPullInDuty], out writtenHigh);
            }

            if (ChargeActivate)
            {
                logger.LogDebug("Charging Contactor Config Added");
                int writtenCharge = 0;
                contactor.Charge.TryWrite([ChargeIndex, (byte)ChargeFlag, (byte)ChargeHoldDelay, (byte)ChargeHoldDuty, (byte)ChargePullInDuty], out writtenCharge);
            }

            if (PreChargeActivate)
            {
                logger.LogDebug("Precharging Contactor Config Added");
                int writtenPrecharge = 0;
                contactor.Precharge.TryWrite([PreChargeIndex, (byte)PreChargeFlag, (byte)PreChargeHoldDelay, (byte)PreChargeHoldDuty, (byte)PreChargePullInDuty], out writtenPrecharge);
            }

            if (PreChargeDelayActivate)
            {
                logger.LogDebug("Precharging Delay Config Added");
                contactor.PrechargeDelay = PreChargeDelay;
            }
            
            if (PWMActivate)
            {
                logger.LogDebug("Contactor PWM Frequency Config Added");
                contactor.ContactorPwmFrequency = ContactorPwmFrequency;
            }

            if (MainHighSideActivate || MainLowSideActivate || ChargeActivate || PreChargeActivate || PreChargeDelayActivate || PWMActivate)
            {
                configuration.Add(contactor);
            }


            //Voltage Resistance Characterization
            if (VoltageResTableActivate)
            {
                logger.LogDebug("Voltage Resistance Table Config Added");
                PSR.BMS.Configuration.Tables.VoltageResistanceCharacterization voltReChar = new();
                voltReChar.Length = ReLength;
                voltReChar.Current = ReCurrent;
                voltReChar.StartVoltage = ReStartVoltage;
                voltReChar.Step = ReSteps;
                voltReChar.Resistance = ResitanceArray; //This is an array
                configuration.Add(voltReChar);
            }

            //Voltage Watt Hour Characterization
            if (VoltageWatTableActivate)
            {
                logger.LogDebug("Voltage Watt Table Config Added");
                PSR.BMS.Configuration.Tables.VoltageWattHourCharacterization voltWattChar = new();
                voltWattChar.Length = WattLength;
                voltWattChar.Current = WattCurrent;
                voltWattChar.StartVoltage = WattStartVoltage;
                voltWattChar.Step = WattSteps;
                voltWattChar.WattHours = WattHoursArray; //This is an array
                configuration.Add(voltWattChar);
            }

            //Pack topology
            PSR.BMS.Configuration.Tables.PackTopology pack = new();
            if (Module1Activate)
            {
                logger.LogDebug("Module 1 Config Added");
                pack.ActivatedCells[0] = Module1Setting;
            }
            if (Module2Activate)
            {
                logger.LogDebug("Module 2 Config Added");
                pack.ActivatedCells[1] = Module2Setting;
            }
            if (Module3Activate)
            {
                logger.LogDebug("Module 3 Config Added");
                pack.ActivatedCells[2] = Module3Setting;
            }
            if (PackTopoActivate)
            {
                logger.LogDebug("Pack Topology Config Added");
                pack.CellGroups = CellGroups;
                pack.Parallel = ParallelCells;
                pack.Series = SeriesCells;
                pack.Reserved = Reserved;
                configuration.Add(pack);
            }

            //Converting To Byte Array
            int written = 0;
            byte[] configurationArray = ToByteArray(configuration, out written);
            logger.LogDebug("configuration byte array generated");
            //Write to file;
            WriteFile(configurationArray);
            logger.LogDebug("Written to File");
        }

        public byte[] ToByteArray(PSR.BMS.Configuration.ConfigurationWriter configuration, out int written)
        {
            byte[] configurationArray = new byte[configuration.GetSize()];
            configuration.TryWrite(configurationArray, out written);
            return configurationArray;
        }

        //Write To File
        public async void WriteFile(byte[] configurationArray)
        {
            string dataTimeRep = DateTime.Now.Month.ToString() + '-' + DateTime.Now.Day.ToString() + '-' + DateTime.Now.Year.ToString() + '_' + DateTime.Now.Hour.ToString() + '-' + DateTime.Now.Minute.ToString() + '-' + DateTime.Now.Second.ToString();

            logger.LogDebug("filePath: {dataTimeRep}", dataTimeRep);
            var picker = new Windows.Storage.Pickers.FolderPicker();
            nint windowHandle = WindowNative.GetWindowHandle(App.m_window);
            InitializeWithWindow.Initialize(picker, windowHandle);
            Windows.Storage.StorageFolder Folder = await picker.PickSingleFolderAsync();
            
            if (Folder != null)
            {
                string filePath = Folder.Path + "\\configOn_" + dataTimeRep;
                logger.LogDebug("filePath: {filePath}", filePath);
                BinaryWriter binWriter = new(File.Create(filePath));
                binWriter.Flush();
                binWriter.Write(configurationArray);
                binWriter.Close();
            }
        }

        //Wrappers for Tab View Operation

        public void TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            TableTabViewOperation.Tabs_CloseButtonClick(sender, args);
        }

        public async void TableTabAddRequested(TabView sender, object args)
        {
            await TableTabViewOperation.TableTabs_AddButtonClick(sender, args);
        }

        public async void ModuleTabAddRequested(TabView sender, object args)
        {
            await ModuleTabViewOperation.ModuleTabs_AddButtonClick(sender, args);
        }

        //GUI Config
        //Color Initilization
        SolidColorBrush greenBrush = new(Microsoft.UI.Colors.LightGreen);
        SolidColorBrush redBrush = new(Microsoft.UI.Colors.Red);

        //Isolation Limits
        private void IsoRes_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    IsolationRes = int.Parse(BoxIsoRes.Text);
                    logger.LogDebug("Store IsolationRes Value: {iso}", IsolationRes);
                    IsoResInd.Text = "✓";
                    IsoResInd.Foreground = greenBrush;
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

        private void IsoRes_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsoResInd.Text = "X";
            IsoResInd.Foreground = redBrush;
        }

        private void IsoResWarn_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    IsolationResWarning = int.Parse(BoxIsoResWarn.Text);
                    logger.LogDebug("Store IsolationResWarning Value: {iso}", IsolationResWarning);
                    IsoResWarnInd.Text = "✓";
                    IsoResWarnInd.Foreground = greenBrush;
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

        private void IsoResWarn_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsoResWarnInd.Text = "X";
            IsoResWarnInd.Foreground = redBrush;
        }

        //Current Warning Limits
        private void CurrentWarnMaxCharge_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentWarnMaxCharging = int.Parse(BoxCurrentWarnMaxCharge.Text);
                    logger.LogDebug("Store CurrentWarnMaxCharging Value: {iso}", CurrentWarnMaxCharging);
                    CurrentWarnMaxChargeInd.Text = "✓";
                    CurrentWarnMaxChargeInd.Foreground = greenBrush;
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

        private void CurrentWarnMaxCharge_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentWarnMaxChargeInd.Text = "X";
            CurrentWarnMaxChargeInd.Foreground = redBrush;
        }

        private void CurrentWarnMaxChargeTemp_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentWarnMaxChargingTemp = int.Parse(BoxCurrentWarnMaxChargeTemp.Text);
                    logger.LogDebug("Store CurrentWarnMaxChargingTemp Value: {iso}", CurrentWarnMaxChargingTemp);
                    CurrentWarnMaxChargeTempInd.Text = "✓";
                    CurrentWarnMaxChargeTempInd.Foreground = greenBrush;
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

        private void CurrentWarnMaxChargeTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentWarnMaxChargeTempInd.Text = "X";
            CurrentWarnMaxChargeTempInd.Foreground = redBrush;
        }

        private void CurrentWarnMaxDischarge_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentWarnMaxDischarging = int.Parse(BoxCurrentWarnMaxDischarge.Text);
                    logger.LogDebug("Store CurrentWarnMaxDischarging Value: {iso}", CurrentWarnMaxDischarging);
                    CurrentWarnMaxDischargeInd.Text = "✓";
                    CurrentWarnMaxDischargeInd.Foreground = greenBrush;
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

        private void CurrentWarnMaxDischarge_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentWarnMaxDischargeInd.Text = "X";
            CurrentWarnMaxDischargeInd.Foreground = redBrush;
        }

        private void CurrentWarnMaxDischargeTemp_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentWarnMaxDischargingTemp = int.Parse(BoxCurrentWarnMaxDischargeTemp.Text);
                    logger.LogDebug("Store CurrentWarnMaxDischargingTemp Value: {iso}", CurrentWarnMaxDischargingTemp);
                    CurrentWarnMaxDischargeTempInd.Text = "✓";
                    CurrentWarnMaxDischargeTempInd.Foreground = greenBrush;
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

        private void CurrentWarnMaxDischargeTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentWarnMaxDischargeTempInd.Text = "X";
            CurrentWarnMaxDischargeTempInd.Foreground = redBrush;
        }


        //Current Limits Operations
        private void CurrentMaxCharge_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentMaxCharging = int.Parse(BoxCurrentMaxCharge.Text);
                    logger.LogDebug("Store CurrentMaxCharging Value: {iso}", CurrentMaxCharging);
                    CurrentMaxChargeInd.Text = "✓";
                    CurrentMaxChargeInd.Foreground = greenBrush;
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

        private void CurrentMaxCharge_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentMaxChargeInd.Text = "X";
            CurrentMaxChargeInd.Foreground = redBrush;
        }

        private void CurrentMaxChargeTemp_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentMaxChargingTemp = int.Parse(BoxCurrentMaxChargeTemp.Text);
                    logger.LogDebug("Store CurrentMaxChargingTemp Value: {iso}", CurrentMaxChargingTemp);
                    CurrentMaxChargeTempInd.Text = "✓";
                    CurrentMaxChargeTempInd.Foreground = greenBrush;
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

        private void CurrentMaxChargeTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentMaxChargeTempInd.Text = "X";
            CurrentMaxChargeTempInd.Foreground = redBrush;
        }

        private void CurrentMaxDischarge_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentMaxDischarging = int.Parse(BoxCurrentMaxDischarge.Text);
                    logger.LogDebug("Store CurrentMaxDischarging Value: {iso}", CurrentMaxDischarging);
                    CurrentMaxDischargeInd.Text = "✓";
                    CurrentMaxDischargeInd.Foreground = greenBrush;
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

        private void CurrentMaxDischarge_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentMaxDischargeInd.Text = "X";
            CurrentMaxDischargeInd.Foreground = redBrush;
        }

        private void CurrentMaxDischargeTemp_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentMaxDischargingTemp = int.Parse(BoxCurrentMaxDischargeTemp.Text);
                    logger.LogDebug("Store CurrentMaxDischargingTemp Value: {iso}", CurrentMaxDischargingTemp);
                    CurrentMaxDischargeTempInd.Text = "✓";
                    CurrentMaxDischargeTempInd.Foreground = greenBrush;
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

        private void CurrentMaxDischargeTemp_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentMaxDischargeTempInd.Text = "X";
            CurrentMaxDischargeTempInd.Foreground = redBrush;
        }

        private void CurrentMaxPulse_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentMaxPulse = int.Parse(BoxCurrentMaxPulse.Text);
                    logger.LogDebug("Store CurrentMaxPulse Value: {iso}", CurrentMaxPulse);
                    CurrentMaxPulseInd.Text = "✓";
                    CurrentMaxPulseInd.Foreground = greenBrush;
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

        private void CurrentMaxPulse_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentMaxPulseInd.Text = "X";
            CurrentMaxPulseInd.Foreground = redBrush;
        }

        private void CurrentMaxPulseDuration_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CurrentMaxPulseDuration = int.Parse(BoxCurrentMaxPulseDuration.Text);
                    logger.LogDebug("Store CurrentMaxPulseDuration Value: {iso}", CurrentMaxPulseDuration);
                    CurrentMaxPulseDurationInd.Text = "✓";
                    CurrentMaxPulseDurationInd.Foreground = greenBrush;
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

        private void CurrentMaxPulseDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            CurrentMaxPulseDurationInd.Text = "X";
            CurrentMaxPulseDurationInd.Foreground = redBrush;
        }

        private void VoltageMaxCell_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMaxCell = int.Parse(BoxVoltageMaxCell.Text);
                    logger.LogDebug("Store VoltageMaxCell Value: {iso}", VoltageMaxCell);
                    VoltageMaxCellInd.Text = "✓";
                    VoltageMaxCellInd.Foreground = greenBrush;
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

        private void VoltageMaxCell_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageMaxCellInd.Text = "X";
            VoltageMaxCellInd.Foreground = redBrush;
        }

        private void VoltageMinCell_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMinCell = int.Parse(BoxVoltageMinCell.Text);
                    logger.LogDebug("Store VoltageMinCell Value: {iso}", VoltageMinCell);
                    VoltageMinCellInd.Text = "✓";
                    VoltageMinCellInd.Foreground = greenBrush;
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

        private void VoltageMinCell_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageMinCellInd.Text = "X";
            VoltageMinCellInd.Foreground = redBrush;
        }

        private void VoltageMaxCellCharge_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMaxCellCharging = int.Parse(BoxVoltageMaxCellCharge.Text);
                    logger.LogDebug("Store VoltageMaxCellCharging Value: {iso}", VoltageMaxCellCharging);
                    VoltageMaxCellChargeInd.Text = "✓";
                    VoltageMaxCellChargeInd.Foreground = greenBrush;
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

        private void VoltageMaxCellCharge_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageMaxCellChargeInd.Text = "X";
            VoltageMaxCellChargeInd.Foreground = redBrush;
        }

        private void VoltageMaxPack_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMaxPack = int.Parse(BoxVoltageMaxPack.Text);
                    logger.LogDebug("Store VoltageMaxPack Value: {iso}", VoltageMaxPack);
                    VoltageMaxPackInd.Text = "✓";
                    VoltageMaxPackInd.Foreground = greenBrush;
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

        private void VoltageMaxPack_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageMaxPackInd.Text = "X";
            VoltageMaxPackInd.Foreground = redBrush;
        }

        private void VoltageMinPack_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMinPack = int.Parse(BoxVoltageMinPack.Text);
                    logger.LogDebug("Store VoltageMinPack Value: {iso}", VoltageMinPack);
                    VoltageMinPackInd.Text = "✓";
                    VoltageMinPackInd.Foreground = greenBrush;
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

        private void VoltageMinPack_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageMinPackInd.Text = "X";
            VoltageMinPackInd.Foreground = redBrush;
        }

        private void VoltageMaxPackCharge_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMaxPackCharging = int.Parse(BoxVoltageMaxPackCharge.Text);
                    logger.LogDebug("Store VoltageMaxPackCharging Value: {iso}", VoltageMaxPackCharging);
                    VoltageMaxPackChargeInd.Text = "✓";
                    VoltageMaxPackChargeInd.Foreground = greenBrush;
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

        private void VoltageMaxPackCharge_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageMaxPackChargeInd.Text = "X";
            VoltageMaxPackChargeInd.Foreground = redBrush;
        }

        private void VoltageWarnMaxCell_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMaxCellWarning = int.Parse(BoxVoltageWarnMaxCell.Text);
                    logger.LogDebug("Store VoltageMaxCellWarning Value: {iso}", VoltageMaxCellWarning);
                    VoltageWarnMaxCellInd.Text = "✓";
                    VoltageWarnMaxCellInd.Foreground = greenBrush;
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

        private void VoltageWarnMaxCell_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageWarnMaxCellInd.Text = "X";
            VoltageWarnMaxCellInd.Foreground = redBrush;
        }

        private void VoltageWarnMinCell_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMinCellWarning = int.Parse(BoxVoltageWarnMinCell.Text);
                    logger.LogDebug("Store VoltageMinCellWarning Value: {iso}", VoltageMinCellWarning);
                    VoltageWarnMinCellInd.Text = "✓";
                    VoltageWarnMinCellInd.Foreground = greenBrush;
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

        private void VoltageWarnMinCell_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageWarnMinCellInd.Text = "X";
            VoltageWarnMinCellInd.Foreground = redBrush;
        }

        private void VoltageWarnMaxCellCharge_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMaxCellChargingWarning = int.Parse(BoxVoltageWarnMaxCellCharge.Text);
                    logger.LogDebug("Store VoltageMaxCellChargingWarning Value: {iso}", VoltageMaxCellChargingWarning);
                    VoltageWarnMaxCellChargeInd.Text = "✓";
                    VoltageWarnMaxCellChargeInd.Foreground = greenBrush;
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

        private void VoltageWarnMaxCellCharge_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageWarnMaxCellChargeInd.Text = "X";
            VoltageWarnMaxCellChargeInd.Foreground = redBrush;
        }

        private void VoltageWarnMaxPack_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMaxPackWarning = int.Parse(BoxVoltageWarnMaxPack.Text);
                    logger.LogDebug("Store VoltageMaxPackWarning Value: {iso}", VoltageMaxPackWarning);
                    VoltageWarnMaxPackInd.Text = "✓";
                    VoltageWarnMaxPackInd.Foreground = greenBrush;
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

        private void VoltageWarnMaxPack_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageWarnMaxPackInd.Text = "X";
            VoltageWarnMaxPackInd.Foreground = redBrush;
        }

        private void VoltageWarnMinPack_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMinPackWarning = int.Parse(BoxVoltageWarnMinPack.Text);
                    logger.LogDebug("Store VoltageMinPackWarning Value: {iso}", VoltageMinPackWarning);
                    VoltageWarnMinPackInd.Text = "✓";
                    VoltageWarnMinPackInd.Foreground = greenBrush;
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

        private void VoltageWarnMinPack_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageWarnMinPackInd.Text = "X";
            VoltageWarnMinPackInd.Foreground = redBrush;
        }

        private void VoltageWarnMaxPackCharge_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    VoltageMaxPackChargingWarning = int.Parse(BoxVoltageWarnMaxPackCharge.Text);
                    logger.LogDebug("Store VoltageMaxPackChargingWarning Value: {iso}", VoltageMaxPackChargingWarning);
                    VoltageWarnMaxPackChargeInd.Text = "✓";
                    VoltageWarnMaxPackChargeInd.Foreground = greenBrush;
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

        private void VoltageWarnMaxPackCharge_TextChanged(object sender, TextChangedEventArgs e)
        {
            VoltageWarnMaxPackChargeInd.Text = "X";
            VoltageWarnMaxPackChargeInd.Foreground = redBrush;
        }

        private void MainLowIndex_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainLowIndex = byte.Parse(BoxMainLowIndex.Text);
                    logger.LogDebug("Store MainLowIndex Value: {iso}", MainLowIndex);
                    MainLowIndexInd.Text = "✓";
                    MainLowIndexInd.Foreground = greenBrush;
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

        private void MainLowIndex_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainLowIndexInd.Text = "X";
            MainLowIndexInd.Foreground = redBrush;
        }

        private void MainLowFlag_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainLowFlag = (PSR.BMS.Configuration.Tables.ContactorConfiguration.Contactor.StateFlags) byte.Parse(BoxMainLowFlag.Text);
                    logger.LogDebug("Store MainLowFlag Value: {iso}", MainLowFlag);
                    MainLowFlagInd.Text = "✓";
                    MainLowFlagInd.Foreground = greenBrush;
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

        private void MainLowFlag_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainLowFlagInd.Text = "X";
            MainLowFlagInd.Foreground = redBrush;
        }

        private void MainLowHoldDelay_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainLowHoldDelay = ushort.Parse(BoxMainLowHoldDelay.Text);
                    logger.LogDebug("Store MainLowHoldDelay Value: {iso}", MainLowHoldDelay);
                    MainLowHoldDelayInd.Text = "✓";
                    MainLowHoldDelayInd.Foreground = greenBrush;
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

        private void MainLowHoldDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainLowHoldDelayInd.Text = "X";
            MainLowHoldDelayInd.Foreground = redBrush;
        }

        private void MainLowHoldDuty_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainLowHoldDuty = float.Parse(BoxMainLowHoldDuty.Text);
                    logger.LogDebug("Store MainLowHoldDuty Value: {iso}", MainLowHoldDuty);
                    MainLowHoldDutyInd.Text = "✓";
                    MainLowHoldDutyInd.Foreground = greenBrush;
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

        private void MainLowHoldDuty_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainLowHoldDutyInd.Text = "X";
            MainLowHoldDutyInd.Foreground = redBrush;
        }

        private void MainHighIndex_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainHighIndex = byte.Parse(BoxMainHighIndex.Text);
                    logger.LogDebug("Store MainHighIndex Value: {iso}", MainHighIndex);
                    MainHighIndexInd.Text = "✓";
                    MainHighIndexInd.Foreground = greenBrush;
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

        private void MainHighIndex_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainHighIndexInd.Text = "X";
            MainHighIndexInd.Foreground = redBrush;
        }

        private void MainHighFlag_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainHighFlag = (PSR.BMS.Configuration.Tables.ContactorConfiguration.Contactor.StateFlags) byte.Parse(BoxMainHighFlag.Text);
                    logger.LogDebug("Store MainHighFlag Value: {iso}", MainHighFlag);
                    MainHighFlagInd.Text = "✓";
                    MainHighFlagInd.Foreground = greenBrush;
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

        private void MainHighFlag_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainHighFlagInd.Text = "X";
            MainHighFlagInd.Foreground = redBrush;
        }

        private void MainHighHoldDelay_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainHighHoldDelay = ushort.Parse(BoxMainHighHoldDelay.Text);
                    logger.LogDebug("Store MainHighHoldDelay Value: {iso}", MainHighHoldDelay);
                    MainHighHoldDelayInd.Text = "✓";
                    MainHighHoldDelayInd.Foreground = greenBrush;
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

        private void MainHighHoldDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainHighHoldDelayInd.Text = "X";
            MainHighHoldDelayInd.Foreground = redBrush;
        }

        private void MainHighHoldDuty_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainHighHoldDuty = float.Parse(BoxMainHighHoldDuty.Text);
                    logger.LogDebug("Store MainHighHoldDuty Value: {iso}", MainHighHoldDuty);
                    MainHighHoldDutyInd.Text = "✓";
                    MainHighHoldDutyInd.Foreground = greenBrush;
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

        private void MainHighHoldDuty_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainHighHoldDutyInd.Text = "X";
            MainHighHoldDutyInd.Foreground = redBrush;
        }

        private void MainHighPullDuty_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainHighPullInDuty = float.Parse(BoxMainHighPullDuty.Text);
                    logger.LogDebug("Store MainHighPullInDuty Value: {iso}", MainHighPullInDuty);
                    MainHighPullDutyInd.Text = "✓";
                    MainHighPullDutyInd.Foreground = greenBrush;
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

        private void MainHighPullDuty_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainHighPullDutyInd.Text = "X";
            MainHighPullDutyInd.Foreground = redBrush;
        }

        private void MainLowPullDuty_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    MainLowPullInDuty = float.Parse(BoxMainLowPullDuty.Text);
                    logger.LogDebug("Store MainLowPullInDuty Value: {iso}", MainLowPullInDuty);
                    MainLowPullDutyInd.Text = "✓";
                    MainLowPullDutyInd.Foreground = greenBrush;
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

        private void MainLowPullDuty_TextChanged(object sender, TextChangedEventArgs e)
        {
            MainLowPullDutyInd.Text = "X";
            MainLowPullDutyInd.Foreground = redBrush;
        }

        private void ChargeIndex_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    ChargeIndex = byte.Parse(BoxChargeIndex.Text);
                    logger.LogDebug("Store ChargeIndex Value: {iso}", ChargeIndex);
                    ChargeIndexInd.Text = "✓";
                    ChargeIndexInd.Foreground = greenBrush;
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

        private void ChargeIndex_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChargeIndexInd.Text = "X";
            ChargeIndexInd.Foreground = redBrush;
        }

        private void ChargeFlag_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    ChargeFlag = (PSR.BMS.Configuration.Tables.ContactorConfiguration.Contactor.StateFlags) byte.Parse(BoxChargeFlag.Text);
                    logger.LogDebug("Store ChargeFlag Value: {iso}", ChargeFlag);
                    ChargeFlagInd.Text = "✓";
                    ChargeFlagInd.Foreground = greenBrush;
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

        private void ChargeFlag_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChargeFlagInd.Text = "X";
            ChargeFlagInd.Foreground = redBrush;
        }

        private void ChargeHoldDelay_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    ChargeHoldDelay = ushort.Parse(BoxChargeHoldDelay.Text);
                    logger.LogDebug("Store ChargeHoldDelay Value: {iso}", ChargeHoldDelay);
                    ChargeHoldDelayInd.Text = "✓";
                    ChargeHoldDelayInd.Foreground = greenBrush;
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

        private void ChargeHoldDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChargeHoldDelayInd.Text = "X";
            ChargeHoldDelayInd.Foreground = redBrush;
        }

        private void ChargeHoldDuty_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    ChargeHoldDuty = float.Parse(BoxChargeHoldDuty.Text);
                    logger.LogDebug("Store ChargeHoldDuty Value: {iso}", ChargeHoldDuty);
                    ChargeHoldDutyInd.Text = "✓";
                    ChargeHoldDutyInd.Foreground = greenBrush;
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

        private void ChargeHoldDuty_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChargeHoldDutyInd.Text = "X";
            ChargeHoldDutyInd.Foreground = redBrush;
        }

        private void ChargePullDuty_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    ChargePullInDuty = float.Parse(BoxChargePullDuty.Text);
                    logger.LogDebug("Store ChargePullInDuty Value: {iso}", ChargePullInDuty);
                    ChargePullDutyInd.Text = "✓";
                    ChargePullDutyInd.Foreground = greenBrush;
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

        private void ChargePullDuty_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChargePullDutyInd.Text = "X";
            ChargePullDutyInd.Foreground = redBrush;
        }

        private void PrechargeIndex_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    PreChargeIndex = byte.Parse(BoxPrechargeIndex.Text);
                    logger.LogDebug("Store PreChargeIndex Value: {iso}", PreChargeIndex);
                    PrechargeIndexInd.Text = "✓";
                    PrechargeIndexInd.Foreground = greenBrush;
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

        private void PrechargeIndex_TextChanged(object sender, TextChangedEventArgs e)
        {
            PrechargeIndexInd.Text = "X";
            PrechargeIndexInd.Foreground = redBrush;
        }

        private void PrechargeFlag_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    PreChargeFlag = (PSR.BMS.Configuration.Tables.ContactorConfiguration.Contactor.StateFlags) byte.Parse(BoxPrechargeFlag.Text);
                    logger.LogDebug("Store PreChargeFlag Value: {iso}", PreChargeFlag);
                    PrechargeFlagInd.Text = "✓";
                    PrechargeFlagInd.Foreground = greenBrush;
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

        private void PrechargeFlag_TextChanged(object sender, TextChangedEventArgs e)
        {
            PrechargeFlagInd.Text = "X";
            PrechargeFlagInd.Foreground = redBrush;
        }

        private void PrechargeHoldDelay_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    PreChargeHoldDelay = ushort.Parse(BoxPrechargeHoldDelay.Text);
                    logger.LogDebug("Store PreChargeHoldDelay Value: {iso}", PreChargeHoldDelay);
                    PrechargeHoldDelayInd.Text = "✓";
                    PrechargeHoldDelayInd.Foreground = greenBrush;
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

        private void PrechargeHoldDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            PrechargeHoldDelayInd.Text = "X";
            PrechargeHoldDelayInd.Foreground = redBrush;
        }

        private void PrechargeHoldDuty_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    PreChargeHoldDuty = float.Parse(BoxPrechargeHoldDuty.Text);
                    logger.LogDebug("Store PreChargeHoldDuty Value: {iso}", PreChargeHoldDuty);
                    PrechargeHoldDutyInd.Text = "✓";
                    PrechargeHoldDutyInd.Foreground = greenBrush;
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

        private void PrechargeHoldDuty_TextChanged(object sender, TextChangedEventArgs e)
        {
            PrechargeHoldDutyInd.Text = "X";
            PrechargeHoldDutyInd.Foreground = redBrush;
        }

        private void PrechargePullDuty_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    PreChargePullInDuty = float.Parse(BoxPrechargePullDuty.Text);
                    logger.LogDebug("Store PreChargePullInDuty Value: {iso}", PreChargePullInDuty);
                    PrechargePullDutyInd.Text = "✓";
                    PrechargePullDutyInd.Foreground = greenBrush;
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

        private void PrechargePullDuty_TextChanged(object sender, TextChangedEventArgs e)
        {
            PrechargePullDutyInd.Text = "X";
            PrechargePullDutyInd.Foreground = redBrush;
        }

        private void PackTopoCellGroups_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    CellGroups = byte.Parse(BoxPackTopoCellGroups.Text);
                    logger.LogDebug("Store CellGroups Value: {iso}", CellGroups);
                    PackTopoCellGroupsInd.Text = "✓";
                    PackTopoCellGroupsInd.Foreground = greenBrush;
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

        private void PackTopoCellGroups_TextChanged(object sender, TextChangedEventArgs e)
        {
            PackTopoCellGroupsInd.Text = "X";
            PackTopoCellGroupsInd.Foreground = redBrush;
        }

        private void PackTopoParallel_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    ParallelCells = byte.Parse(BoxPackTopoParallel.Text);
                    logger.LogDebug("Store ParallelCells Value: {iso}", ParallelCells);
                    PackTopoParallelInd.Text = "✓";
                    PackTopoParallelInd.Foreground = greenBrush;
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

        private void PackTopoParallel_TextChanged(object sender, TextChangedEventArgs e)
        {
            PackTopoParallelInd.Text = "X";
            PackTopoParallelInd.Foreground = redBrush;
        }

        private void PackTopoSeries_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    SeriesCells = byte.Parse(BoxPackTopoSeries.Text);
                    logger.LogDebug("Store SeriesCells Value: {iso}", SeriesCells);
                    PackTopoSeriesInd.Text = "✓";
                    PackTopoSeriesInd.Foreground = greenBrush;
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

        private void PackTopoSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            PackTopoSeriesInd.Text = "X";
            PackTopoSeriesInd.Foreground = redBrush;
        }

        private void PackTopoReserved_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    Reserved = byte.Parse(BoxPackTopoReserved.Text);
                    logger.LogDebug("Store Reserved Value: {iso}", Reserved);
                    PackTopoReservedInd.Text = "✓";
                    PackTopoReservedInd.Foreground = greenBrush;
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

        private void PackTopoReserved_TextChanged(object sender, TextChangedEventArgs e)
        {
            PackTopoReservedInd.Text = "X";
            PackTopoReservedInd.Foreground = redBrush;
        }

        private void PreChargeDelay_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    PreChargeDelay = float.Parse(BoxPreChargeDelay.Text);
                    logger.LogDebug("Store PreChargeDelay Value: {iso}", PreChargeDelay);
                    PreChargeDelayInd.Text = "✓";
                    PreChargeDelayInd.Foreground = greenBrush;
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

        private void PreChargeDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            PreChargeDelayInd.Text = "X";
            PreChargeDelayInd.Foreground = redBrush;
        }

        private void ContactorPWMFreq_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == (VirtualKey)13)
            {
                try
                {
                    ContactorPwmFrequency = uint.Parse(BoxContactorPWMFreq.Text);
                    logger.LogDebug("Store ContactorPwmFrequency Value: {iso}", ContactorPwmFrequency);
                    ContactorPWMFreqInd.Text = "✓";
                    ContactorPWMFreqInd.Foreground = greenBrush;
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

        private void ContactorPWMFreq_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContactorPWMFreqInd.Text = "X";
            ContactorPWMFreqInd.Foreground = redBrush;
        }

        private void IsoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsoActivate = true;
        }

        private void IsoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsoActivate = false;
        }

        private void CurrentWarnCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CurrentWarnActivate = true;
        }

        private void CurrentWarnCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CurrentWarnActivate = false;
        }

        private void CurrentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CurrentLimitsActivate = true;
        }

        private void CurrentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CurrentLimitsActivate = false;
        }

        private void VoltageLimitsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            VoltageActivate = true;
        }

        private void VoltageLimitsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            VoltageActivate = false;
        }

        private void VoltageWarningCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            VoltageWarnActivate = true;
        }

        private void VoltageWarningCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            VoltageWarnActivate = false;
        }

        private void MainHighCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MainHighSideActivate = true;
        }

        private void MainHighCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MainHighSideActivate = false;
        }

        private void MainLowCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MainLowSideActivate = true;
        }

        private void MainLowCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MainLowSideActivate = false;
        }

        private void ChargeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ChargeActivate = true;
        }

        private void ChargeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ChargeActivate = false;
        }

        private void PreChargeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PreChargeActivate = true;
        }

        private void PreChargeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PreChargeActivate = false;
        }

        private void PreChargeDelayCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PreChargeDelayActivate = true;
        }

        private void PreChargeDelayCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PreChargeDelayActivate = false;
        }

        private void PWMFreqCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PWMActivate = true;
        }

        private void PWMFreqCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PWMActivate = false;
        }

        private void PackTopoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PackTopoActivate = true;
        }

        private void PackTopoCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PackTopoActivate = false;
        }
        //private void changeButtonText(Button myButton, string text)
        //{
        //    myButton.Content = text;
        //}

        //private string processTextBox()
        //{
        //    return textBoxTest.Text;
        //}

        //private void TextBoxChange(object sender, TextChangedEventArgs e)
        //{
        //    changeButtonText(myButton, "Edit");
        //}

        //private void TextBoxKeyDown(object sender, KeyRoutedEventArgs e)
        //{
        //    if (e.Key == (VirtualKey)13)
        //    {
        //        logger.LogDebug("Enter Key is hit");
        //        string temp = value;
        //        value = processTextBox();
        //        logger.LogDebug("{temp} is changed into {value}", temp, value);
        //        //WriteToPort(value); //For Serial Port Operation
        //        changeButtonText(myButton, "Edited");
        //    }
        //}



        //Port operation
        /// <summary>
        /// Detect New Port Names
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void PortDetect(object sender, RoutedEventArgs e)
        //{
        //    logger.LogDebug("Run Port Detection");
        //    portsNames = SerialPort.GetPortNames();
        //    PortsBox.ItemsSource = portsNames;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void PortSelect(object sender, SelectionChangedEventArgs e)
        //{
        //    if (portsNames.Length > 0 & !portConnected)
        //    {
        //        changeButtonText(PortDisconnectButton, "Disconnect");
        //        if (e.AddedItems[0] != null)
        //        { 
        //            selectedPortName = e.AddedItems[0].ToString();
        //            serialPort = new SerialPort(selectedPortName, 9600, Parity.None, 8, StopBits.One);
        //            serialPort.RtsEnable = true;
        //            serialPort.DataReceived += new SerialDataReceivedEventHandler(ReadFromPort);
        //            //Attempting to connect
        //            PortConnect(serialPort);
        //            portConnected = true;
        //        }
        //    }
        //}

        //private void PortConnect(SerialPort serialPort)
        //{
        //    logger.LogDebug("Connecting to {port}", serialPort.PortName);
        //    int tries = 22;
        //    while (tries > 0)
        //    {
        //        try
        //        {
        //            serialPort.Handshake = Handshake.XOnXOff;
        //            if (!serialPort.IsOpen)
        //            {
        //                serialPort.Open();
        //                Thread.Sleep(1);
        //            }
        //            break;
        //        }
        //        catch (UnauthorizedAccessException)
        //        {
        //            tries--;
        //            Thread.Sleep(1);  
        //        }
        //    }
        //    if (serialPort.IsOpen)
        //    {
        //        PortStatusText.Text = "Port Status:    Connected"; //Spacing to match "Port Status: Disconnected"
        //        logger.LogDebug("{port} is selected and opened!", serialPort.PortName);
        //    }
        //}

        //private void DisconnectPort(object sender, RoutedEventArgs e)
        //{
        //    if (serialPort != null)
        //    {
        //        if (serialPort.IsOpen)
        //        {
        //            logger.LogDebug("Disconnecting from {port}", serialPort.PortName);
        //            serialPort.Close();
        //            changeButtonText(PortDisconnectButton, "Disconnected");
        //            PortStatusText.Text = "Port Status: Disconnected";
        //            errorWindows DisconnectWindow = new errorWindows();
        //            DisconnectWindow.Activate();
        //            logger.LogDebug("Disconnected. If you want to connect back to the same port, MUST unplug and plug it in again");
        //        }
        //    }
        //}

        //private void WriteToPort(string text)
        //{
        //    logger.LogDebug("Writing Data: {text}", text);
        //     if (serialPort.IsOpen)
        //    {
        //        serialPort.WriteLine(text);
        //        logger.LogDebug("Data Written: {text}", text);
        //    }
        //}

        ////Read 1 byte from Port
        //private void ReadFromPort(object sender, SerialDataReceivedEventArgs e)
        //{
        //    logger.LogDebug("Intercepting Data");
        //    if (serialPort.IsOpen)
        //    {
        //        serialPort.Read(portBuffer, 0, 1);
        //        logger.LogDebug("Data read: {data}", portBuffer[0]);
        //        logger.LogDebug("Data Intercepted");
        //    }
        //}
        ////End Line
    }
}
