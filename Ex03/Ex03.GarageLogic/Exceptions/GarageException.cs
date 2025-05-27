using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleAlreadyExistsException : Exception
    {
        private readonly string m_LicenseID;

        public VehicleAlreadyExistsException(string i_LicenseID) : base("Vehicle already exists in the garage.")
        {
            m_LicenseID = i_LicenseID;    
        }


        public string LicenseID
        {
            get { return m_LicenseID; }
        }

    }
}
