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

        public abstract void FillEnergySource(float i_AmountOfEnergyToFill);

    }
}
