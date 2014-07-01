// Type: System.Dynamic.ExpandoObject
// Assembly: System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Core.dll

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace System.Dynamic
{
    public sealed class ExpandoObject : IDynamicMetaObjectProvider, IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable, INotifyPropertyChanged
    {
        public ExpandoObject();

        #region IDictionary<string,object> Members

        void IDictionary<string, object>.Add(string key, object value);
        bool IDictionary<string, object>.ContainsKey(string key);
        bool IDictionary<string, object>.Remove(string key);
        bool IDictionary<string, object>.TryGetValue(string key, out object value);
        void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item);
        void ICollection<KeyValuePair<string, object>>.Clear();
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item);
        void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex);
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item);
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator();
        ICollection<string> IDictionary<string, object>.Keys { get; }
        ICollection<object> IDictionary<string, object>.Values { get; }
        object IDictionary<string, object>.this[string key] { get; set; }
        int ICollection<KeyValuePair<string, object>>.Count { get; }
        bool ICollection<KeyValuePair<string, object>>.IsReadOnly { get; }

        #endregion

        #region IDynamicMetaObjectProvider Members

        DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter);

        #endregion

        #region INotifyPropertyChanged Members

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged;

        #endregion
    }
}
