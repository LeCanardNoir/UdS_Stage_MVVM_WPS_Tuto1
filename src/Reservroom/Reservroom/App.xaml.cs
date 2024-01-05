using Microsoft.EntityFrameworkCore;
using Reservroom.DbContexts;
using Reservroom.Exceptions;
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
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private readonly HotelStore _hotelStore;
        private readonly ReservroomDbContextFactory _reservroomDbContextFactory;

        public App()
        {
            _reservroomDbContextFactory = new ReservroomDbContextFactory(CONNECTION_STRING);
            IReservationProvider reservationProvider = new DatabaseReservationProvider(_reservroomDbContextFactory);
            IReservationCreator reservationCreator = new DatabaseReservationCreator(_reservroomDbContextFactory);
            IResvationConflictValidator reservationConflictValidator = new DataBaseReservationConflictValidator(_reservroomDbContextFactory);

            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);

            _hotel = new Hotel("burn@ Hotel", reservationBook);
            _hotelStore = new HotelStore(_hotel);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (ReservroomDbContext dbContext = _reservroomDbContextFactory.CreateDbContext())
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
            return new MakeReservationViewModel(_hotelStore, new NavigationServices(_navigationStore, CreateReservationListViewModel));
        }

        private ReservationListViewModel CreateReservationListViewModel()
        {
            return ReservationListViewModel.LoadViewModel(
                _hotelStore, 
                new NavigationServices(_navigationStore, CreateMakeReservationViewModel)
                );
        }
    }
}
