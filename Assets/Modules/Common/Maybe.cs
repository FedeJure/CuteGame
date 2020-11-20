using System;
using UnityEngine.Assertions;

namespace Modules.Common
{
    public struct Maybe<T>
    {
        private bool hasValue;
        private T value;

        public Maybe(T value)
        {
            hasValue = true;
            this.value = value;
        }

        public T Value
        {
            get
            {
                Assert.IsTrue(hasValue, "Can't get Maybe<T> value when not set");
                return value;
            }
        }

        public Maybe<T> Do(Action<T> callback)
        {
            if (!hasValue) return this;
            callback(value);
            return this;
        }

        public Maybe<T> DoWhenAbsent(Action callback)
        {
            if (!hasValue) callback();
            return this;
        }

        public U ReturnOrDefault<U>(Func<T, U> callback, U defaultObject)
        {
            return hasValue ? callback(value) : defaultObject;
        }

        public U ReturnOrException<U>(Func<T, U> callback, Exception exeption)
        {
            if (!hasValue) throw exeption;
            return callback(value);
        }

        public static Maybe<T> Nothing
        {
            get { return default(Maybe<T>); }
        }
        
    }
}