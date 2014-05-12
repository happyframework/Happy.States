using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.States
{
    /// <summary>
    /// 状态机配置器。
    /// </summary>
    public sealed class StateMachineConfiger<TState, TOperation>
    {
        private readonly StateMachine<TState, TOperation> _machine;
        private readonly TState _sourceState;

        internal StateMachineConfiger(StateMachine<TState, TOperation> machine, 
                                      TState sourceState)
        {
            Check.MustNotNull(machine, "machine");
            Check.MustNotNull(sourceState, "sourceState");

            _machine = machine;
            _sourceState = sourceState;
        }

        /// <summary>
        /// 允许执行<paramref name="operation"/>，执行后状态不会发生变化。
        /// </summary>
        public StateMachineConfiger<TState, TOperation> Permit(TOperation operation)
        {
            return this.Permit(operation, _sourceState);
        }

        /// <summary>
        /// 允许执行<paramref name="operation"/>，执行后状态迁移到
        /// <paramref name="targetState"/>。
        /// </summary>
        public StateMachineConfiger<TState, TOperation> Permit(TOperation operation, 
                                                               TState targetState)
        {
            return this.Permit(operation, context => true, targetState);
        }

        /// <summary>
        /// 当<paramref name="condition"/>为<code>true</code>的时候，允许执行
        /// <paramref name="operation"/>，执行后状态迁移到<paramref name="targetState"/>。
        /// </summary>
        public StateMachineConfiger<TState, TOperation> Permit(
                            TOperation operation,
                            Predicate<OperationContext<TState, TOperation>> condition,
                            TState targetState)
        {
            return this.Permit(operation, condition, targetState, null);
        }

        /// <summary>
        /// 当<paramref name="condition"/>为<code>true</code>的时候，允许执行
        /// <paramref name="operation"/>，执行后状态迁移到<paramref name="targetState"/>
        /// ，然后执行<paramref name="postAction"/>。
        /// </summary>
        public StateMachineConfiger<TState, TOperation> Permit(
                            TOperation operation,
                            Predicate<OperationContext<TState, TOperation>> condition,
                            TState targetState,
                            Action<OperationContext<TState, TOperation>> postAction)
        {

            Check.MustNotNull(operation, "operation");
            Check.MustNotNull(condition, "condition");
            Check.MustNotNull(targetState, "targetState");

            _machine.RegistTransition(new Transition<TState, TOperation>
            {
                SourceState = _sourceState,
                Operation = operation,
                Condition = condition,
                TargetState = targetState,
                PostAction = postAction
            });

            return this;
        }
    }
}
