using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        private float m_CurrentAmount;
        private float m_MaxAmount;
        private float m_EnergyPrecentageRemaining;

        public EnergySource(float i_EnergyPrecentageRemaining, float i_CurrentAmount, float i_MaxAmount)
        {
            m_EnergyPrecentageRemaining = i_EnergyPrecentageRemaining;
            m_CurrentAmount = i_CurrentAmount; 
            m_MaxAmount = i_MaxAmount;
        }

        protected void AddEnergy(float i_Amount)
        {
            m_CurrentAmount += i_Amount;
            m_EnergyPrecentageRemaining = (float)(m_CurrentAmount / m_MaxAmount) * 100;
        }

        public float CurrentAmount
        {
            get { return m_CurrentAmount; }
            set { m_CurrentAmount = value; }
        }

        public float MaxAmount
        {
            get { return m_MaxAmount; }
            set { m_MaxAmount = value; }
        }

        public float EnergyPrecentageRemaining
        {
            get { return m_EnergyPrecentageRemaining; }
            set { m_EnergyPrecentageRemaining = value; }
        }
    }
}
