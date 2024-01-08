using Reservroom.Models;
using Reservroom.Services;
using Reservroom.Stores;
using Reservroom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Commands
{
    public class NavigateCommand<VM> : CommandBase where VM : ViewModelBase
    {
        private NavigationServices<VM> _navigateServices;

        public NavigateCommand(NavigationServices<VM> navigateServices)
        {
            _navigateServices = navigateServices;
        }

        public override void Execute(object? parameter)
        {
            _navigateServices.Navigate();
        }
    }
}
