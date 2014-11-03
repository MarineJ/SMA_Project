using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace SMA_Project_V1
{
    class BuildingSimulation : WindowMaker
    {
         AnimationState mAnimationState = null; //The AnimationState the moving object
        float mDistance = 0.0f;              //The distance the object has left to travel
        Vector3 mDirection = Vector3.ZERO;   // The direction the object is moving
        Vector3 mDestination = Vector3.ZERO; // The destination the object is moving towards
        LinkedList<Vector3> mWalkList = null; // A doubly linked containing the waypoints
        float mWalkSpeed =50.0f;  // The speed at which the object is moving
 
 
        protected override void CreateSceneManager()
        {
 
 
 
 
        }
 
        protected override void CreateInputHandler()
        {
            base.CreateInputHandler();
            this.Root.FrameStarted += new FrameListener.FrameStartedHandler(FrameStarted);
 
 
        }
 
        protected bool nextLocation()
        {
            return true;
        }
 
        bool FrameStarted(FrameEvent evt)
        {
 
 
            return true;
        }
 
 
    }
}
