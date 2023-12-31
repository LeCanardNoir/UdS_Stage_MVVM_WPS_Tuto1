using Reservroom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.ViewModels
{
    public class MainViewModel : ViewModelsBase
    {
        public ViewModelsBase CurrentViewModel { get; }

        public MainViewModel(Hotel hotel)
        {
            CurrentViewModel = new ReservationListViewModel();
        }
    }
}
