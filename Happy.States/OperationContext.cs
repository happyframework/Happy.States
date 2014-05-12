using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.States
{
    /// <summary>
    /// 操作上下文。
    /// </summary>
    public sealed class OperationContext<TState, TOperation>
    {
        internal OperationContext(StateMachine<TState, TOperation> machine,
                                  TOperation operation)
        {
            Check.MustNotNull(machine, "machine");
            Check.MustNotNull(operation, "operation");

            this.StateMachine = machine;
            this.Operation = operation;
        }

        /// <summary>
        /// 状态机。
        /// </summary>
        public StateMachine<TState, TOperation> StateMachine { get; private set; }

        /// <summary>
        /// 操作。
        /// </summary>
        public TOperation Operation { get; private set; }
    }
}
