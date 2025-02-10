using Better.StateMachine.Runtime.States;

namespace EndlessHeresy.Core.States
{
    public abstract class BaseState<TContext> : BaseState where TContext : IContext
    {
        protected TContext Context { get; private set; }

        public void SetContext(TContext context)
        {
            OnContextSet(context);
        }

        protected virtual void OnContextSet(TContext context)
        {
            Context = context;
        }

        public sealed override void OnEntered()
        {
        }

        public sealed override void OnExited()
        {
        }
    }
}