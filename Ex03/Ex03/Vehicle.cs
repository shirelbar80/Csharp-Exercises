using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        private string m_Name;
        private string m_PlateNumber;
        private EnergySource m_EnergySource;
        List<Wheel> m_Wheels;


        public Vehicle(string i_EnergySource)
        {
            if(i_EnergySource == "Fuel")
            {
                m_EnergySource = new Fuel();
            }
            else if(i_EnergySource == "Electric")
            {
                m_EnergySource = new Electric();
            }


        }

        public EnergySource EnergySource 
        {  get { return m_EnergySource; }
           set { m_EnergySource = value; } 
        }

    }
}
