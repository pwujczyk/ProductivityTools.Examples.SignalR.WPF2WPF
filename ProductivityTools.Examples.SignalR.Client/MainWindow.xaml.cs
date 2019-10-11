﻿using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ProductivityTools.Examples.SignalR.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://localhost:8080/";
        public HubConnection Connection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }
         
        private void btnConnectClick(object sender, RoutedEventArgs e)
        {
            Connection = new HubConnection(ServerURI);
            HubProxy = Connection.CreateHubProxy("ExampleHub");
            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On<string>("Send", (date) => this.Dispatcher.Invoke(() => btnSend.Content = "fsa"));
            try
            {
                Connection.Start().Wait();
            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void btnSendClick(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("Send", "test123");
        }
    }
}