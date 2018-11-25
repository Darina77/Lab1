using System;
using System.ServiceModel;
using System.ServiceProcess;
using Lab1.Tools;

namespace Lab1.Services{
    public class WalletSimulatorWindowsService : ServiceBase
    {
        internal const string CurrentServiceName = "WalletSimulatorService1";
        internal const string CurrentServiceDisplayName = "Wallet Simulator Service1";
        internal const string CurrentServiceSource = "WalletSimulatorServiceSource1";
        internal const string CurrentServiceLogName = "WalletSimulatorServiceLogName1";
        internal const string CurrentServiceDescription = "Wallet Simulator for learning purposes1.";
        private ServiceHost _serviceHost = null;

        public WalletSimulatorWindowsService()
        {
            ServiceName = CurrentServiceName;
            try
            {
                AppDomain.CurrentDomain.UnhandledException += UnhandledException;
                Logger.Log("Initialization");
            }
            catch (Exception ex)
            {
                Logger.Log("Initialization", ex);
            }
        }

        protected override void OnStart(string[] args)
        {
            Logger.Log("OnStart");
            RequestAdditionalTime(120 * 1000);
#if DEBUG
            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(1000);
            //}
#endif
            try
            {
                if (_serviceHost != null)
                    _serviceHost.Close();
            }
            catch
            {
            }
            try
            {
                _serviceHost = new ServiceHost(typeof(WalletSimulatorService));
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                Logger.Log("OnStart", ex);
                throw;
            }
            Logger.Log("Service Started");
        }

        protected override void OnStop()
        {
            Logger.Log("OnStop");
            RequestAdditionalTime(120 * 1000);
            try
            {
                _serviceHost.Close();
            }
            catch (Exception ex)
            {
                Logger.Log("Trying To Stop The Host Listener", ex);
            }
            Logger.Log("Service Stopped");
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            var ex = (Exception)args.ExceptionObject;

            Logger.Log("UnhandledException", ex);
        }
    }
}
