namespace AzureStorageSync
{
    using System;
    using Catel.Logging;
    using Logging;

    internal class Program
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private static int Main(string[] args)
        {
#if DEBUG
            LogManager.AddDebugListener(true);
#endif

            var consoleLogListener = new OutputLogListener();
            LogManager.AddListener(consoleLogListener);

            try
            {
                HelpWriter.WriteAppHeader(s => Log.Write(LogEvent.Info, s));

                var context = ArgumentParser.ParseArguments(args);
                if (context.IsHelp)
                {
                    HelpWriter.WriteHelp(s => Log.Write(LogEvent.Info, s));

                    WaitForKeyPress();

                    return 0;
                }

                var task = Synchronizer.SyncAsync(context);
                task.Wait();

#if DEBUG
                WaitForKeyPress();
#endif

                return task.Result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred");

#if DEBUG
                WaitForKeyPress();
#endif

                return -1;
            }
        }

        private static void WaitForKeyPress()
        {
            Log.Info(string.Empty);
            Log.Info("Press any key to continue");

            Console.ReadKey();
        }
    }
}