using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    interface IComportement
    {
        bool Comportement(FrameEvent evt, Random rand, Agent agent);

        void evolve(Agent agent);

        void regress(Agent agent);

        void remaneWhatYouAre(Agent agent);

        void negociateWithManager(Agent negociator, Agent other);

        void negociateWithDrag(Agent negociator, Agent other);

        void negociateWithIdler(Agent negociator, Agent other);

        void negociateWithBuilder(Agent negociator, Agent other);



    }
}


