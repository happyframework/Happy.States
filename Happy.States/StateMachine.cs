using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.States
{
    /// <summary>
    /// 状态机。
    /// </summary>
    public sealed class StateMachine<TState, TOperation>
    {
        private readonly List<Transition<TState, TOperation>> _transitions =
                                            new List<Transition<TState, TOperation>>();
        private readonly Func<TState> _stateGetter;
        private readonly Action<TState> _stateSetter;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public StateMachine(Func<TState> stateGetter, Action<TState> stateSetter)
        {
            Check.MustNotNull(stateGetter, "stateGetter");
            Check.MustNotNull(stateSetter, "stateSetter");

            _stateGetter = stateGetter;
            _stateSetter = stateSetter;
        }

        internal void RegistTransition(Transition<TState, TOperation> transition)
        {
            Check.MustNotNull(transition, "transition");

            _transitions.Add(transition);
        }

        /// <summary>
        /// 配置状态<paramref name="sourceState"/>。
        /// </summary>
        public StateMachineConfiger<TState, TOperation> When(TState sourceState)
        {
            Check.MustNotNull(sourceState, "sourceState");

            return new StateMachineConfiger<TState, TOperation>(this, sourceState);
        }

        /// <summary>
        /// 触发<paramref name="operation"/>。
        /// </summary>
        public void Fire(TOperation operation)
        {
            Check.MustNotNull(operation, "operation");

            var matchedTransition = this.GetMatchedTransition(operation);
            if (matchedTransition == null)
            {
                throw new StateException(_stateGetter(), operation);
            }

            _stateSetter(matchedTransition.TargetState);

            if (matchedTransition.PostAction != null)
            {
                matchedTransition.PostAction(new OperationContext<TState, TOperation>(this, operation));
            }
        }

        private Transition<TState, TOperation> GetMatchedTransition(
                                                                TOperation operation)
        {
            return _transitions.FirstOrDefault(x =>
                        x.SourceState.Equals(_stateGetter())
                        && x.Operation.Equals(operation)
                        && x.Condition(new OperationContext<TState, TOperation>(this, 
                                                                        operation)));
        }
    }
}
