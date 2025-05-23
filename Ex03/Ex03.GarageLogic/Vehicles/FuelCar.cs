using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelCar : Car
    {
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan95;
        private const float k_MaxFuelAmount = 48;



        public FuelCar(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
        }

        public override void setEnergySource(float i_EnergyPrecentageRemaining, float i_CurrentAmount)
        {
            EnergySource = new Fuel(k_FuelType, i_EnergyPrecentageRemaining, i_CurrentAmount, k_MaxFuelAmount);
        }
    }
}
