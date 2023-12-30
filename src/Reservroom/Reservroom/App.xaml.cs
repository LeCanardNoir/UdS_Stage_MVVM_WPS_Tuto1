using Reservroom.Exceptions;
using Reservroom.Models;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            Hotel hotel = new Hotel("burN@ Suites");

            hotel.MakeReservation(new Reservation(
                new RoomID(1,3),
                new DateTime(2024, 1, 6),
                new DateTime(2024, 2, 6),
                "burN@"
                ));

            hotel.MakeReservation(new Reservation(
                new RoomID(1, 3),
                new DateTime(2024, 3, 6),
                new DateTime(2024, 4, 6),
                "burN@"
                ));

            try
            {
                hotel.MakeReservation(new Reservation(
                    new RoomID(1, 3),
                    new DateTime(2024, 1, 15),
                    new DateTime(2024, 1, 31),
                    "burN@"
                    ));

            }
            catch(ReservationConflictException ex) {
                Debug.WriteLine("FUCK YEAH!!");
            }

            IEnumerable<Reservation> reservations = hotel.GetReservationsForUser("burN@");

            base.OnStartup(e);
        }
    }
}
