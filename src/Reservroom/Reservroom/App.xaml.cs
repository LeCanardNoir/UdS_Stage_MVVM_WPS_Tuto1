using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservroom.DbContexts;
using Reservroom.Exceptions;
using Reservroom.HostBuilders;
using Reservroom.Models;
using Reservroom.Services;
using Reservroom.Services.ReservationConflictValidators;
using Reservroom.Services.ReservationCreators;
using Reservroom.Services.ReservationProviders;
using Reservroom.Stores;
using Reservroom.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Reservroom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=reservroom.db";
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices((hostContext, services) => 
                {
                    string connectionString = hostContext.Configuration.GetConnectionString("Default");
                    services.AddSingleton(new ReservroomDbContextFactory(connectionString));

                    services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                    services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    services.AddSingleton<IResvationConflictValidator, DataBaseReservationConflictValidator>();

                    services.AddTransient<ReservationBook>();

                    string hotelName = hostContext.Configuration.GetValue<string>("hotelName");
                    services.AddSingleton<Hotel>(s => new Hotel(hotelName, s.GetRequiredService<ReservationBook>()));

                    services.AddSingleton<HotelStore>();
                    services.AddSingleton<NavigationStore>();


                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });


                }).Build();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            ReservroomDbContextFactory reservroomDbContextFactory = _host.Services.GetRequiredService<ReservroomDbContextFactory>();

            using (ReservroomDbContext dbContext = reservroomDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigationServices<ReservationListViewModel> navigationServices = _host.Services.GetRequiredService<NavigationServices<ReservationListViewModel>>();
            navigationServices.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host?.Dispose();
            base.OnExit(e);
        }
    }
}
