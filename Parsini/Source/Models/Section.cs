using System;
using System.Collections.Generic;
using Parsini.Collections;

namespace Parsini.Models
{
    public class Section : ISearchableByName
    {
        #region Properties
        public string Name { get; private set; }
        public Collection<Key> Keys { get; private set; }
        #endregion

        #region Constructors
        internal Section(string name)
        {
            this.Name = name;
            this.Keys = new Collection<Key>();
        }
        #endregion
    }
}