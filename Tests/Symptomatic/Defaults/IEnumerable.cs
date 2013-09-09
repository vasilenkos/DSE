﻿using System;
using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace DSE.Tests.Symptomatic
{
    public static class Defaults
    {
        public static class IEnumerable
        {
            public static readonly IEnumerable<String> String = new List<String>()
            {
                null,
                "",
                "Hello",
                "One more hello",
                "One more hello, repeat",
                "One more hello, repeat 2",
                "2",
                "22",
                "222",
                "-2",
                "2.3",
                @"Lorem ipsum dolor sit amet, consectetur adipisicing elit, " + 
                    @"sed do eiusmod tempor incididunt ut labore et dolore magna " + 
                    @"aliqua. Ut enim ad minim veniam, quis nostrud exercitation " + 
                    @"ullamco laboris nisi ut aliquip ex ea commodo consequat. " + 
                    @"Duis aute irure dolor in reprehenderit in voluptate velit " + 
                    @"esse cillum dolore eu fugiat nulla pariatur. Excepteur sint " + 
                    @"occaecat cupidatat non proident, sunt in culpa qui officia " + 
                    @"deserunt mollit anim id est laborum.",
                "Проверка уникодной строки"
            };

            public static readonly IEnumerable<Int32> Int32 = Enumerable.Range(-100, 200);
        }
    }
}