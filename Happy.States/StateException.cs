using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.States
{
    /// <summary>
    /// 状态异常。
    /// </summary>
    public sealed class StateException : ApplicationException
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        public StateException(object state, object operation)
            : base(string.Format(Resources.Messages.Error_OperationCanNotSupportInState,
                                                                    state, operation))
        {
            Check.MustNotNull(state, "state");
            Check.MustNotNull(operation, "operation");

            this.State = state;
            this.Operation = operation;
        }

        /// <summary>
        /// 状态。
        /// </summary>
        public object State { get; private set; }

        /// <summary>
        /// 操作。
        /// </summary>
        public object Operation { get; private set; }
    }
}
