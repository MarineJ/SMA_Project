using System;
using System.Collections.Generic;
using System.Text;

namespace SMA_Project_V1
{
    static class Tools
    {


    internal  const int LEADERSHIP_UP = 2;
    internal  const int LEADERSHIP_DOWN = -2;
    internal  const int ANGRYNESS_UP = 2;
    internal  const int MOTIVATION_UP = 5;
    internal  const int FATIGUE_UP = 4;
    internal  const int SIMPATHY_UP = 4;

    internal  const int BUILDER_ANGRYNESS_INITIAL = 50;
    internal  const int BUILDER_FATIGUE_INITIAL = 50;
    internal  const int BUILDER_LEADERSHIP_INITIAL = 50;
    internal  const int BUILDER_MOTIVATION_INITIAL = 50;
    internal  const int BUILDER_SYMPATHY_INITIAL = 50;

    internal  const int DRAG_ANGRYNESS_INITIAL = 50;
    internal  const int DRAG_FATIGUE_INITIAL = 50;
    internal  const int DRAG_LEADERSHIP_INITIAL = 50;
    internal  const int DRAG_MOTIVATION_INITIAL = 50;
    internal  const int DRAG_SYMPATHY_INITIAL = 50;

    internal  const int IDLER_ANGRYNESS_INITIAL = 50;
    internal  const int IDLER_FATIGUE_INITIAL = 50;
    internal  const int IDLER_LEADERSHIP_INITIAL = 50;
    internal  const int IDLER_MOTIVATION_INITIAL = 50;
    internal  const int IDLER_SYMPATHY_INITIAL = 50;

    internal  const int MANAGER_ANGRYNESS_INITIAL = 50;
    internal  const int MANAGER_FATIGUE_INITIAL = 50;
    internal  const int MANAGER_LEADERSHIP_INITIAL = 50;
    internal  const int MANAGER_MOTIVATION_INITIAL = 50;
    internal  const int MANAGER_SYMPATHY_INITIAL = 50;

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
