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
    public class NavigateCommand : CommandBase
    {
        private NavigationServices _navigateServices;

        public NavigateCommand(NavigationServices navigateServices)
        {
            _navigateServices = navigateServices;
        }

        public override void Execute(object? parameter)
        {
            _navigateServices.Navigate();
        }
    }
}
