using Ex03.GarageLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Fuel : EnergySource
    {
        public enum eFuelType
        {
            Octan96,
            Octan95,
            Octan98,
            Soler
        }

        eFuelType m_FuelType;

        public Fuel(eFuelType i_FuelType, float i_EnergyPrecentageRemaining, float i_CurrentAmount, float i_MaxAmount) : base(i_EnergyPrecentageRemaining, i_CurrentAmount, i_MaxAmount)
        {
            m_FuelType = i_FuelType;
        }

        public void Refuel(eFuelType i_FuelType, float i_Amount)
        {
            if (CurrentAmount + i_Amount > MaxAmount)
            {
                throw new ValueRangeException(0, MaxAmount, $"Cannot add {i_Amount}. It would exceed the maximum allowed amount of {MaxAmount}.");
            }

            AddEnergy(i_Amount); // from EnergySource
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }
    }
}
