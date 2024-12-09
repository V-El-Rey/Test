using System;
using System.Reflection;

namespace Assets.App.Scripts.Core.Services.Base
{
	public class Singleton<T> where T : class
	{
		private static readonly Lazy<T> _lazy =
			new Lazy<T>(() => (T)typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, new Type[0], null).Invoke(null), true);

		public static T Instance { get { return _lazy.Value; } }

		public Singleton()
		{
		}
	}
}