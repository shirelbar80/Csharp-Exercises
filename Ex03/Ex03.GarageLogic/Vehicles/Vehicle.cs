using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly int r_NumberOfWheels;
        private readonly float r_MaxAirPressure;
        private string m_ModelName;
        private string m_LicenseID;
        private EnergySource m_EnergySource = null;
        private List<Wheel> m_Wheels = null;


        public Vehicle(string i_LicenseID, string i_ModelName, int i_NumberOfWheels, float i_MaxAirPressure)
        {
            m_ModelName = i_ModelName;
            m_LicenseID = i_LicenseID;
            r_NumberOfWheels = i_NumberOfWheels;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public int NumberOfWheels
        { 
           get { return r_NumberOfWheels; }           
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }


        public EnergySource EnergySource 
        {  get { return m_EnergySource; }
           set { m_EnergySource = value; } 
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicenseID
        {
            get { return m_LicenseID; }
            set { m_LicenseID = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public void setAllWheels(string i_Manufacturerfloat, float i_CurrentAirPressure)
        {
            for (int i = 0; i < r_NumberOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_Manufacturerfloat, i_CurrentAirPressure, r_MaxAirPressure));
            }

        }

        public abstract void setEnergySource(float i_EnergyPrecentageRemaining, float i_CurrentAmount);




    }
}
