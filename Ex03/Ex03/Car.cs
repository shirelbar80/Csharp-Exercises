using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        enum eCarColours
        {
            Yellow, Black, White, Silver
        }

        enum eDoorsAmount
        {
            Two = 2, 
            Three = 3,
            Four = 4,
            Five = 5
        }

        eCarColours m_CarColor;
        eDoorsAmount m_DoorsAmount;

    }
}
