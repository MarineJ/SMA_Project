using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Drag : IComportement
    {

        public Drag() { }


        public  bool Comportement(FrameEvent evt, Random rand, Agent agent) { return (true); }

        public void evolve(Agent agent)
        {
        }

        public void regress(Agent agent)
        {
            agent.MComportement = new Idler();
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

        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {

        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {

        }

    }
}
