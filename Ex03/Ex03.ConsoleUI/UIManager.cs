using Ex03.GarageLogic;
using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
                Console.WriteLine("3. Show list of vehicles' license plate with an option to filter by the status of the vehicles");
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
                        try
                        {
                            loadVehiclesFromFile();
                        }
                        catch(ValueRangeException valueRangeException)
                        {
                            Console.WriteLine(valueRangeException.Message); //show message to the user
                        }
                        break;
                    case "2":
                        try
                        {
                            GarageVehicle newVehicle = AddVehicleToGarage();
                            m_GarageManager.InsertVehicleToGarage(newVehicle);
                        }
                        catch (VehicleInTheGarageException vehicleInTheGarageException)
                        {
                            Console.WriteLine(vehicleInTheGarageException.Message); //show message to the user

                            if (vehicleInTheGarageException.VehicleIsInTheGarage)  //vehicle is in the garage
                            {
                                m_GarageManager.changeStatusOfAnExistingVehicleInTheGarage(vehicleInTheGarageException.LicenseID, GarageVehicle.eVehicleStatus.InProgress);//change status to InProgress
                            }
                        }
                        catch (ArgumentException argumentException)
                        {
                            Console.WriteLine(argumentException.Message); //show message to the user
                        }
                        catch (FormatException formatException)
                        {
                            Console.WriteLine(formatException.Message); //show message to the user

                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message); //show message to the user
                        }
                            break;
                    case "3":
                        try
                        {
                            presentLicenseNumbersOfVehiclesInTheGarage();
                        }
                        catch(FormatException formatException)
                        {
                            Console.WriteLine(formatException.Message); //show message to the user
                        }

                        break;
                    case "4":
                        changeExistingVehicleStatus();
                        break;
                    case "5":
                        inflateWheels();
                        break;
                    case "6":
                        fillFuelVehicle();
                        break;
                    case "7":
                        chargeElectricVehicle();
                        break;
                    case "8":
                        getDetailesOfVehicle();
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
            if (licenseNumber.Length != 9)
            {
                throw new FormatException("License number must be exactly 9 characters.");
            }
            if (m_GarageManager.isVehicleInTheGarage(licenseNumber))
            {
                throw new VehicleInTheGarageException(licenseNumber,true, $"Vehicle {licenseNumber} already exists in the garage.");
            }
            
            Console.WriteLine("What type of vehicle would you like to insert the garage?");
            string vehicleType = Console.ReadLine();

            Console.Write("Enter model name: ");
            string modelName = Console.ReadLine();

            //creating the car here 
            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseNumber, modelName);
            if(vehicle == null)      //type not supported
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
            if(precentageOfEnergyRemaining < 0 ||  precentageOfEnergyRemaining > 100)
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
            else if(vehicle is Truck truck)
            {
                getDetailsAndSetTruck(truck);
            }

            GarageVehicle vehicleToInsertToGarage = new GarageVehicle(vehicle, ownerName, phoneNumber);

        
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

        private void getDetailsAndSetCar(Car i_Car)
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

        private void getDetailsAndSetTruck(Truck i_truck)
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
            if(cargoVolumeAmount < 0)
            {
                throw new ValueRangeException(float.MaxValue, 0, "Cargo volume cannot be a negetive number.");
            }
            i_truck.CargoVolume = cargoVolumeAmount;



        }

        private float getDetailAndTryFloatParse()
        {
            string detailFromUser = Console.ReadLine();

            if (!float.TryParse(detailFromUser, out float numberOfDetailFromUser))
            {
                throw new FormatException("Input is not in a valid float format.");
            }

            return numberOfDetailFromUser;
        }

        private void loadVehiclesFromFile()
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

        private void presentLicenseNumbersOfVehiclesInTheGarage()
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
            else if(userWantToFilterByStatus == "n")
            {//nothing
            }
            else
            {
                throw new FormatException("Invalid input.");
            }

        } 


        private void printListOfStrings(List<string> i_listOfStrings)
        {
            foreach (string sentence in i_listOfStrings) //print all list
            {
                Console.WriteLine(sentence);
            }
        }

        private void changeExistingVehicleStatus()
        {
            Console.WriteLine("What is the license number?");
            string vehicleLicenseNumber = Console.ReadLine();
            Console.WriteLine("What the new status? (InProgress, Fixed, Payed)");
            string newStatus = Console.ReadLine();
            if(!Enum.TryParse(newStatus, out GarageVehicle.eVehicleStatus vehicleStatus)){
                throw new FormatException($"Vehicle status '{newStatus}' is invalid.");
            }
            m_GarageManager.changeStatusOfAnExistingVehicleInTheGarage(vehicleLicenseNumber, vehicleStatus);


        }

        private void inflateWheels()
        {

            Console.WriteLine("What is the license number?");
            string vehicleLicenseNumber = Console.ReadLine();
            if (!m_GarageManager.isVehicleInTheGarage(vehicleLicenseNumber))
            {
                throw new VehicleInTheGarageException(vehicleLicenseNumber, false, $"Vehicle {vehicleLicenseNumber} doesn't exists in the garage.");
            }
            m_GarageManager.inflateTiresAirPressureToMax(vehicleLicenseNumber);
        }

        private void fillFuelVehicle()
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


        private void chargeElectricVehicle()
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

        private void getDetailesOfVehicle()
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

        private void checkPhoneNumberIsValid(string i_PhoneNumber)
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

    }
}

