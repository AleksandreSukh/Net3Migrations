#if !CLR40
using System;
using System.Threading;

namespace Net3Migrations.System
{
    public delegate TResult TFunc<TResult>();

    public class Lazy<T>
    {


        private T _value = default(T);
        private volatile bool _isValueCreated = false;

        private TFunc<T> _valueFactory = null;
        private object _lock;

        public Lazy()
            : this(Activator.CreateInstance<T>)
        {
        }

        public Lazy(bool isThreadSafe)
            : this(Activator.CreateInstance<T>, isThreadSafe)
        {
        }

        public Lazy(TFunc<T> valueFactory) :
            this(valueFactory, true)
        {
        }

        public Lazy(TFunc<T> valueFactory, bool isThreadSafe)
        {
            if (valueFactory == null) throw new ArgumentNullException(nameof(valueFactory));
            //Requires.NotNull(valueFactory, "valueFactory");//This is changed by me!
            if (isThreadSafe)
            {
                this._lock = new object();
            }

            this._valueFactory = valueFactory;
        }


        public T Value
        {
            get
            {
                if (!this._isValueCreated)
                {
                    if (this._lock != null)
                    {
                        Monitor.Enter(this._lock);
                    }

                    try
                    {
                        T value = this._valueFactory.Invoke();
                        this._valueFactory = null;
                        Thread.MemoryBarrier();
                        this._value = value;
                        this._isValueCreated = true;
                    }
                    finally
                    {
                        if (this._lock != null)
                        {
                            Monitor.Exit(this._lock);
                        }
                    }
                }
                return this._value;
            }
        }
    }
}
#endif