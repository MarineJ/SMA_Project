using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Builder : IComportement
    {

        public bool Comportement(FrameEvent evt, Random rand, Agent agent) { return (true); }

        public Builder()
        {
        }

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
            Random rand = new Random();
            int num = rand.Next(300);

            if (num < other.MLeaderShip)
            {
                Tools.updateValue(negociator.MMotivation, Tools.MOTIVATION_UP);
            }
            else if (num < other.MLeaderShip + negociator.MMotivation)
            {
                evolve(negociator);
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_UP);
            }
            else if(num < other.MLeaderShip + negociator.MMotivation + negociator.MFatigue)
            {
                regress(negociator);
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_DOWN);
            }
        }

        //
        public void negociateWithIdler(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < other.MMotivation + negociator.MSimpathy)
            {
                other.MComportement.evolve(other);
            }
            else if (num < other.MMotivation + negociator.MSimpathy + other.MAngryness)
            {
                regress(negociator);
            }
        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < negociator.MSimpathy)
            {
                other.MComportement.evolve(other);
            }
            else if (num < negociator.MSimpathy + negociator.MAngryness + negociator.MFatigue)
            {
                regress(negociator);
            }
            else
            {
                Tools.updateValue(other.MMotivation, Tools.MOTIVATION_UP);
            }
        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < other.MMotivation + other.MSimpathy)
            {
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_UP);
            }
            else if (num < other.MMotivation + other.MSimpathy + other.MFatigue)
            {
                regress(negociator);
            }
            else
            {
                Tools.updateValue(negociator.MSimpathy, Tools.SIMPATHY_UP);
            }
        }
    }
}
