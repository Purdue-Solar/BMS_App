using CsvHelper.TypeConversion;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSManagerRebuilt.GUIOperation.TableConfig
{
    class TableTabViewOperation
    {
        /// <summary>
        /// Command Line Debug Logger Variable (not save in logfile)
        /// </summary>
        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        static ILogger logger = factory.CreateLogger<MainWindow>();

        public static string TabName = default;


        public static void Tabs_CloseButtonClick(TabView sender, TabViewTabCloseRequestedEventArgs args)
        {
            sender.TabItems.Remove(args.Tab);
        }

        public static async Task TableTabs_AddButtonClick(TabView sender,object args)
        {
            logger.LogDebug("Table Add Button Clicked");
            TableNewTab p_Window = new TableNewTab();
            p_Window.Activate();
            TabName = p_Window.TabName;

            //Wait for TabName to be inserted
            while (TabName == default)
            {
                logger.LogDebug("TabName is {TabName}", TabName);
                await Task.Delay(100);
                TabName = p_Window.TabName;
            }

            //Close Window after TabName is changed
            p_Window.Close();
            logger.LogDebug("TabName is {TabName}", TabName);
            //Initialized TabViewItem
            var newTab = new TabViewItem();
            newTab.HorizontalContentAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center;
            newTab.Header = TabName;
            logger.LogDebug("New tab initial setting");

            Frame newTabFrame = new Frame();
            //TableConfigPage.TabName = TabName;
            newTabFrame.Navigate(typeof(TableConfigPage));
            logger.LogDebug("New Tab Frame successfully created");
            newTab.Content = newTabFrame;

            //newTabFrame.TabName = TabName;
            sender.TabItems.Add(newTab);
            sender.SelectedItem = newTab;
            logger.LogDebug("New Tab Frame successfully added");
        }
    }
}
