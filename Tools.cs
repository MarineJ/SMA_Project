using System;
using System.Collections.Generic;
using System.Text;

namespace SMA_Project_V1
{
    static class Tools
    {


    internal static const int LEADERSHIP_UP = 2;
    internal static const int LEADERSHIP_DOWN = -2;
    internal static const int ANGRYNESS_UP = 2;

    static internal void updateValue(int diff, int valueToUpdate)
    {
        if (valueToUpdate + diff < 0)
        {
            valueToUpdate = 0;
        }
        else if (valueToUpdate + diff > 100)
        {
            valueToUpdate = 100;
        }
        else
        {
            valueToUpdate += diff;
        }
    }

    }
}
