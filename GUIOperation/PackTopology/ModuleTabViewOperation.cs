using BMSManagerRebuilt.GUIOperation.NewTabNameWindow;
using BMSManagerRebuilt.GUIOperation.PackTopology;
using CsvHelper.TypeConversion;
using Microsoft.Extensions.Logging;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSManagerRebuilt.GUIOperation.TableConfig
{
    class ModuleTabViewOperation
    {
        /// <summary>
        /// Command Line Debug Logger Variable (not save in logfile)
        /// </summary>
        static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole().AddDebug().SetMinimumLevel(LogLevel.Debug));
        static ILogger logger = factory.CreateLogger<MainWindow>();
        public static string TabName { get; set; }


        public static async Task ModuleTabs_AddButtonClick(TabView sender,object args)
        {
            TabName = default;
            ModuleNewTab p_Window = new ModuleNewTab();
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
            
            //Initialized TabViewItem
            var newTab = new TabViewItem();
            newTab.HorizontalContentAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center;
            newTab.Header = TabName;
            Frame newTabFrame = new Frame();

            if (TabName == "MODULE 1")
            {
                newTabFrame.Navigate(typeof(Module1Page));

            }
            else if (TabName == "MODULE 2")
            {
                newTabFrame.Navigate(typeof(Module2Page));

            }
            else if (TabName == "MODULE 3")
            {
                newTabFrame.Navigate(typeof(Module3Page));

            }

            newTab.Content = newTabFrame; 
            sender.TabItems.Add(newTab);
            sender.SelectedItem = newTab;
        }

    }
}
