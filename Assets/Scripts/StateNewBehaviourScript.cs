//namespace Zach
//Idle
//Crouch
//Jumping/IsGrounded
//WalkingForward
//WalkingBackward
using Contexts;

namespace ZachGame
{
    public class DeleteThisIdleState : States.State
    {
        protected FighterData Data => UnityEngine.Resources.Load<FighterData>("FighterData");
        public override void OnEnter(IContext context)
        {
            Data.IsIdle = true;
            Data.IsWalking = false;
        }
        public override void OnExit(IContext context)
        {
            Data.IsIdle = false;
        }
    }
    public class DeleteThisWalkState : States.State
    {
        protected FighterData Data => UnityEngine.Resources.Load<FighterData>("FighterData");
        public override void OnEnter(IContext context)
        {
            Data.IsIdle = false;
            Data.IsWalking = true;
        }
        public override void OnExit(IContext context)
        {
            Data.IsWalking = false;
        }
        public override void Update(IContext context)
        {
            Data.position += new UnityEngine.Vector3(0.1f, 0, 0);
        }
    }
}