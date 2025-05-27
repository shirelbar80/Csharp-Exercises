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

        public bool isVehicleInTheGarage(string i_LicenseNumber)
        {
            return m_Vehicles.ContainsKey(i_LicenseNumber);//if is in the garage
            
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


        public void inflateTiresAirPressureToMax(string i_LicenseNumber)
        {

            Vehicle i_CurrentVehicle = m_Vehicles[i_LicenseNumber].Vehicle;

            foreach (Wheel wheel in i_CurrentVehicle.Wheels)
            {
                float amountToAdd = i_CurrentVehicle.MaxAirPressure - wheel.CurrentAirPressure;
                if (amountToAdd > 0)
                {
                    wheel.InflateWheels(amountToAdd);
                }
            }

        }


        public void fillFuelBasedVehicle(string i_LicenseNumber,Fuel.eFuelType i_fuelType,float i_amountToFill)
        {

            Vehicle i_CurrentVehicle = m_Vehicles[i_LicenseNumber].Vehicle;

            Fuel fuel = i_CurrentVehicle.EnergySource as Fuel;
            fuel.Refuel(i_fuelType, i_amountToFill);
            

        }

        public void chargeElectricCar(string i_LicenseNumber, float i_MinutesToCharge)
        {

            Vehicle i_CurrentVehicle = m_Vehicles[i_LicenseNumber].Vehicle;

            Electric electric = i_CurrentVehicle.EnergySource as Electric;
            electric.Recharge(i_MinutesToCharge);
        }

        public string GetVehicleData(string i_LicenseNumber)
        {
            GarageVehicle currentGarageVehicle = m_Vehicles[i_LicenseNumber];
            Vehicle currentVehicle = currentGarageVehicle.Vehicle;
            StringBuilder vehicleData = new StringBuilder();

            vehicleData.AppendLine($"License Number: {currentVehicle.LicenseID}");
            vehicleData.AppendLine($"Model Name: {currentVehicle.ModelName}");
            vehicleData.AppendLine($"Owner Name: {currentGarageVehicle.OwnerName}");
            vehicleData.AppendLine($"Owner Phone: {currentGarageVehicle.OwnerPhone}");
            vehicleData.AppendLine($"Garage Status: {currentGarageVehicle.VehicleStatus}");

            vehicleData.AppendLine("Wheels:");
            foreach (Wheel wheel in currentVehicle.Wheels)
            {
                vehicleData.AppendLine($"  Manufacturer: {wheel.Manufacturer}, Air Pressure: {wheel.CurrentAirPressure}/{wheel.MaxAirPressure}");
            }

            if (currentVehicle.EnergySource is Fuel fuel)
            {
                vehicleData.AppendLine($"Fuel Type: {fuel.FuelType}");
                vehicleData.AppendLine($"Fuel Level: {fuel.CurrentAmount}/{fuel.MaxAmount}");
            }
            else if (currentVehicle.EnergySource is Electric electric)
            {
                vehicleData.AppendLine($"Battery Level: {electric.CurrentAmount}/{electric.MaxAmount} hours");
            }

            if (currentVehicle is Car car)
            {
                vehicleData.AppendLine($"Car Color: {car.CarColor}");
                vehicleData.AppendLine($"Number of Doors: {car.DoorsAmount}");
            }
            else if (currentVehicle is Motorcycle motorcycle)
            {
                vehicleData.AppendLine($"License Type: {motorcycle.LicenseType}");
                vehicleData.AppendLine($"Engine Volume: {motorcycle.EngineVolume}");
            }
            else if (currentVehicle is Truck truck)
            {
                vehicleData.AppendLine($"Carries Dangerous Materials: {truck.DangeresCargo}");
                vehicleData.AppendLine($"Cargo Volume: {truck.CargoVolume}");
            }

            return vehicleData.ToString();
        }

    }
}
