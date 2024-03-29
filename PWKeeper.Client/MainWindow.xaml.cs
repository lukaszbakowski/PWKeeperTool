﻿using System;
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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using PWKeeper.Core;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.IO;
using PWKeeper.Core.Models;

namespace PWKeeper.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration config = builder.Build();
            TestModel test = new();
            config.Bind("Test", test);

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(test);
            serviceCollection.AddWpfBlazorWebView();
            serviceCollection.AddPWKeeperCore();
            Resources.Add("services", serviceCollection.BuildServiceProvider());
        }
    }
}
