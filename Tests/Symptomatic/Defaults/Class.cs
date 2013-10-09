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

            public class Freelancer : Person
            {
                public String[] Skills;
            }

            public class Scientist : Person
            {
                public String Direction;
            }

            public class Alchemist: Scientist
            {
                public Int32 Age;
            }

            public static readonly Scientist DefaultScientist = new Scientist()
            {
                FirstName = "Daniel",
                SecondName = "Whoever",
                LastName = "Waterhouse",
                Direction = "Common Science"
            };

            public static readonly Alchemist DefaultAlchemist = new Alchemist()
            {
                FirstName = "Enoch",
                SecondName = "",
                LastName = "Root",
                Direction = "Quicksilver Alchemy",
                Age = 666
            };

            public static readonly Freelancer DefaultFreelancer = new Freelancer()
            {
                FirstName = "Bob",
                SecondName = "",
                LastName = "Shaftoe",
                Skills = new String[] {
                    "Intelligence",
                    "Combat"
                }
            };

            public static readonly Person DefaultPerson1 = DefaultScientist;
            public static readonly Person DefaultPerson2 = DefaultAlchemist;
            public static readonly Person DefaultPerson3 = DefaultFreelancer;

            public static readonly Person[] Persons = new Person[]
            {
                DefaultPerson1,
                DefaultPerson2,
                DefaultPerson3
            };
        }
    }
}