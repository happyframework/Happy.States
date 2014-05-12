using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Happy.States.Test.Stub;

namespace Happy.States.Test
{
    [TestFixture]
    public class StateMachineTests
    {
        [Test]
        public void Can_save_unsaved_order()
        {
            var order = new Order { Status = Status.UnSaved };
            order.StateMachine().Fire(Operation.Save);

            Assert.AreEqual(Status.Saved, order.Status);
        }

        [Test]
        [ExpectedException(typeof(StateException))]
        public void Can_not_submit_unsaved_order()
        {
            var order = new Order { Status = Status.UnSaved };
            order.StateMachine().Fire(Operation.Submit);
        }

        [Test]
        public void Can_save_saved_order()
        {
            var order = new Order { Status = Status.Saved };
            order.StateMachine().Fire(Operation.Save);

            Assert.AreEqual(Status.Saved, order.Status);
        }

        [Test]
        public void Can_submit_saved_order()
        {
            var order = new Order { Status = Status.Saved };
            order.StateMachine().Fire(Operation.Submit);

            Assert.AreEqual(Status.Submitted, order.Status);
        }

        [Test]
        [ExpectedException(typeof(StateException))]
        public void Can_not_save_submited_order()
        {
            var order = new Order { Status = Status.Submitted };
            order.StateMachine().Fire(Operation.Save);
        }

        [Test]
        [ExpectedException(typeof(StateException))]
        public void Can_not_submit_submited_order()
        {
            var order = new Order { Status = Status.Submitted };
            order.StateMachine().Fire(Operation.Submit);
        }
    }
}
