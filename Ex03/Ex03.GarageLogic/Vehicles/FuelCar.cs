using Ex03.GarageLogic.Exceptions;
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
            if (i_CurrentAmount < 0 || i_CurrentAmount > k_MaxFuelAmount)
            {
                throw new ValueRangeException(k_MaxFuelAmount, 0, $"Invalid current amount, should be between 0 and {k_MaxFuelAmount}");
            }
            EnergySource = new Fuel(k_FuelType, i_EnergyPrecentageRemaining, i_CurrentAmount, k_MaxFuelAmount);
        }
    }
}
