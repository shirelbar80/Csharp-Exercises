using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        float m_CargoVolume;
        bool m_DangeresCargo;

        public Truck(string i_EnergySource, string i_ModelName) : base(i_EnergySource)
        {
        }
    }
}
