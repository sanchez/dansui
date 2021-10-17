using Sanchez.DansUI.Attributes;
using Sanchez.DansUI.Runner.Blazor.Components;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Sanchez.DansUI.Runner.Blazor.Models
{
    public class RegisteredPerson
    {
        [TableHeaderVisible(false)]
        [TableSortable]
        public string Id { get; set; }

        [TableSortable]
        [TableCellSuffix(typeof(TableCellBin))]
        [TableHeaderDisplayName("First Name")]
        public string FirstName { get; set; }

        [TableHeaderDisplayName("Surname")]
        public string LastName { get; set; }

        [TableSortable]
        public int Age { get; set; }

        [TableSortable]
        public string PostCode { get; set; }

        public static readonly IList<string> CommonFirstNames = new List<string>()
        {
            "James",
            "Mary",
            "Robert",
            "Patricia",
            "John",
            "Jennifer",
            "Michael",
            "Linda",
            "William",
            "Elizabeth",
            "David",
            "Barbara",
            "Richard",
            "Susan",
            "Joseph",
            "Jessica",
            "Thomas",
            "Sarah",
            "Charles",
            "Karen"
        };

        public static readonly IList<string> CommonLastNames = new List<string>()
        {
            "Smith",
            "Johnson",
            "Williams",
            "Brown",
            "Jones",
            "Garcia",
            "Miller",
            "Davis",
            "Rodriguez",
            "Martinez"
        };

        public static RegisteredPerson CreateRandom()
        {
            var random = new Random();

            return new RegisteredPerson()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = CommonFirstNames[random.Next(CommonFirstNames.Count)],
                LastName = CommonLastNames[random.Next(CommonLastNames.Count)],
                Age = random.Next(18, 85),
                PostCode = string.Format("{0}{1}{2}{3}", random.Next(9), random.Next(9), random.Next(9), random.Next(9))
            };
        }

        public static RegisteredPerson Create(string firstName, string lastName, int age, string postCode)
        {
            return new RegisteredPerson()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = firstName,
                LastName = lastName,
                Age = age,
                PostCode = postCode
            };
        }

        public static ICollection<RegisteredPerson> GeneratePeopleList(int count)
        {
            var people = Enumerable.Range(0, count).Select(x => CreateRandom()).ToList();

            return people;
        }
    }
}
