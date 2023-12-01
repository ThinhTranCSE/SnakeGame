using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Ultilities
{
    public class Unsubscriber<T> : IDisposable
    {
        List<IObserver<T>> Observers;
        IObserver<T> Observer;
        public Unsubscriber(List<IObserver<T>> Observers, IObserver<T> Observer)
        {
            this.Observers = Observers;
            this.Observer = Observer;
        }

        public void Dispose()
        {
            if (this.Observer != null && this.Observers.Contains(this.Observer))
            {
                this.Observers.Remove(this.Observer);
            }
        }
    }
}
