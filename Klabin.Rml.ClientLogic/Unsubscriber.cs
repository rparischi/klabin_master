using System;
using System.Collections.Generic;

namespace Klabin.Rml.ClientLogic
{
    public class Unsubscriber<T> : IDisposable where T : MachineData
    {
        private List<IObserver<T>> _observers;
        private IObserver<T> _observer;

        internal Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
