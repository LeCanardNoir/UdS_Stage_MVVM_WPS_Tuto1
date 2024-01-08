using Reservroom.Stores;
using Reservroom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Services
{
    public class NavigationServices<VM> where VM : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<VM> _createViewModel;

        public NavigationServices(NavigationStore navigationStore, Func<VM> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }
        public void Navigate() 
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
