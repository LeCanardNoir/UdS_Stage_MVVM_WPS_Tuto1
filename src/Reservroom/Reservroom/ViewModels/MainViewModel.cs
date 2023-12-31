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
        public MainViewModel()
        {
            CurrentViewModel = new ReservationListViewModel();
        }
    }
}
