using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {

        private const float k_MaxBatteryTime = 3.2f;

        public ElectricMotorcycle(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
        }

        public override void setEnergySource(float i_EnergyPrecentageRemaining, float i_CurrentAmount)
        {
            EnergySource = new Electric(i_EnergyPrecentageRemaining, i_CurrentAmount, k_MaxBatteryTime);
        }
    }
}
