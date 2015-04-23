using System;
using System.Collections.Generic;

namespace CSharpSnippets.Decorators.Dictionary
{
	public class DefaultDictDecorator<K,T> : IDictionary<K,T>
	{
		private IDictionary<K,T> Dictionary { get; set; }
		private Func<T> DefaultFactory { get; set; }

		public DefaultDictDecorator(IDictionary<K,T> dict, Func<T> defaultFactory )
		{
			this.Dictionary = dict;
			this.DefaultFactory = defaultFactory;
		}

		public void Add (K key, T value)
		{
			this.Dictionary.Add (key, value);
		}

		public bool ContainsKey (K key)
		{
			return this.Dictionary.ContainsKey (key);
		}

		public bool Remove (K key)
		{
			return this.Dictionary.Remove (key);
		}

		public void Add (KeyValuePair<K, T> item)
		{
			this.Dictionary.Add (item);
		}

		public void Clear ()
		{
			this.Dictionary.Clear ();
		}

		public bool Contains (KeyValuePair<K, T> item)
		{
			return this.Dictionary.Contains (item);
		}

		public void CopyTo (KeyValuePair<K, T>[] array, int arrayIndex)
		{
			this.Dictionary.CopyTo (array, arrayIndex);
		}

		public bool Remove (KeyValuePair<K, T> item)
		{
			return this.Dictionary.Remove (item);
		}

		public IEnumerator<KeyValuePair<K, T>> GetEnumerator ()
		{
			return this.Dictionary.GetEnumerator ();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator ()
		{
			return this.Dictionary.GetEnumerator ();
		}

		public ICollection<K> Keys {
			get {
				return this.Dictionary.Keys;
			}
		}

		public ICollection<T> Values {
			get {
				return this.Dictionary.Values;
			}
		}

		public int Count {
			get {
				return this.Dictionary.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return this.Dictionary.IsReadOnly;
			}
		}

		public bool TryGetValue (K key, out T value)
		{
			return this.Dictionary.TryGetValue (key, out value);
		}

		/* the only difference */
		public T this[K key] {
			get {
				T result;
				if (!this.Dictionary.TryGetValue (key, out result)) {
					result = this.Dictionary [key] = this.DefaultFactory ();
				}
				return result;
			}

			set {
				this.Dictionary [key] = value;
			}
		}
	}
}

