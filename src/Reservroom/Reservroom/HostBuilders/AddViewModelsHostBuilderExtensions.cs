using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reservroom.Services;
using Reservroom.Stores;
using Reservroom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient((s) => CreateReservationListingViewModel(s));
                services.AddSingleton<NavigationServices<ReservationListViewModel>>();
                services.AddSingleton<Func<ReservationListViewModel>>(s => () => s.GetRequiredService<ReservationListViewModel>());
                services.AddSingleton<NavigationServices<ReservationListViewModel>>();

                services.AddTransient<MakeReservationViewModel>();
                services.AddSingleton<NavigationServices<MakeReservationViewModel>>();
                services.AddSingleton<Func<MakeReservationViewModel>>(s => () => s.GetRequiredService<MakeReservationViewModel>());

                services.AddSingleton<MainViewModel>();
            });
            return hostBuilder;
        }


        private static ReservationListViewModel CreateReservationListingViewModel(IServiceProvider s)
        {
            return ReservationListViewModel.LoadViewModel(
                s.GetRequiredService<HotelStore>(),
                s.GetRequiredService<NavigationServices<MakeReservationViewModel>>()
                );
        }
    }
}
