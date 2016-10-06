using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using RestSharp;
using REST.Entity;
using System.Timers;
using System.Windows.Threading;
using REST.Interfaces;
using REST.Tools;
using REST.WebClient;

namespace REST
{
 
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Persons list code behind collection
        /// </summary>
        private ObservableCollection<Person> _listCollection = new ObservableCollection<Person>();

        /// <summary>
        /// Persons list item source for UI
        /// </summary>
        public ObservableCollection<Person> ListCollection
        {
            get { return _listCollection; }
            set { _listCollection = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
       
            ListBox.ItemsSource = ListCollection;
            SetTimeInMili();
        }

        /// <summary>
        /// Show time in miliseconds to UI via Dispatcher
        /// </summary>
       private void SetTimeInMili()
        {
            new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 5), DispatcherPriority.SystemIdle, delegate
            {
               lblTime.Content = DateTime.Now.ToString("ffff");
            }, Dispatcher);
        }
        /// <summary>
        /// In background thread download data from server and populate them
        /// </summary>
        /// <returns></returns>
        private async Task GetPersonListAndPopulateAsync()
       {
            await Task.Run(() =>
            {
                Console.WriteLine(Thread.CurrentThread.IsBackground);

                IWebClient webClient = new Rest();
                List<Person> persons = webClient.GetPersonsListAsync().Result;

                if (persons != null && persons.Count != 0)
                {
                    ObservableCollection<Person> pl = new ObservableCollection<Person>(persons);

                    this.Dispatcher.InvokeAsync(() =>
                    {
                        ChangeStatusMessage("OK");
                        SetPersonsItemSource(pl);
                    });
                }
                else
                {
                    this.Dispatcher.InvokeAsync(() =>
                    {
                        ChangeStatusMessage( "Error connecting to server" );
                    }); 
                }
            });
       }

        /// <summary>
        /// Set persons list item source
        /// </summary>
        /// <param name="pl">Item source collection</param>
        private void SetPersonsItemSource (ObservableCollection<Person> pl)
        {
            _listCollection = pl;
            ListBox.ItemsSource = ListCollection;
        }

        
        private void GetData_Click(object sender, RoutedEventArgs e)
        {
            GetPersonListAndPopulateAsync();
        }

        private void Freeze_Click(object sender, RoutedEventArgs e)
        {
            Freezer freezer = new Freezer();
            freezer.Freeze((Button) e.Source, this);
        }

        private void ChangeStatusMessage(string message)
        {
            lblStatus.Content = message;
        }
    }
}
