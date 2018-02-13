using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Parsini.Collections
{
    public class Collection<T> : IEnumerable where T: ISearchableByName
    {
        #region Members
        private List<T> list = new List<T>();
        #endregion

        #region Properties
        public int Count => this.list.Count;
        #endregion

        #region Public Methods
        public void Add(T item) => this.list.Add(item);
        public void Insert(int index, T item) => this.list.Insert(index, item);
        public T Last() => this.list.Last();
        #endregion

        #region Interface Implementation
        public IEnumerator GetEnumerator() => this.list.GetEnumerator();
        #endregion

        #region Operators
        public T this[int index] => (T)this.list[index];
        public T this[string key] => (T)this.list.Find(i => i.Name == key);
        #endregion
    }
}
