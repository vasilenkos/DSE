using System;
using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace DSE.Tests.Symptomatic
{
    public static partial class Defaults
    {
        public static class Tree
        {
            public enum _Color
            {
                Black = 0,
                Red = 1,
                Green = 2,
                Blue = 4,
                Yellow = 3,
                Magenta = 5,
                Cyan = 6,
                White = 7
            }

            public class _Root
            {
                public Int32 Weight = 30;
                public _Color Color = _Color.Black;

                public List<_Branch> Branches = null;
            }

            public class _Branch
            {
                public Int32 Length = 20;
                public _Color Color = _Color.Yellow;

                public List<_Leaf> Leaves = null;
            }

            public class _Leaf
            {
                public Int32 Square = 0;
                public _Color Color = _Color.Green;
            }

            public class _Tree
            {
                public String Name = String.Empty;
                public _Root Root = null;
            }

            public static readonly _Tree Default = new _Tree()
            {
                Name = "Fir",
                Root = new _Root()
                {
                    Color = _Color.Yellow,
                    Weight = 10,
                    Branches = new List<_Branch>()
                    {
                        new _Branch(){
                            Color = _Color.Green,
                            Length = 10,
                            Leaves = new List<_Leaf>()
                            {
                                new _Leaf(){ Color = _Color.Cyan, Square = 1},
                                new _Leaf(){ Color = _Color.Cyan, Square = 2},
                                new _Leaf(){ Color = _Color.Cyan, Square = 3},
                                new _Leaf(){ Color = _Color.Green, Square = 2},
                                new _Leaf(){ Color = _Color.Green, Square = 3},
                                new _Leaf(){ Color = _Color.Green, Square = 3},
                                new _Leaf(){ Color = _Color.Cyan, Square = 2},
                                new _Leaf(){ Color = _Color.Cyan, Square = 2},
                                new _Leaf(){ Color = _Color.Green, Square = 3},
                                new _Leaf(){ Color = _Color.Green, Square = 1},
                                new _Leaf(){ Color = _Color.Cyan, Square = 5},
                            }
                        },
                        new _Branch(){
                            Color = _Color.Green,
                            Length = 26,
                            Leaves = new List<_Leaf>()
                            {
                                new _Leaf(){ Color = _Color.Cyan, Square = 1},
                                new _Leaf(){ Color = _Color.Red, Square = 2},
                                new _Leaf(){ Color = _Color.Cyan, Square = 1},
                                new _Leaf(){ Color = _Color.Green, Square = 3},
                                new _Leaf(){ Color = _Color.Green, Square = 3},
                                new _Leaf(){ Color = _Color.Yellow, Square = 3},
                                new _Leaf(){ Color = _Color.Cyan, Square = 2},
                                new _Leaf(){ Color = _Color.Cyan, Square = 3},
                                null,
                                new _Leaf(){ Color = _Color.Green, Square = 1},
                                new _Leaf(){ Color = _Color.White, Square = 2},
                                new _Leaf(){ Color = _Color.Cyan, Square = 5},
                            }
                        },
                        null,
                        new _Branch(){
                            Color = _Color.Magenta,
                            Length = 17,
                            Leaves = new List<_Leaf>()
                            {
                                new _Leaf(){ Color = _Color.Cyan, Square = 2},
                                new _Leaf(){ Color = _Color.Red, Square = 3},
                                new _Leaf(){ Color = _Color.Cyan, Square = 4},
                                new _Leaf(){ Color = _Color.Green, Square = 1},
                                new _Leaf(){ Color = _Color.Blue, Square = 2},
                                new _Leaf(){ Color = _Color.Yellow, Square = 3},
                                new _Leaf(){ Color = _Color.Cyan, Square = 4},
                                new _Leaf(){ Color = _Color.Cyan, Square = 5},
                                null,
                                new _Leaf(){ Color = _Color.Yellow, Square = 2},
                                new _Leaf(){ Color = _Color.White, Square = 1},
                                new _Leaf(){ Color = _Color.Cyan, Square = 1},
                            }
                        }
                    }
                }
            };
        }
    }
}