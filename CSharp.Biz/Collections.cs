using System;
using System.Collections.Generic;

namespace CSharp.Biz
{
    public class Collections
    {
        public Collections()
        {
            //var states = new Dictionary<string, string>();
            //states.Add("CA", "California");
            //states.Add("WA", "Washington");
            //states.Add("NY", "New York");

            var states = new Dictionary<string, string>()
            {
                { "CA", "California" },
            { "WA", "Washington"},
            { "NY", "New York"}
        };
            Console.WriteLine(states);
        }
        public Collections(int collectionsId,
                        string collectionsName,
                        string description) : this()
        {
            this.CollectionsId = collectionsId;
            this.CollectionsName = collectionsName;
            this.Description = description;
        }
        public string Description { get; set; }
        public decimal Cost { get; set; }

        public int CollectionsId { get; set; }

        private string collectionsName;
        public string CollectionsName
        {
            get
            {
                var formattedValue = collectionsName?.Trim();
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";

                }
                else
                {
                    collectionsName = value;

                }
            }
        }
        public string ValidationMessage { get; private set; }
        public override string ToString()
        {
            return this.CollectionsName + " (" + this.CollectionsId + ")";
        }

    }
}