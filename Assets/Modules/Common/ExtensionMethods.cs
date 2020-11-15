using System;
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
    }
}