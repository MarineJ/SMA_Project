using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Idler : IComportement
    {

        public Idler() { }
        
        public Idler(Agent agent) 
        {
            agent.MAngryness = Tools.IDLER_ANGRYNESS_INITIAL;
            agent.MFatigue = Tools.IDLER_FATIGUE_INITIAL;
            agent.MLeaderShip = Tools.IDLER_LEADERSHIP_INITIAL;
            agent.MMotivation = Tools.IDLER_MOTIVATION_INITIAL;
            agent.MSimpathy = Tools.IDLER_SYMPATHY_INITIAL;
        }


        // le comportement de l'agent lors de la simulation
        public  bool Comportement(FrameEvent evt, Random rand, Agent agent)
        {
            
            return true;

        }

       
        public void evolve(Agent agent)
        {
            agent.MComportement = new Builder(agent);
        }

        public void regress(Agent agent)
        {
            agent.MComportement = new Drag(agent);
        }


        public void negociateWithManager(Agent negociator, Agent other)
        {
            /*
             * L'évolution dépend de Angryness et du leadership du manager
             * Il a une chance d'évoluer en Builder, regresser en Drag ou de rester Idler
             * Si il devient builder --> le manager gagne l en leadership
             * Si il devient si il regresse en Drag --> le manager perd l en leadership
             * Si il reste Idler --> Angryness ++
             */
            Random rand = new Random();
            int num = rand.Next(200); //Random pondéré en fonction de 2 caractéristiques
            if (num < negociator.MAngryness)
            {
                regress(negociator);
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_DOWN);
            }
            else if (num < negociator.MAngryness + other.MLeaderShip)
            {
                evolve(negociator);
                Tools.updateValue(other.MLeaderShip, Tools.LEADERSHIP_UP);
            }
            else
            {
                Tools.updateValue(negociator.MAngryness, Tools.ANGRYNESS_UP);
            }
        }

        public void negociateWithDrag(Agent negociator, Agent other)
        {
           
            Random rand = new Random();
            int num = rand.Next(200);
            if (num < other.MMotivation)
            {
                regress(negociator);
            }
            else if (num < other.MMotivation + negociator.MSimpathy)
            {
                other.MComportement.evolve(other);
            }
            else
            {
                Tools.updateValue(negociator.MFatigue, Tools.FATIGUE_UP);
            }
        }

        public void negociateWithIdler(Agent negociator, Agent other)
        {
            Console.WriteLine("Negociation between two idlers");
        }

        public void negociateWithBuilder(Agent negociator, Agent other)
        {
            Random rand = new Random();
            int num = rand.Next(300);
            if (num < negociator.MMotivation + other.MSimpathy)
            {
                evolve(negociator);
            }
            else if (num < negociator.MMotivation + other.MSimpathy + negociator.MAngryness)
            {
                other.MComportement.regress(other);
            }
        }



        // le manager marche aléatoirement
        public void marcheAleatoire(Random rand, Agent agent)
        {
            agent.MWalkList.Clear();

            double angle = rand.NextDouble() * System.Math.PI;
            Vector3 tmp = new Vector3((float)(1500 * System.Math.Cos(angle)), agent.Node.Position.y, (float)(1500 * System.Math.Sin(angle)));
            agent.MWalkList.AddLast(tmp);
        }

      

    }
}
