using Better.StateMachine.Runtime.States;

namespace Inspirio.Gameplay.Actors.GameBoard.States.Abstractions
{
    public abstract class GameBoardState : BaseState
    {
        protected GameBoardActor Context { get; }

        protected GameBoardState(GameBoardActor context)
        {
            Context = context;
        }

        public override void OnEntered()
        {
        }

        public override void OnExited()
        {
        }
    }
}