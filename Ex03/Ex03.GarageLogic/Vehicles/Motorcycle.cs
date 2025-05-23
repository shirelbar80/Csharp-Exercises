using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Motorcycle : Vehicle
    {
        public enum eMotorcycleLicenseType
        {
            A, A2, AB, B2
        }

        private const int k_NumberOfWheels = 2;
        private const float k_MaxAirPressure = 30;

        private eMotorcycleLicenseType m_LicenseType;
        private int m_EngineVolume;

        public eMotorcycleLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineVolume
        {
            get { return m_EngineVolume; }
            set { m_EngineVolume = value; }
        }

        public Motorcycle(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName, k_NumberOfWheels, k_MaxAirPressure)
        {
        }

        



       

    }
}
