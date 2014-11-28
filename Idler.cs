using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class Idler : Agent
    {

        public Idler(string mesh, SceneManager SceneManager, string nom, LinkedList<Vector3> walklist, float walkspeed, int index) :
            base(mesh, SceneManager, nom, walklist, walkspeed, index) { }

        public override bool Comportement(FrameEvent evt, Random rand) { return (true); }

      

    }
}
