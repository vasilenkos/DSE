using System;
using System.Linq;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace DSE.Tests.Symptomatic
{
    public static partial class Defaults
    {
        public static class Class
        {
            public class Person
            {
                public String FirstName;
                public String LastName;
                public String SecondName;
            }

            public static readonly Person DefaultPerson1 = new Person()
            {
                FirstName = "Daniel",
                SecondName = "Whoever",
                LastName = "Waterhouse"
            };

            public static readonly Person DefaultPerson2 = new Person()
            {
                FirstName = "Enoch",
                SecondName = "",
                LastName = "Root"
            };

            public static readonly Person DefaultPerson3 = new Person()
            {
                FirstName = "Bob",
                SecondName = "",
                LastName = "Shaftoe"
            };

            public static readonly Person[] Persons = new Person[]
            {
                DefaultPerson1,
                DefaultPerson2,
                DefaultPerson3
            };
        }
    }
}