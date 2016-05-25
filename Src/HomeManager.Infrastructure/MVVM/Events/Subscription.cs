using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HomeManager.Infrastructure.MVVM.Events
{
    internal abstract class Subscription<T>
    {
        private readonly object _trueReference;
        private readonly WeakReference _reference;
        private readonly MethodInfo _method;
        private readonly Type _delegateType;
        private readonly Type _eventType;

        public Subscription(Delegate d)
        {
            _trueReference = d.Target;
            _reference = new WeakReference(d.Target);
            _eventType = typeof(T);
            _method = d.GetMethodInfo();
            _delegateType = d.GetType();
        }

        public Action<T> Method
        {
            get { return (Action<T>)Target; }
        }

        public Type EventType
        {
            get { return _eventType; }
        }

        public bool IsAlive
        {
            //get { return _reference.IsAlive; }
            get { return true; }
        }

        public Delegate Target
        {
            get { return TryGetDelegate(); }
        }

        internal abstract Task FinalizedTask(T eventDataTask);

        protected int DelegateGetHashCode(Delegate obj)
        {
            if (obj == null) return 0;

            int result = obj.Method.GetHashCode() ^ obj.GetType().GetHashCode();

            if (obj.Target != null) result ^= RuntimeHelpers.GetHashCode(obj);

            return result;
        }

        private Delegate TryGetDelegate()
        {
            if (_method.IsStatic) return _method.CreateDelegate(_delegateType, null);
            /// TODO: Figure out how to get WeakReference to work correctly.
            /// This is currently a memory leak.
            ///var target = _reference.Target;
            var target = _trueReference;

            if (target != null) return _method.CreateDelegate(_delegateType, target);

            return null;
        }
    }
}