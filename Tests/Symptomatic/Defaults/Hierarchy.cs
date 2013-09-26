using System;
using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace DSE.Tests.Symptomatic
{
    public static partial class Defaults
    {
        public static class Hierarchy
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

            public class _Item
            {
                public String ID;
                public _Color Color = _Color.Black;
                public String ParentID;
            }

            public static readonly _Item[] Default = new _Item[]
            {
                new _Item() { ID = "Root", Color = _Color.Black, ParentID = null },
                    new _Item() { ID = "Left", Color = _Color.Blue, ParentID = "Root" },
                        new _Item() { ID = "LeftTop", Color = _Color.Red, ParentID = "Left" },
                        new _Item() { ID = "LeftBottom", Color = _Color.Red, ParentID = "Left" },
                    new _Item() { ID = "Right", Color = _Color.Green, ParentID = "Root" },
                        new _Item() { ID = "RightTop", Color = _Color.Magenta, ParentID = "Right" },
                        new _Item() { ID = "RightBottom", Color = _Color.Magenta, ParentID = "Right" },
            };
        }
    }
}