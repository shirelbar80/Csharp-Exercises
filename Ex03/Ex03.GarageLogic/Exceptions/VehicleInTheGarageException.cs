using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleInTheGarageException : Exception
    {
        private readonly string m_LicenseID;
        private bool m_VehicleIsInTheGarage;

        public VehicleInTheGarageException(string i_LicenseID, bool i_VehicleIsInTheGarage, string i_Message) : base(i_Message)
        {
            m_VehicleIsInTheGarage = i_VehicleIsInTheGarage;
            m_LicenseID = i_LicenseID;    
        }


        public string LicenseID
        {
            get { return m_LicenseID; }
        }

        public bool VehicleIsInTheGarage
        {
            get { return m_VehicleIsInTheGarage;}
        }

    }
}
