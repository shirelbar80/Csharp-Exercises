using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.Fuel;

namespace Ex03.ConsoleUI
{
    public class GarageActions
    {
        private GarageManager m_GarageManager;


        public GarageActions(GarageManager m_garageManager)
        {
            m_GarageManager = m_garageManager;     
        }

        public void presentLicenseNumbersOfVehiclesInTheGarage()
        {
            List<string> listOfLicenseNumbers = m_GarageManager.GetLicenseNumbersFromGarage();

            printListOfStrings(listOfLicenseNumbers);

            Console.WriteLine("Whould you like to filter by status? (Y/N)");
            string userWantToFilterByStatus = Console.ReadLine().ToLower();

            if (userWantToFilterByStatus == "y")
            {
                Console.WriteLine("Which status would you like to filter by? (InProgress, Fixed, Payed)");
                string filterStatusBy = Console.ReadLine();

                if (!Enum.TryParse(filterStatusBy, out GarageVehicle.eVehicleStatus vehicleStatus))
                {
                    throw new FormatException($"Vehicle status '{filterStatusBy}' is invalid.");
                }

                listOfLicenseNumbers = m_GarageManager.GetLicenseNumbersFromGarage(vehicleStatus);
                printListOfStrings(listOfLicenseNumbers);
            }
            else if (userWantToFilterByStatus == "n")
            {//nothing
            }
            else
            {
                throw new FormatException("Invalid input.");
            }

        }

        public void printListOfStrings(List<string> i_listOfStrings)
        {
            foreach (string sentence in i_listOfStrings) //print all list
            {
                Console.WriteLine(sentence);
            }
        }


        public void changeExistingVehicleStatus()
        {
            Console.WriteLine("What is the license number?");
            string vehicleLicenseNumber = Console.ReadLine();
            Console.WriteLine("What the new status? (InProgress, Fixed, Payed)");
            string newStatus = Console.ReadLine();
            if (!Enum.TryParse(newStatus, out GarageVehicle.eVehicleStatus vehicleStatus))
            {
                throw new FormatException($"Vehicle status '{newStatus}' is invalid.");
            }
            m_GarageManager.changeStatusOfAnExistingVehicleInTheGarage(vehicleLicenseNumber, vehicleStatus);


        }

        public void inflateWheels()
        {

            Console.WriteLine("What is the license number?");
            string vehicleLicenseNumber = Console.ReadLine();
            if (!m_GarageManager.isVehicleInTheGarage(vehicleLicenseNumber))
            {
                throw new VehicleInTheGarageException(vehicleLicenseNumber, false, $"Vehicle {vehicleLicenseNumber} doesn't exists in the garage.");
            }
            m_GarageManager.inflateTiresAirPressureToMax(vehicleLicenseNumber);
        }

        public void fillFuelVehicle()
        {
            Console.WriteLine("What is the license number?");
            string vehicleLicenseNumber = Console.ReadLine();
            Console.WriteLine("Enter the desired fuel type:");
            string fuelType = Console.ReadLine();
            if (!Enum.TryParse(fuelType, out eFuelType vehicleFuelType))
            {
                throw new FormatException($"Vehicle fuel type '{fuelType}' is invalid.");
            }
            Console.WriteLine("What is the amount you want to fill?");
            float amountToFillFuelCar = getDetailAndTryFloatParse();
            m_GarageManager.fillFuelBasedVehicle(vehicleLicenseNumber, vehicleFuelType, amountToFillFuelCar);

        }


        public void chargeElectricVehicle()
        {
            Console.WriteLine("What is the license number?");
            string vehicleLicenseNumber = Console.ReadLine();
            if (!m_GarageManager.isVehicleInTheGarage(vehicleLicenseNumber))
            {
                throw new VehicleInTheGarageException(vehicleLicenseNumber, false, $"Vehicle {vehicleLicenseNumber} doesn't exists in the garage.");
            }

            Console.WriteLine("What is the amount of minutes you want to fill?");

            float amountToFillElectricCar = getDetailAndTryFloatParse();
            m_GarageManager.chargeElectricCar(vehicleLicenseNumber, amountToFillElectricCar);
        }

        public void getDetailesOfVehicle()
        {
            Console.WriteLine("What is the license number?");
            string vehicleLicenseNumber = Console.ReadLine();
            if (!m_GarageManager.isVehicleInTheGarage(vehicleLicenseNumber))
            {
                throw new VehicleInTheGarageException(vehicleLicenseNumber, false, $"Vehicle {vehicleLicenseNumber} doesn't exists in the garage.");
            }

            string vehicleData = m_GarageManager.GetVehicleData(vehicleLicenseNumber);
            Console.WriteLine("=== Vehicle Information ===");
            Console.WriteLine(vehicleData);

        }
        public float getDetailAndTryFloatParse()
        {
            string detailFromUser = Console.ReadLine();

            if (!float.TryParse(detailFromUser, out float numberOfDetailFromUser))
            {
                throw new FormatException("Input is not in a valid float format.");
            }

            return numberOfDetailFromUser;
        }

    }
}
