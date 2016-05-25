using System;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.MVVM.Events
{
    internal class AsyncSubscription<T> : Subscription<T>
    {
        public AsyncSubscription(Delegate d)
            : base(d)
        {
        }

        public new Func<T, Task> Method
        {
            get { return (Func<T, Task>)Target; }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (!(obj is AsyncSubscription<T>)) return false;

            return Equals((AsyncSubscription<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked { return Method != null ? DelegateGetHashCode(Method) : 0; }
        }

        internal override async Task FinalizedTask(T eventDataTask)
        {
            await Method(eventDataTask);
        }

        private bool Equals(AsyncSubscription<T> other)
        {
            return Method.Equals(other.Method);
        }
    }
}