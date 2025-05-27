using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.ConsoleUI
{
    public class VehicleUI
    {
        private GarageManager m_GarageManager;
        public VehicleUI(GarageManager m_GarageManager)
        {
            this.m_GarageManager = m_GarageManager;
        }
        public GarageVehicle AddVehicleToGarage()
        {
            Console.Write("Enter license number: ");
            string licenseNumber = Console.ReadLine();
            if (licenseNumber.Length != 9)
            {
                throw new FormatException("License number must be exactly 9 characters.");
            }
            if (m_GarageManager.isVehicleInTheGarage(licenseNumber))
            {
                throw new VehicleInTheGarageException(licenseNumber, true, $"Vehicle {licenseNumber} already exists in the garage.");
            }

            Console.WriteLine("What type of vehicle would you like to insert the garage?");
            string vehicleType = Console.ReadLine();

            Console.Write("Enter model name: ");
            string modelName = Console.ReadLine();

            //creating the car here 
            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseNumber, modelName);
            if (vehicle == null)      //type not supported
            {
                throw new ArgumentException($"Vehicle type '{vehicleType}' is not supported by the system.");
            }

            setWheelsFromUser(vehicle);

            Console.Write("Enter owner name: ");
            string ownerName = Console.ReadLine();

            Console.Write("Enter phone number: ");
            string phoneNumber = Console.ReadLine();
            checkPhoneNumberIsValid(phoneNumber);

            Console.WriteLine("Enter energy precentage remaining: ");
            float precentageOfEnergyRemaining = getDetailAndTryFloatParse();
            if (precentageOfEnergyRemaining < 0 || precentageOfEnergyRemaining > 100)
            {
                throw new ValueRangeException(100, 0, "Precentage value should be between 0 and 100.");
            }

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
            else if (vehicle is Truck truck)
            {
                getDetailsAndSetTruck(truck);
            }

            GarageVehicle vehicleToInsertToGarage = new GarageVehicle(vehicle, ownerName, phoneNumber);


            return vehicleToInsertToGarage;
        }

        public void setWheelsFromUser(Vehicle i_Vehicle)
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

        public void getDetailsAndSetMotorcycle(Motorcycle i_Motorcycle)
        {
            Console.WriteLine("Enter license type: ");
            string licenseType = Console.ReadLine();
            if (!Enum.TryParse(licenseType, out eMotorcycleLicenseType LicenseType))
            {
                throw new FormatException("Invalid motorcycle license type.");
            }
            i_Motorcycle.LicenseType = LicenseType;


            Console.WriteLine("Enter engine volume: ");
            string engineVolume = Console.ReadLine();
            if (!int.TryParse(engineVolume, out int numberOfEngineVolume))
            {
                throw new FormatException("Invalid engine volume.");
            }
            if (numberOfEngineVolume < 0)
            {
                throw new ValueRangeException(float.MaxValue, 0, "Engine volume cannot be a negetive number.");
            }
            i_Motorcycle.EngineVolume = numberOfEngineVolume;


        }

        public void getDetailsAndSetCar(Car i_Car)
        {

            Console.WriteLine("Enter car colour: ");
            string colour = Console.ReadLine();
            if (!Enum.TryParse(colour, out eCarColours ColourType))
            {
                throw new FormatException("Invalid car colour.");
            }
            i_Car.CarColor = ColourType;

            Console.WriteLine("Enter the amount of doors the car has: ");
            string doors = Console.ReadLine();
            if (!Enum.TryParse(doors, out eDoorsAmount DoorsNumber))
            {
                throw new FormatException("Invalid doors amount.");
            }
            i_Car.DoorsAmount = DoorsNumber;


        }

        public void getDetailsAndSetTruck(Truck i_truck)
        {

            Console.WriteLine("Do you have dangerous cargo?: ");
            string dangerousCargo = Console.ReadLine();
            if (!bool.TryParse(dangerousCargo, out bool containsDangerousCargo))
            {
                throw new FormatException("Invalid input, cannot tell if it has a dangerous cargo.");
            }
            i_truck.DangeresCargo = containsDangerousCargo;

            Console.WriteLine("Enter cargo volume: ");
            string cargoVolume = Console.ReadLine();
            if (!float.TryParse(cargoVolume, out float cargoVolumeAmount))
            {
                throw new FormatException("Invalid cargo volume.");
            }
            if (cargoVolumeAmount < 0)
            {
                throw new ValueRangeException(float.MaxValue, 0, "Cargo volume cannot be a negetive number.");
            }
            i_truck.CargoVolume = cargoVolumeAmount;



        }



        public void loadVehiclesFromFile()
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

                    GarageVehicle newVehicle = new GarageVehicle(vehicle, ownerName, ownerPhone);
                    m_GarageManager.InsertVehicleToGarage(newVehicle);
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Error loading line: {vehicleDetails}\n{exception.Message}");
                }
            }

            Console.WriteLine("Vehicles loaded successfully from DB.");
        }

        public void checkPhoneNumberIsValid(string i_PhoneNumber)
        {
            if (i_PhoneNumber.Length != 11)
            {
                throw new FormatException("Phone number must be exactly 11 characters long in the format XXX-XXXXXXX.");
            }

            // Check that the 4th character is a hyphen
            if (i_PhoneNumber[3] != '-')
            {
                throw new FormatException("Phone number must contain a hyphen after the first 3 digits.");
            }

            // Check that the first 3 characters and the last 7 characters are all digits
            string prefix = i_PhoneNumber.Substring(0, 3);
            string numberPart = i_PhoneNumber.Substring(4);

            if (!prefix.All(char.IsDigit) || !numberPart.All(char.IsDigit))
            {
                throw new FormatException("Phone number must contain only digits and one hyphen (e.g., 050-6564480).");
            }

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
