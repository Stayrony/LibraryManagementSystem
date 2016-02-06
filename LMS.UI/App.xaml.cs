// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Логика взаимодействия для App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using LMS.UI.Context;
using LMS.UI.Control;

namespace LMS.UI
{
    using System.Windows;

    /// <summary>
    ///     Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow mainWindow;

        private void AppStartUp(object sender, StartupEventArgs args)
        {
            try
            {
                ControlManager.GetInstance().Add("MainWindow", typeof(MainWindow));
                ControlManager.GetInstance().Add("LoginControl", typeof(LoginControl));
                ControlManager.GetInstance().Add("DashboardControl", typeof(DashboardControl));

                this.mainWindow = ControlManager.GetInstance().GetControl("MainWindow") as MainWindow;
                this.mainWindow.WindowStartupLocation= WindowStartupLocation.CenterScreen;
                this.mainWindow.Show();

                ControlManager.GetInstance().Place("MainWindow", "mainRegion", "LoginControl");
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        public void AppExit(object sender, ExitEventArgs args)
        {
            this.Shutdown();
        }
    }
}