using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
       Dictionary<string,GarageVehicle> m_Vehicles;//list of vehicles in the garage


        public void isVehicleInTheGarage(string i_LicenseNumber)
        {
            if (m_Vehicles.ContainsKey(i_LicenseNumber))//if is in the garage
            {
                //throw new VehicleAlreadyExistsException(i_Vehicle.LicenseNumber);
            }
        }

        public void insertVehicleToGarage(GarageVehicle i_vehicle)
        {
            m_Vehicles.Add(i_vehicle.Vehicle.LicenseID, i_vehicle);
        }


    }
}
