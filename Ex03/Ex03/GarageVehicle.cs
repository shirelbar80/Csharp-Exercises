using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageVehicle
    {
        enum eVehicleStatus
        {
            InProgress, Fixed, Payed
        }


        private Vehicle m_Vehicle;
        string m_OwnerName;
        string m_OwnerPhone;
        eVehicleStatus m_VehicleStatus;

        public GarageVehicle()
        {
            
        }


    }
}
