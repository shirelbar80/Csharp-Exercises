using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxBatteryTime = 4.8f;                       
        public ElectricCar(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName){}

        public override void setEnergySource(float i_EnergyPrecentageRemaining, float i_CurrentAmount)
        {
            if(i_CurrentAmount < 0 || i_CurrentAmount > k_MaxBatteryTime)
            {
                throw new ValueRangeException(k_MaxBatteryTime, 0, $"Invalid current amount, should be between 0 and {k_MaxBatteryTime}");
            }

            EnergySource = new Electric(i_EnergyPrecentageRemaining, i_CurrentAmount, k_MaxBatteryTime);
        }


    }
}
