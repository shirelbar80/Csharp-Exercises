using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Fuel;

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
                        AddVehicle();
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

        public void AddVehicle()
        {
            Console.Write("Enter license number: ");
            string licenseNumber = Console.ReadLine();

            m_GarageManager.isVehicleInTheGarage(licenseNumber);
            //will throw an excemption or will continue to add the vehicle

            Console.WriteLine("What type of vehicle would you like to insert the garage?");
            string vehicleType = Console.ReadLine();
            //add exception

            Console.Write("Enter model name: ");
            string modelName = Console.ReadLine();


            Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseNumber, modelName);
            //check if null -> exception

            setWheelsFromUser(vehicle);


            Console.WriteLine("Enter energy precentage remaining: ");
            float precentageOfEnergyRemaining = getDetailAndTryFloatParse();

            Console.WriteLine("Enter current energy amount: ");
            float currentEnergyAmount = getDetailAndTryFloatParse();

            vehicle.setEnergySource(precentageOfEnergyRemaining, currentEnergyAmount);

            //need to set the unique variables of the vehicles
            if (vehicleType.ToLower().Contains("motorcycle"))//motorcycle
            {

            }
            else if(vehicleType.ToLower().Contains("car"))   //car
            {
                 
            }
            else//truck
            {
                
            }









            Console.WriteLine("Vehicle added.");
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
        private void getDetailsAndSetMotorcycle()
        {
            Console.WriteLine("Enter license type: ");
            string licenseType = Console.ReadLine();
            //add exception



            Console.WriteLine("Enter engine volume: ");
            string engineVolume = Console.ReadLine();
            if (!float.TryParse(engineVolume, out float numberOfEngineVolume))
            {
                //add exception - did not succeeded parse
            }

            




        }

        private void getDetailsAndSetCar()
        {



        }

        private void getDetailsAndSetTruck()
        {






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

        






    }



}

