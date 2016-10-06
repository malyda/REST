using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace REST.Tools
{
    /// <summary>
    /// Used for freeze and unfreeze UI thread
    /// </summary>
    class Freezer
    {
        const int TIMER_INTERVAL = 3000;

        /// <summary>
        /// Timer which ends frozen state of UI thread
        /// </summary>
        System.Timers.Timer _timer = new System.Timers.Timer(TIMER_INTERVAL);

        /// <summary>
        /// freezeLock which value is injected from background task used for breaking frozen state
        /// </summary>
        bool _freezeLock = true;

        /// <summary>
        /// Freeze UI thread on button click and unfreeze after timer elapsed
        /// </summary>
        /// <param name="button"></param>
        /// <param name="uiThread"></param>
        public void Freeze(Button button, Window uiThread) 
        {
            button.Content = "Frozen";

            _timer.Elapsed += FreezeStop;
            _timer.Enabled = true;

           uiThread.Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.SystemIdle, null);

           
            while (_freezeLock) ;

            button.Content = "Freeze";
            _freezeLock = false;
            _timer.Enabled = false;
        }

        /// <summary>
        /// Unfreeze UI thread by changing freezeLock value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void FreezeStop(object sender, ElapsedEventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                Console.WriteLine(Thread.CurrentThread.IsBackground);
                _freezeLock = false;
            });
        }
    }
}
