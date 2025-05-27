using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic.Exceptions
{
    public class ValueRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;



        public ValueRangeException(float i_MaxValue, float i_MinValue, string i_Message) : base(i_Message)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }








    }
}
