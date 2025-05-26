using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Electric : EnergySource
    {


        public Electric(float i_EnergyPrecentageRemaining, float i_CurrentAmount, float i_MaxAmount) : base(i_EnergyPrecentageRemaining, i_CurrentAmount, i_MaxAmount){}


        public void Recharge(float i_MinutesToCharge)
        {
            if (i_MinutesToCharge < 0 || CurrentAmount + i_MinutesToCharge > MaxAmount)
            {
                //exception
            }

            float hoursToCharge = (float)i_MinutesToCharge / 60;

            AddEnergy(hoursToCharge);
        }



    }
}
