using System.Threading;
using FluentAssertions;
using HomeManager.Infrastructure.MVVM.Events;
using Moq;
using NUnit.Framework;

namespace HomeManager.Infrastructure.Tests
{
    [TestFixture]
    public class AsyncEventAggregatorTests
    {
        public class MockEvent : AsyncPubSubEvent<object>
        {
            public override void ApplyContext(SynchronizationContext context)
            {
            }
        }

        [Test]
        public void When_GetEvent_is_called_should_return_correct_event()
        {
            var expected = new MockEvent();
            var ea = new AsyncEventAggregator();

            var e = ea.GetEvent<MockEvent>();

            e.GetType().ShouldBeEquivalentTo(expected.GetType());
        }
    }
}