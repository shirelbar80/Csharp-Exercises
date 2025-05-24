using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageVehicle
    {
        public enum eVehicleStatus
        {
            InProgress, Fixed, Payed
        }


        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eVehicleStatus m_VehicleStatus;

        public GarageVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_VehicleStatus = eVehicleStatus.InProgress;
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }
        public string OwnerPhone
        {
            get { return m_OwnerPhone; }
            set { m_OwnerPhone = value; }
        }
        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

    }
}
