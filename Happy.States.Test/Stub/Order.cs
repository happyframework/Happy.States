using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.States.Test.Stub
{
    enum Status
    {
        UnSaved,
        Saved,
        Submitted
    }

    internal enum Operation
    {
        Save,
        Submit,
        Edit
    }

    sealed class Order
    {
        public Status Status { get; set; }

        public StateMachine<Status, Operation> StateMachine()
        {
            var machine = new StateMachine<Status, Operation>(() => this.Status, (x) => this.Status = x);

            machine
                .When(Status.UnSaved)
                .Permit(Operation.Save, Status.Saved);
            machine
                .When(Status.Saved)
                .Permit(Operation.Save)
                .Permit(Operation.Submit, Status.Submitted);

            return machine;
        }
    }
}
