﻿using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Builder : IComportement
    {

        public bool Comportement(FrameEvent evt, Random rand, Agent agent) { return (true); }

        public void evolve(Agent agent)
        {
        }

        public void regress(Agent agent)
        {
            agent.MComportement = new Builder();
        }

        public void remaneWhatYouAre(Agent agent)
        {

        }

        //
        public void negociateWithManager(Agent negociator, Agent other)
        {

        }

        //
        public void negociateWithIdler(Agent negociator, Agent other)
        {

        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {

        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {

        }
    }
}
