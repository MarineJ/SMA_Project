using System;
using System.Collections.Generic;
using System.Text;

namespace SMA_Project_V1
{
    static class Tools
    {


    internal static   int LEADERSHIP_UP = 2;
    internal static   int LEADERSHIP_DOWN = -2;
    internal static   int ANGRYNESS_UP = 2;
    internal static   int MOTIVATION_UP = 5;
    internal static   int FATIGUE_UP = 4;

    internal static   int BUILDER_ANGRYNESS_INITIAL = 50;
    internal static   int BUILDER_FATIGUE_INITIAL = 50;
    internal static   int BUILDER_LEADERSHIP_INITIAL = 50;
    internal static   int BUILDER_MOTIVATION_INITIAL = 50;
    internal static   int BUILDER_SYMPATHY_INITIAL = 50;

    internal static   int DRAG_ANGRYNESS_INITIAL = 50;
    internal static   int DRAG_FATIGUE_INITIAL = 50;
    internal static   int DRAG_LEADERSHIP_INITIAL = 50;
    internal static   int DRAG_MOTIVATION_INITIAL = 50;
    internal static   int DRAG_SYMPATHY_INITIAL = 50;

    internal static   int IDLER_ANGRYNESS_INITIAL = 50;
    internal static   int IDLER_FATIGUE_INITIAL = 50;
    internal static   int IDLER_LEADERSHIP_INITIAL = 50;
    internal static   int IDLER_MOTIVATION_INITIAL = 50;
    internal static   int IDLER_SYMPATHY_INITIAL = 50;

    internal static   int MANAGER_ANGRYNESS_INITIAL = 50;
    internal static   int MANAGER_FATIGUE_INITIAL = 50;
    internal static   int MANAGER_LEADERSHIP_INITIAL = 50;
    internal static   int MANAGER_MOTIVATION_INITIAL = 50;
    internal static   int MANAGER_SYMPATHY_INITIAL = 50;

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
