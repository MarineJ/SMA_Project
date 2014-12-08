﻿using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Builder : IComportement
    {

        public bool Comportement(FrameEvent evt, Random rand, Agent agent) { return (true); }

        public Builder(Agent agent) 
        {
            agent.MAngryness = Tools.BUILDER_ANGRYNESS_INITIAL;
            agent.MFatigue = Tools.BUILDER_FATIGUE_INITIAL;
            agent.MLeaderShip = Tools.BUILDER_LEADERSHIP_INITIAL;
            agent.MMotivation = Tools.BUILDER_MOTIVATION_INITIAL;
            agent.MSimpathy = Tools.BUILDER_SYMPATHY_INITIAL;
        }

        public void evolve(Agent agent)
        {
            agent.MComportement = new Manager(agent);
        }

        public void regress(Agent agent)
        {
            agent.MComportement = new Idler(agent);
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
