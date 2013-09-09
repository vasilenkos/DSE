using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace DSE.Extensions
{
    /// <summary>
    /// Класс гибкого обходчика для коллекций IEnumerable
    /// </summary>
    /// <typeparam name="T">Тип элемента коллекции</typeparam>
    /// <typeparam name="C">Тип контекста коллекции</typeparam>
    public class EnumerableWalker<T, C>
    {
        #region Protected
        /// <summary>
        /// Поле для хранения контекста
        /// </summary>
        protected C _oContext;
        /// <summary>
        /// Поле для хранения ссылки на коллекцию, которую необходимо обходить
        /// </summary>
        protected IEnumerable<T> _oValues;
        /// <summary>
        /// Поле для хранения групп
        /// </summary>
        protected List<_IGroup> _oGroups;
        /// <summary>
        /// Функтор для обработки элемента (C - контекст, T - текущий элемент коллекции)
        /// </summary>
        protected Action<C, T> _oItemFunctor = null;
        /// <summary>
        /// Функтор для обработки начала обхода (C - контекст)
        /// </summary>
        protected Action<C> _oBeginFunctor = null;
        /// <summary>
        /// Функтор для обработки окончания обхода (C - контекст)
        /// </summary>
        protected Action<C> _oEndFunctor = null;
        #endregion

        #region Public
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="poValues">Объект коллекции</param>
        /// <param name="poContext">Инициализированный объект контекста</param>
        public EnumerableWalker(IEnumerable<T> poValues, C poContext)
        {
            _oContext = poContext;
            _oValues = poValues;
            _oGroups = new List<_IGroup>();
        }

        public EnumerableWalker<T, C> OnGroup<G>(G poGroup, Func<C, G, T, bool> poPredicateFunctor, Action<C, G, T> poBeginFunctor, Action<C, G> poEndFunctor)
        {
            _oGroups.Add(
                new _Group<G>(
                    poGroup,
                    poPredicateFunctor,
                    poBeginFunctor,
                    poEndFunctor
                )
            );

            return this;
        }

        public EnumerableWalker<T, C> OnItem(Action<C, T> poOnItemFunctor)
        {
            _oItemFunctor = poOnItemFunctor;

            return this;
        }

        public EnumerableWalker<T, C> OnBegin(Action<C> poOnBegin)
        {
            _oBeginFunctor = poOnBegin;

            return this;
        }

        public EnumerableWalker<T, C> OnEnd(Action<C> poOnEnd)
        {
            _oEndFunctor = poOnEnd;

            return this;
        }

        public void Walk()
        {
            _DoOnBegin();

            foreach (var loItem in _oValues)
            {
                foreach (var loGroup in _oGroups)
                    loGroup.DoBegin(_oContext, loItem);

                if (_oItemFunctor != null)
                    _oItemFunctor(_oContext, loItem);
            }

            foreach (var loGroup in _oGroups)
                loGroup.DoEnd(_oContext);

            _DoOnEnd();
        }

        private void _DoOnEnd()
        {
            if (_oEndFunctor != null)
                _oEndFunctor(_oContext);
        }

        private void _DoOnBegin()
        {
            if (_oBeginFunctor != null)
                _oBeginFunctor(_oContext);
        }

        public interface _IGroup
        {
            void DoBegin(C poContext, T poItem);
            void DoEnd(C poContext);
        }

        public class _Group<G> : _IGroup
        {
            #region Protected
            protected G _oGroup;
            protected Func<C, G, T, bool> _oPredicateFunctor = null;
            protected Action<C, G, T> _oBeginFunctor = null;
            protected Action<C, G> _oEndFunctor = null;
            protected bool _bHasBegun = false;
            #endregion

            #region Public
            public _Group(G poGroup, Func<C, G, T, bool> poPredicateFunctor, Action<C, G, T> poBeginFunctor, Action<C, G> poEndFunctor)
            {
                _oGroup = poGroup;
                _oPredicateFunctor = poPredicateFunctor;
                _oBeginFunctor = poBeginFunctor;
                _oEndFunctor = poEndFunctor;
            }

            #region _IGroup
            void _IGroup.DoBegin(C poContext, T poItem)
            {
                if (_oPredicateFunctor != null)
                    if (_oPredicateFunctor(poContext, _oGroup, poItem))
                    {
                        _DoEnd(poContext);

                        if (_oBeginFunctor != null)
                            _oBeginFunctor(poContext, _oGroup, poItem);
                    }
            }

            private void _DoEnd(C poContext)
            {
                if (_bHasBegun)
                    if (_oEndFunctor != null)
                        _oEndFunctor(poContext, _oGroup);

                _bHasBegun = true;
            }

            void _IGroup.DoEnd(C poContext)
            {
                _DoEnd(poContext);
            }
            #endregion
            #endregion
        }
        #endregion
    }
}