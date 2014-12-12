using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Drag : IComportement
    {

        public Drag()
        {

        }

        public Drag(Agent agent) 
        {
            agent.MAngryness = Tools.DRAG_ANGRYNESS_INITIAL;
            agent.MFatigue = Tools.DRAG_FATIGUE_INITIAL;
            agent.MLeaderShip = Tools.DRAG_LEADERSHIP_INITIAL;
            agent.MMotivation = Tools.DRAG_MOTIVATION_INITIAL;
            agent.MSimpathy = Tools.DRAG_SYMPATHY_INITIAL;
        }

        public bool Comportement(FrameEvent evt, Random rand, Agent agent, Agent other)
        {
            agent.negociate(agent, other);
            return (true);
        }

        public void evolve(Agent agent)
        {
            agent.MComportement = new Idler(agent);
        }

        public void regress(Agent agent)
        {
        }


        public void negociateWithManager(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < other.MLeaderShip)
            {
                evolve(negociator);
            }
            else if (num < other.MLeaderShip + negociator.MAngryness)
            {
                Tools.updateValue(negociator.MAngryness, Tools.ANGRYNESS_UP);
            }
            else if (num < other.MLeaderShip + negociator.MAngryness + negociator.MMotivation)
            {
                Tools.updateValue(negociator.MMotivation, Tools.MOTIVATION_UP);
            }
        }

        public void negociateWithIdler(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(200);
            if (num < negociator.MMotivation)
            {
                other.MComportement.regress(other);
            }
            else if (num < negociator.MMotivation + other.MSimpathy)
            {
                evolve(negociator);
            }
            else
            {
                Tools.updateValue(other.MFatigue, Tools.FATIGUE_UP);
            }

        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {

        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {

        }

    }
}
