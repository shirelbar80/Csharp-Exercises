using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.Fuel;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.ConsoleUI
{
    public class UIManager
    {
        private GarageManager m_GarageManager = new GarageManager();

        public void Run()
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
                        //AddVehicle();
                        break;
                    case "2":
                        GarageVehicle newVehicle = AddVehicleToGarage();
                        m_GarageManager.insertVehicleToGarage(newVehicle);
                        break;
                    case "3":
                        //InflateWheels();
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

        public GarageVehicle AddVehicleToGarage()
        {
            Console.Write("Enter license number: ");
            string licenseNumber = Console.ReadLine();

            m_GarageManager.isVehicleInTheGarage(licenseNumber);
            //will throw an excemption or will continue to add the vehicle
            //if it already exists we need to make it in progress

            Console.WriteLine("What type of vehicle would you like to insert the garage?");
            string vehicleType = Console.ReadLine();
            //add exception

            Console.Write("Enter model name: ");
            string modelName = Console.ReadLine();

            //creating the car here 
            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseNumber, modelName);
            //check if null -> exception

            setWheelsFromUser(vehicle);

            Console.Write("Enter owner name: ");
            string ownerName = Console.ReadLine();

            Console.Write("Enter phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.WriteLine("Enter energy precentage remaining: ");
            float precentageOfEnergyRemaining = getDetailAndTryFloatParse();

            Console.WriteLine("Enter current energy amount: ");
            float currentEnergyAmount = getDetailAndTryFloatParse();

            vehicle.setEnergySource(precentageOfEnergyRemaining, currentEnergyAmount);

            //need to set the unique variables of the vehicles
            if (vehicle is Motorcycle motorcycle)//motorcycle
            {
                getDetailsAndSetMotorcycle(motorcycle);
            }
            else if (vehicle is Car car)   //car
            {
                getDetailsAndSetCar(car);
            }
            else if(vehicle is Truck truck)
            {
                getDetailsAndSetTruck(truck);
            }

            GarageVehicle vehicleToInsertToGarage = new GarageVehicle(vehicle, ownerName, phoneNumber);

            //delete this later


            Console.WriteLine("Vehicle added.");
            return vehicleToInsertToGarage;
        }

        private void setWheelsFromUser(Vehicle i_Vehicle)
        {
            List<Wheel> wheelsSet = new List<Wheel>();

            for (int i = 0; i < i_Vehicle.NumberOfWheels; i++)
            {
                Console.Write("For wheel number " + i + ":");
                Console.Write("Enter tier model: ");
                string tierModel = Console.ReadLine();

                Console.WriteLine("Enter current air pressure: ");
                float currentAirPressure = getDetailAndTryFloatParse();

                wheelsSet.Add(new Wheel(tierModel, currentAirPressure, i_Vehicle.MaxAirPressure));
            }

            i_Vehicle.Wheels = wheelsSet;
        }
        private void getDetailsAndSetMotorcycle(Motorcycle i_Motorcycle)
        {
            Console.WriteLine("Enter license type: ");
            string licenseType = Console.ReadLine();
            //add exception

            eMotorcycleLicenseType LicenseType;
            if (!Enum.TryParse(licenseType, out LicenseType))
            {
                //add exception
            }
            i_Motorcycle.LicenseType = LicenseType;


            Console.WriteLine("Enter engine volume: ");
            string engineVolume = Console.ReadLine();
            if (!int.TryParse(engineVolume, out int numberOfEngineVolume))
            {
                //add exception - did not succeeded parse
            }
            i_Motorcycle.EngineVolume = numberOfEngineVolume;


        }

        private void getDetailsAndSetCar(Car i_Car)
        {

            Console.WriteLine("Enter colour type: ");
            string colour = Console.ReadLine();
            eCarColours ColourType;
            if (!Enum.TryParse(colour, out ColourType))
            {
                //add exception
            }
            i_Car.CarColor = ColourType;

            Console.WriteLine("Enter colour type: ");
            string doors = Console.ReadLine();
            eDoorsAmount DoorsNumber;
            if (!Enum.TryParse(doors, out DoorsNumber))
            {
                //add exception
            }
            i_Car.DoorsAmount = DoorsNumber;


        }

        private void getDetailsAndSetTruck(Truck i_truck)
        {

            Console.WriteLine("Do you have dangerous cargo?: ");
            string dangerousCargo = Console.ReadLine();

            if (!bool.TryParse(dangerousCargo, out bool containsDangerousCargo))
            {
                //add exception
            }
            i_truck.DangeresCargo = containsDangerousCargo;

            Console.WriteLine("Enter cargo volume: ");
            string cargoVolume = Console.ReadLine();
            if (!float.TryParse(cargoVolume, out float cargoVolumeAmount))
            {
                //add exception
            }
            i_truck.CargoVolume = cargoVolumeAmount;



        }

        private float getDetailAndTryFloatParse()
        {
            string detailFromUser = Console.ReadLine();

            if (!float.TryParse(detailFromUser, out float numberOfDetailFromUser))
            {
                //add exception - did not succeeded parse
            }

            return numberOfDetailFromUser;
        }

        public void LoadVehiclesFromFile()
        {
            //add exceptions to all cases
            string[] vehiclesDetailsFromFile = File.ReadAllLines("Vehicles.db");

            foreach (string vehicleDetails in vehiclesDetailsFromFile)
            {
                try
                {
                    string[] parts = vehicleDetails.Split(',');

                    string vehicleType = parts[0];
                    string licensePlate = parts[1];
                    string modelName = parts[2];
                    float energyPercentage = float.Parse(parts[3]);
                    string tierModel = parts[4];
                    float currAirPressure = float.Parse(parts[5]);
                    string ownerName = parts[6];
                    string ownerPhone = parts[7];

                    Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licensePlate, modelName);
                    vehicle.setAllWheels(tierModel, currAirPressure);
                    vehicle.setEnergySource(energyPercentage, 0); // temp current amount, updated below

                    if (vehicle is Car car)
                    {
                        car.CarColor = (Car.eCarColours)Enum.Parse(typeof(Car.eCarColours), parts[8]);
                        car.DoorsAmount = (Car.eDoorsAmount)int.Parse(parts[9]);
                    }
                    else if (vehicle is Motorcycle motorcycle)
                    {
                        motorcycle.LicenseType = (Motorcycle.eMotorcycleLicenseType)Enum.Parse(typeof(Motorcycle.eMotorcycleLicenseType), parts[8]);
                        motorcycle.EngineVolume = int.Parse(parts[9]);
                    }
                    else if (vehicle is Truck truck)
                    {
                        truck.DangeresCargo = bool.Parse(parts[8]);
                        truck.CargoVolume = float.Parse(parts[9]);
                    }

                    GarageVehicle newVehicle = new GarageVehicle(vehicle, licensePlate, modelName);
                    m_GarageManager.insertVehicleToGarage(newVehicle);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading line: {vehicleDetails}\n{ex.Message}");
                }
            }

            Console.WriteLine("Vehicles loaded successfully from DB.");
        }
    } 

    }

