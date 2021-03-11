﻿using AppRefrigitz.Services;
using AppRefrigitz.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppRefrigitz
{
    public partial class App : Application
    {
        Chess.Chess run = null;

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            run = new Chess.Chess();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
