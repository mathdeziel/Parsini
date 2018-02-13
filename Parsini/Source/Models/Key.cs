using Parsini.Collections;

namespace Parsini.Models
{
    public class Key : ISearchableByName
    {
        #region Properties
        public string Name { get; private set; }
        public string Value { get; private set; }
        #endregion

        #region Constructors
        internal Key(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        #endregion
    }
}