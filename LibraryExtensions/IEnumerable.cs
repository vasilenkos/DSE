﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DSE.Extensions
{
    public static class _IEnumerable
    {
        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> poSeq, Func<T, R> poFunc)
        {
            foreach (var loT in poSeq)
                yield return poFunc(loT);
        }

        public static Double AverageDefault(this IEnumerable<Double> poSeq, Double poDefault)
        {
            try
            {
                return poSeq.Average();
            }

            catch { }

            return poDefault;
        }

        public static Double MinDefault(this IEnumerable<Double> poSeq, Double poDefault)
        {
            try
            {
                return poSeq.Min();
            }

            catch { }

            return poDefault;
        }

        public static Double MaxDefault(this IEnumerable<Double> poSeq, Double poDefault)
        {
            try
            {
                return poSeq.Max();
            }

            catch { }

            return poDefault;
        }

        public static void Apply<T>(this IEnumerable<T> poSeq, Action<T> poFunc)
        {
            foreach (var loT in poSeq)
                poFunc(loT);
        }

        public static void Apply<T>(this IEnumerable<T> poSeq, Action<T, Int64> poFunc)
        {
            var pnIndex = (Int64)0;

            foreach (var loT in poSeq)
            {
                poFunc(loT, pnIndex++);
            }
        }

        public static IEnumerable<R> MapToFirst<T, R>(this IEnumerable<T> poSeq, Func<T, R> poFunc)
        {
            if (poSeq.Count() > 0)
                yield return poFunc(
                    poSeq.First()
                );
        }

        public static void ApplyToFirst<T>(this IEnumerable<T> poSeq, Action<T> poFunc)
        {
            if (poSeq.Count() > 0)
                poFunc(
                    poSeq.First()
                );
        }

        public static IEnumerable<R> MapToLast<T, R>(this IEnumerable<T> poSeq, Func<T, R> poFunc)
        {
            if (poSeq.Count() > 0)
                yield return poFunc(
                    poSeq.Last()
                );
        }

        public static void ApplyToLast<T>(this IEnumerable<T> poSeq, Action<T> poFunc)
        {
            if (poSeq.Count() > 0)
                poFunc(
                    poSeq.Last()
                );
        }

        public static Action<A> Y<A>(Func<Action<A>, Action<A>> F) { return a => F(Y(F))(a); }

        public static IEnumerable<T> TraverseDown<T>(this IEnumerable<T> poSource, Func<T, IEnumerable<T>> poChildrenExtractor, Func<T, bool> poPredicate)
        {
            var loList = new List<T>();

            Y<IEnumerable<T>>((f) => (items) => items.UseIfNotNull(_=>
                {
                    loList.AddRange(_.Where(poPredicate));
                    _.Apply(__ => f(poChildrenExtractor(__)));
                })
            )(poSource);

            return loList;
        }

        public static IEnumerable<T> TraverseUp<T>(this T poSource, Func<T, T> poParentExtractor, Func<T, bool> poPredicate)
        {
            var loList = new List<T>();

            Y<T>((f) => (item) =>
            {
                if (poPredicate(item))
                {
                    loList.Add(item);
                    f(poParentExtractor(item));
                }
            }
            )(poSource);

            return loList;
        }

        /// <summary>
        /// Метод расширения, инициализирующий гибкий обходчик для коллекций IEnumerable
        /// </summary>
        /// <typeparam name="T">Тип элемента коллекции (обычно подставляется компилятором автоматически)</typeparam>
        /// <typeparam name="C">Тип для хранения контекста обходчика (обычно подставляется компилятором автоматически; нужен для того, чтобы не пачкать код замыканиями)</typeparam>
        /// <param name="poSeq">Ссылка на объект коллекции</param>
        /// <param name="poContext">Объект контекста (может быть скаляром или строкой для тех случаев, когда контекст допускается иммутабельным)</param>
        /// <returns>Возвращает инициализированный обходчик</returns>
        public static EnumerableWalker<T, C> Walker<T, C>(this IEnumerable<T> poSeq, C poContext)
        {
            return new EnumerableWalker<T, C>(poSeq, poContext);
        }
    }
}