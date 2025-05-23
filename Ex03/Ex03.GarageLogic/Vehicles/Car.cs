using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Car : Vehicle
    {
        enum eCarColours
        {
            Yellow, Black, White, Silver
        }

        enum eDoorsAmount
        {
            Two = 2, 
            Three = 3,
            Four = 4,
            Five = 5
        }

        private const int k_NumberOfWheels = 5;
        private const float k_DefualtMaxAirPressure = 27;
        private eCarColours m_CarColor;
        private eDoorsAmount m_DoorsAmount;

        public Car(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName, k_NumberOfWheels, k_DefualtMaxAirPressure)
        {
            
        }

        
    }
}
