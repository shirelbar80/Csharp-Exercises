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
    public class UIController
    {
        private GarageManager m_GarageManager;
        private VehicleUI m_VehicleUI;
        private GarageActions m_Actions;
        
        public UIController(GarageManager garageManager, VehicleUI vehicleUI, GarageActions actions)
        {
            m_GarageManager = garageManager;
            m_VehicleUI = vehicleUI;
            m_Actions = actions;
        }


        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                printMenu();
                Console.Write("Choose: ");
                string chosedAction = Console.ReadLine();

                switch (chosedAction)
                {
                    case "1":
                        try
                        {
                            m_VehicleUI.loadVehiclesFromFile();
                        }
                        catch (ValueRangeException valueRangeException)
                        {
                            Console.WriteLine(valueRangeException.Message); //show message to the user
                            Console.WriteLine();
                        }
                        catch(FormatException formatException)
                        {
                            Console.WriteLine(formatException.Message); //show message to the user
                            Console.WriteLine();
                        }
                        catch(ArgumentException argumentException)
                        {
                            Console.WriteLine(argumentException.Message); //show message to the user
                            Console.WriteLine();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine($"Error loading line\n{exception.Message}");
                            Console.WriteLine();
                        }
                        break;
                    case "2":
                        try
                        {
                            GarageVehicle newVehicle = m_VehicleUI.AddVehicleToGarage();
                            m_GarageManager.InsertVehicleToGarage(newVehicle);
                        }
                        catch (VehicleInTheGarageException vehicleInTheGarageException)
                        {
                            Console.WriteLine(vehicleInTheGarageException.Message); //show message to the user
                            Console.WriteLine();

                            if (vehicleInTheGarageException.VehicleIsInTheGarage)  //vehicle is in the garage
                            {
                                m_GarageManager.changeStatusOfAnExistingVehicleInTheGarage(vehicleInTheGarageException.LicenseID, GarageVehicle.eVehicleStatus.InProgress);//change status to InProgress
                            }
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message); //show message to the user
                            Console.WriteLine();
                        }
                            break;
                    case "3":
                        try
                        {
                            m_Actions.presentLicenseNumbersOfVehiclesInTheGarage();
                        }
                        catch(Exception exception)
                        {
                            Console.WriteLine(exception.Message); //show message to the user
                            Console.WriteLine();
                        }

                        break;
                    case "4":
                        try
                        {
                            m_Actions.changeExistingVehicleStatus();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message); //show message to the user
                            Console.WriteLine();
                        }
                        break;
                    case "5":
                        try
                        {
                            m_Actions.inflateWheels();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message); //show message to the user
                            Console.WriteLine();
                        }
                        break;
                    case "6":
                        try
                        {
                            m_Actions.fillFuelVehicle();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message); //show message to the user
                            Console.WriteLine();
                        }
                        break;
                    case "7":
                        try
                        {
                            m_Actions.chargeElectricVehicle();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message); //show message to the user
                            Console.WriteLine();
                        }
                        break;
                    case "8":
                        try
                        {
                            m_Actions.getDetailesOfVehicle();
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message); //show message to the user
                            Console.WriteLine();
                        }
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        private void printMenu()
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
        }

       

    }
}

