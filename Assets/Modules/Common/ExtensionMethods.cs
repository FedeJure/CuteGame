using System;
using System.Collections.Generic;
using UniRx;

namespace Modules.Common
{
    public static class ExtensionMethods
    {
        public static IObservable<T> ToObservableDummy<T>(this T self)
        {
            return Observable.ReturnUnit()
                .Select(_ => self);
        }

        public static Maybe<T> ToMaybe<T>(this T self)
        {
            return new Maybe<T>(self);
        }

        public static void DisposeAll(this List<IDisposable> disposables)
        {
            disposables.ForEach(d => d.Dispose());
        }

        public static IObservable<Unit> OnCompleteAsObservable(this LTDescr evnt)
        {
            var observable = new ReplaySubject<Unit>();
            observable.OnNext(Unit.Default);
            evnt.setOnComplete(() => observable.OnCompleted());
            return observable;
        }
    }
}