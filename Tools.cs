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
    internal static const int MOTIVATION_UP = 5;
    internal static const int FATIGUE_UP = 4;

    internal static const int BUILDER_ANGRYNESS_INITIAL = 50;
    internal static const int BUILDER_FATIGUE_INITIAL = 50;
    internal static const int BUILDER_LEADERSHIP_INITIAL = 50;
    internal static const int BUILDER_MOTIVATION_INITIAL = 50;
    internal static const int BUILDER_SYMPATHY_INITIAL = 50;

    internal static const int DRAG_ANGRYNESS_INITIAL = 50;
    internal static const int DRAG_FATIGUE_INITIAL = 50;
    internal static const int DRAG_LEADERSHIP_INITIAL = 50;
    internal static const int DRAG_MOTIVATION_INITIAL = 50;
    internal static const int DRAG_SYMPATHY_INITIAL = 50;

    internal static const int IDLER_ANGRYNESS_INITIAL = 50;
    internal static const int IDLER_FATIGUE_INITIAL = 50;
    internal static const int IDLER_LEADERSHIP_INITIAL = 50;
    internal static const int IDLER_MOTIVATION_INITIAL = 50;
    internal static const int IDLER_SYMPATHY_INITIAL = 50;

    internal static const int MANAGER_ANGRYNESS_INITIAL = 50;
    internal static const int MANAGER_FATIGUE_INITIAL = 50;
    internal static const int MANAGER_LEADERSHIP_INITIAL = 50;
    internal static const int MANAGER_MOTIVATION_INITIAL = 50;
    internal static const int MANAGER_SYMPATHY_INITIAL = 50;

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
