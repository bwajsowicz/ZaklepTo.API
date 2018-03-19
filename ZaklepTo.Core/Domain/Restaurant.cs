using System;
using System.Collections.Generic;
using System.Text;

namespace ZaklepTo.Core.Domain
{
    class Restaurant
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Cousine { get; private set; }
        public string Localization { get; private set; }
        private List<Table> _tables { get; set; }
        public IEnumerable<Table> Tables => _tables;
    }
}
