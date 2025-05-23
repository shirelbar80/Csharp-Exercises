using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Fuel;

namespace Ex03.ConsoleUI
{
    public class UIManager
    {
        private GarageManager m_GarageManager = new GarageManager();

        public static void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=== Garage Menu ===");
                Console.WriteLine("1. Load from the database");
                Console.WriteLine("2. Enter new vehicle");
                Console.WriteLine("3. Show list of vehicles' license plate");
                Console.WriteLine("4. Change vehicle status");
                Console.WriteLine("5. Inflate wheels to maximum");
                Console.WriteLine("6. Fill fuelvehicle");
                Console.WriteLine("7. Charge electric vehicle");
                Console.WriteLine("8. Show vehicle information");
                Console.WriteLine("9. Exit");
                Console.Write("Choose: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddVehicle();
                        break;
                    case "2":
                        ListVehicles();
                        break;
                    case "3":
                        InflateWheels();
                        break;
                    case "4":
                        
                        break;
                    case "5":

                        break;
                    case "6":

                        break;
                    case "7":

                        break;
                    case "8":

                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        public void AddVehicle()
        {
            Console.Write("Enter license number: ");
            string vehicleLicenseNumber = Console.ReadLine();

            m_GarageManager.isVehicleInTheGarage(vehicleLicenseNumber); 
            //will throw an excemption or will continue to add the vehicle

            Console.WriteLine("What type of vehicle would you like to insert the garage?");
            Console.WriteLine("1 - FuelCar");
            Console.WriteLine("2 - ElectricCar");
            Console.WriteLine("3 - FuelMotorcycle");
            Console.WriteLine("4 - ElectricMotorcycle");
            Console.WriteLine("5 - Truck");
            string vehicleType = Console.ReadLine();

            Console.WriteLine("What is the status of the car? (In Progress, Fixed, Payed)");
            string vehicleStatus = Console.ReadLine();


          





            Console.WriteLine("Vehicle added.");
        }

        /*private static void ListVehicles()
        {
            Console.WriteLine("Vehicles in garage:");
            foreach (var license in s_GarageManager.GetAllLicenseNumbers())
            {
                Console.WriteLine(license);
            }
        }

        private static void InflateWheels()
        {
            Console.Write("Enter license number: ");
            string license = Console.ReadLine();
            s_GarageManager.InflateWheelsToMax(license);
            Console.WriteLine("Wheels inflated.");
        }*/
    }


}

