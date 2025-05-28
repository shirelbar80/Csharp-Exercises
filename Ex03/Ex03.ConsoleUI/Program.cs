using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class Program
    {
        static void Main()
        {
            GarageManager garageManager = new GarageManager();
            VehicleUI vehicleUI = new VehicleUI(garageManager);
            GarageActions actions = new GarageActions(garageManager);
            UIController uIManager = new UIController(garageManager, vehicleUI, actions);    

            uIManager.Run();
        }
    }




    
}
