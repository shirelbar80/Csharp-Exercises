using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Fuel : EnergySource
    {
        public enum eFuelType
        {
            Octan96,
            Octan95,
            Octan98,
            Soler
        }

        eFuelType m_FuelType;


        public override void FillEnergySource(float i_AmountOfEnergyToFill)
        {

        }



    }
}
