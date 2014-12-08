using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    interface IComportement
    {
        public abstract bool Comportement(FrameEvent evt, Random rand, Agent agent);

        public abstract bool evolve();

        public abstract bool regress();

        public abstract void remaneWhatYouAre();

        public abstract void negociateWithManager();

        public abstract void negociateWithDrag();

        public abstract void negociateWithIdler();

        public abstract void negociateWithBuilder();



    }
}
