using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StableObject
{

    public delegate void ValueAssureDelegate<T>(StableObject<T> stableObject, T oldValue);
    public class StableObject<T>
    {
        object _locker = new object();
        T tempValue;
        int setCounter, assuranceCounter;

        public virtual T ActualValue { get; private set; }

        public StableObject(int initCounter, T initValue = default(T))
        {
            assuranceCounter = initCounter;
            ResetTo(initValue);
        }
        public virtual void SetValue(T value, ValueAssureDelegate<T> valueAssureDelegate = null)
        {
            lock (_locker)
            {
                if (!Equals(tempValue, value))
                {
                    tempValue = value;
                    setCounter = 1;
                }
                else if (setCounter < assuranceCounter)
                {
                    setCounter++;
                }
                if (setCounter == assuranceCounter)
                {
                    if (!Equals(ActualValue, value))
                    {
                        ForceValue(value, valueAssureDelegate);
                    }
                    setCounter++;
                }
            }
        }

        public virtual void ResetTo(T resetValue)
        {
            lock (_locker)
            {
                ActualValue = resetValue;
                tempValue = resetValue;
                setCounter = 0;
            }
        }
        protected virtual void ForceValue(T value, ValueAssureDelegate<T> valueAssureDelegate = null)
        {
            var oldValue = ActualValue;
            lock (_locker)
            {
                ActualValue = value;
            }
            valueAssureDelegate?.Invoke(this, oldValue);
        }

        public override string ToString()
        {
            return $"{ActualValue}";
        }
    }
}
