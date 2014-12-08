using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    interface IComportement
    {
        public abstract bool Comportement(FrameEvent evt, Random rand, Agent agent);


        public abstract bool evolve(Agent agent);

        public abstract bool regress(Agent agent);

        public abstract void remaneWhatYouAre(Agent agent);

        public abstract void negociateWithManager(Agent negociator, Agent other);

        public abstract void negociateWithDrag(Agent negociator, Agent other);

        public abstract void negociateWithIdler(Agent negociator, Agent other);

        public abstract void negociateWithBuilder(Agent negociator, Agent other);



    }
}
