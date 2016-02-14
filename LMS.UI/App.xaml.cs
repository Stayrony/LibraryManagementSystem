// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using LMS.UI.Context;
using LMS.UI.Control;

namespace LMS.UI
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
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
                ControlManager.GetInstance().Add("AddRemoveBookControl", typeof(AddRemoveBookControl));
                ControlManager.GetInstance().Add("CreateCategoryControl", typeof(CreateCategoryControl));
                ControlManager.GetInstance().Add("IssueBookControl", typeof(IssueBookControl));
                ControlManager.GetInstance().Add("ReturnBookControl", typeof(ReturnBookControl));

                this.mainWindow = ControlManager.GetInstance().GetControl("MainWindow") as MainWindow;
                this.mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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