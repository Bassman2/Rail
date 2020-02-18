﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Rail.TrackEditor.View;
using Rail.TrackEditor.ViewModel;

namespace Rail.TrackEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            Trace.TraceInformation("Startup {0} {1}", DateTime.Now.ToLocalTime().ToShortTimeString(), DateTime.Now.ToLocalTime().ToShortDateString());

            //string language = Settings.Default.Language;
            //if (!string.IsNullOrEmpty(language))
            //{
            //    CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture = new CultureInfo(language);
            //}

            //CultureInfo.CurrentUICulture = new CultureInfo("de-DE");
            //CultureInfo.CurrentCulture = new CultureInfo("de-DE");
            //CultureInfo.CurrentUICulture = new CultureInfo("en-US");    // for UI
            //CultureInfo.CurrentCulture = new CultureInfo("en-US");      // for ToString("F2")
            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);
                new MainView() { DataContext = new MainViewModel() }.Show();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;
            Trace.TraceError(ex.ToString());
            MessageBox.Show(ex.ToString(), "Unhandled Error");
        }
    }
}
