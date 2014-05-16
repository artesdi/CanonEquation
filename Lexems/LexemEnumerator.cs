using System;
using System.Collections;
using System.Collections.Generic;

namespace CanonEquation.Lexems
{
	public sealed class LexemEnumerator : IEnumerator<char>, IDisposable
	{
		private string _str;
		private char _currentElement;
		private int _currentIndex;

		public LexemEnumerator(string str)
		{
			if (str == null) throw new ArgumentNullException("str");
			
			_str = str;
			_currentIndex = -1;
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}

		public char Current
		{
			get
			{
				if (_currentIndex < 0 || _currentIndex >= _str.Length)
					throw new InvalidOperationException("Index out of bounds");
		
				return _currentElement;
			}
		}

		public bool MoveNext()
		{
			if (_currentIndex < (_str.Length - 1))
			{
				_currentIndex++;
				_currentElement = _str[_currentIndex];
				return true;
			}
			
			_currentIndex = _str.Length;
			return false;
		}

		public bool IsHasValue
		{
			get { return _currentIndex >= 0 && _currentIndex < _str.Length; }
		}

		public void Reset()
		{
			_currentElement = (char)0;
			_currentIndex = -1; 
		}

		public void Dispose()
		{
			if (_str != null)
				_currentIndex = _str.Length;
			_str = null; 
		}
	}
}