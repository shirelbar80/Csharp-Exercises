using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        Dictionary<string,GarageVehicle> m_Vehicles = null;//list of vehicles in the garage

        public GarageManager()
        {
            m_Vehicles = new Dictionary<string,GarageVehicle>();//create dictionary
        }

        public void isVehicleInTheGarage(string i_LicenseNumber)
        {
            if (m_Vehicles.ContainsKey(i_LicenseNumber))//if is in the garage
            {
                //throw new VehicleAlreadyExistsException(i_Vehicle.LicenseNumber);
            }
        }

        public void InsertVehicleToGarage(GarageVehicle i_vehicle)
        {
            m_Vehicles.Add(i_vehicle.Vehicle.LicenseID, i_vehicle);
        }

        public List<string> GetLicenseNumbersFromGarage(GarageVehicle.eVehicleStatus? i_FilterStatus = null) //nullable
        {
            List<string> licenseNumbers = new List<string>();

            foreach (GarageVehicle garageVehicle in m_Vehicles.Values)
            {
                if (!i_FilterStatus.HasValue || garageVehicle.VehicleStatus == i_FilterStatus.Value)
                {
                    licenseNumbers.Add(garageVehicle.Vehicle.LicenseID);
                }
            }

            return licenseNumbers;
        }

        public void changeStatusOfAnExistingVehicleInTheGarage(string i_LicenseNumber, GarageVehicle.eVehicleStatus newStatus)
        {
            m_Vehicles[i_LicenseNumber].VehicleStatus = newStatus;
            //catch exception KeyNotFoundException
        }


    }
}
