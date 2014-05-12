using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.States
{
    /// <summary>
    /// 状态迁移。
    /// </summary>
    public sealed class Transition<TState, TOperation>
    {
        /// <summary>
        /// 源状态。
        /// </summary>
        public TState SourceState { get; internal set; }

        /// <summary>
        /// 操作。
        /// </summary>
        public TOperation Operation { get; internal set; }

        /// <summary>
        /// 前置条件。
        /// </summary>
        public Predicate<OperationContext<TState, TOperation>> Condition
        { get; internal set; }

        /// <summary>
        /// 目标状态。
        /// </summary>
        public TState TargetState { get; internal set; }

        /// <summary>
        /// 后置操作。
        /// </summary>
        public Action<OperationContext<TState, TOperation>> PostAction
        { get; internal set; }
    }
}
