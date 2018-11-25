using System;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Lab1.Services
{
    class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //Thread.Sleep(10000);
            bool isInstalled = false;
            bool serviceStarting = false;
            const string serviceName = CountSimulatorWindowsService.CurrentServiceName;

            ServiceController[] services = ServiceController.GetServices();

            foreach (ServiceController service in services)
            {
                if (!service.ServiceName.Equals(serviceName))
                    continue;
                isInstalled = true;
                if (service.Status == ServiceControllerStatus.StartPending)
                {
                    serviceStarting = true;
                }
                break;
            }

            if (!serviceStarting)
            {
                if (isInstalled)
                {
                    DialogResult dr =
                        MessageBox.Show(string.Format("Do You REALLY Want To Uninstall {0}", serviceName),
                            "Danger", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr != DialogResult.Yes)
                        return;
                    SelfInstaller.UninstallMe();
                    MessageBox.Show(string.Format("{0} Successfully Uninstalled", serviceName),
                        "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        SelfInstaller.InstallMe()
                            ? string.Format("{0} Successfully Installed", serviceName)
                            : string.Format("{0} FAILED To Install", serviceName),
                        "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                var servicesToRun = new ServiceBase[] { new CountSimulatorWindowsService(), };
                ServiceBase.Run(servicesToRun);
            }
        }
    }

    internal static class SelfInstaller
    {
        private static readonly string ExePath = Assembly.GetExecutingAssembly().Location;
        internal static bool InstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new[] { ExePath });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        internal static bool UninstallMe()
        {
            try
            {
                ManagedInstallerClass.InstallHelper(
                    new[] { "/u", ExePath });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }
    }
}
