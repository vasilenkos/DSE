using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DSE.Extensions
{
    public enum SorterDirection
    {
        Ascending = 0,
        Descending = 1
    }

    public class Sorter<T> : IComparer<T>
    {
        protected List<Func<T, T, Int32>> _oComparers = null;

        public Sorter()
        {
            _oComparers = new List<Func<T, T, int>>();
        }

        private int _CompareTwoBooleans(Boolean pbFirst, Boolean pbSecond)
        {
            return (pbFirst == pbSecond) ? 0 : (pbFirst ? 1 : -1);
        }

        private Int32 _InvertResult(Int32 pnResult)
        {
            return pnResult * -1;
        }

        public Sorter<T> ByExpression(Func<T, Boolean> poFunctor)
        {
            return ByExpression(poFunctor, SorterDirection.Ascending);
        }

        public Sorter<T> ByExpression(Func<T, Boolean> poFunctor, SorterDirection peDirection)
        {
            switch (peDirection)
            {
                case SorterDirection.Ascending:
                    _oComparers.Add((x, y) => _CompareTwoBooleans(poFunctor(x), poFunctor(y)));
                    break;
                case SorterDirection.Descending:
                    _oComparers.Add((x, y) => _InvertResult(_CompareTwoBooleans(poFunctor(x), poFunctor(y))));
                    break;
            }

            return this;
        }

        public Sorter<T> ByComparison<P>(Func<T, P> poFunctor)
        {
            return ByComparison(poFunctor, SorterDirection.Ascending);
        }

        public Sorter<T> ByComparison<P>(Func<T, P> poFunctor, SorterDirection peDirection)
        {
            switch (peDirection)
            {
                case SorterDirection.Ascending:
                    _oComparers.Add((x, y) => Comparer<P>.Default.Compare(poFunctor(x), poFunctor(y)));
                    break;
                case SorterDirection.Descending:
                    _oComparers.Add((x, y) => _InvertResult(Comparer<P>.Default.Compare(poFunctor(x), poFunctor(y))));
                    break;
            }

            return this;
        }

        public Sorter<T> ByComparison(Func<T, T, Int32> poFunctor, SorterDirection peDirection)
        {
            switch (peDirection)
            {
                case SorterDirection.Ascending:
                    _oComparers.Add((x, y) => poFunctor(x, y));
                    break;
                case SorterDirection.Descending:
                    _oComparers.Add((x, y) => _InvertResult(poFunctor(x,y)));
                    break;
            }

            return this;
        }

        #region IComparer<T> Members

        Int32 IComparer<T>.Compare(T x, T y)
        {
            foreach (var loComparer in _oComparers)
            {
                var lnResult = loComparer(x, y);

                if (lnResult != 0)
                    return lnResult;
            }

            return 0;
        }

        #endregion
    }
}
