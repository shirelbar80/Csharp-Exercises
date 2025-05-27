using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;


        public Wheel(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_Manufacturer = i_Manufacturer;
            m_CurrentAirPressure = i_CurrentAirPressure;
            m_MaxAirPressure = i_MaxAirPressure;
        }



        public void InflateWheels(float i_AmountToFill)
        {
           //Exception maybe
            if(m_CurrentAirPressure + i_AmountToFill <= MaxAirPressure)
            {
                m_CurrentAirPressure += i_AmountToFill;

            }
            else
            {
                throw new ValueRangeException(MaxAirPressure, 0, $"Air pressure should be between 0 and {MaxAirPressure}.");
            }


        }

        public string Manufacturer
        {
            get { return m_Manufacturer; }
            set { m_Manufacturer = value; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }
            set { m_MaxAirPressure = value; }
        }







    }
}
