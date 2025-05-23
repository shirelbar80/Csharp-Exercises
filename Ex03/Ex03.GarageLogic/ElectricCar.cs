using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricCar : Car
    {
        public ElectricCar(string i_EnergySource, string i_ModelName) : base(i_EnergySource)
        {
        }
    }
}
