using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 12;
        private const float k_MaxAirPressure = 27;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Soler;
        private const float k_MaxFuelAmount = 135;


        private float m_CargoVolume;
        private bool m_DangeresCargo;

        public Truck(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName, k_NumberOfWheels, k_MaxAirPressure)
        {
        }

        public override void setEnergySource(float i_EnergyPrecentageRemaining, float i_CurrentAmount)
        {
            EnergySource = new Fuel(k_FuelType, i_EnergyPrecentageRemaining, i_CurrentAmount, k_MaxFuelAmount);
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }

        public bool DangeresCargo
        {
            get { return m_DangeresCargo; }
            set { m_DangeresCargo = value; }
        }
    }
}
