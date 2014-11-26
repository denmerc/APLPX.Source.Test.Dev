using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace APLPX.UI.WPF.Events
{

    public class EventAggregator
    {
        private readonly ISubject<object> subject = new Subject<object>();

        public IObservable<TData> GetEvent<TData>()
        {
            return subject.OfType<TData>().AsObservable();
        }

        public void Publish<TData>(TData data)
        {
            subject.OnNext(data);
        }
    } 

}
