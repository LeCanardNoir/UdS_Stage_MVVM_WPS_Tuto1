using Microsoft.EntityFrameworkCore;
using Reservroom.DbContexts;
using Reservroom.Exceptions;
using Reservroom.Models;
using Reservroom.Services;
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
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _hotel = new Hotel("burn@ Hotel");
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(CONNECTION_STRING).Options;
            using (ReservroomDbContext dbContext = new ReservroomDbContext(options))
            {
                dbContext.Database.Migrate();
            }


            _navigationStore.CurrentViewModel = CreateReservationListViewModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationServices(_navigationStore, CreateReservationListViewModel));
        }

        private ReservationListViewModel CreateReservationListViewModel()
        {
            return new ReservationListViewModel(_hotel, new NavigationServices(_navigationStore, CreateMakeReservationViewModel));
        }
    }
}
