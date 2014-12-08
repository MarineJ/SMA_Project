using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    interface IComportement
    {
        public abstract bool Comportement(FrameEvent evt, Random rand, Agent agent);

    }
}
