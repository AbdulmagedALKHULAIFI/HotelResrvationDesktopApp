using HotelResrvationDesktopApp.Services;
using HotelResrvationDesktopApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelResrvationDesktopApp.ViewModels.Commands
{
    public class NavigateCommand : CommandBase
    {
        //ToDo: watch delegate commands

        private readonly NavigationService _navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
