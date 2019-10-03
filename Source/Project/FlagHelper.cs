using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using EPiServer.ServiceLocation;

namespace RegionOrebroLan.EPiServer
{
	[ServiceConfiguration(typeof(IFlagHelper), Lifecycle = ServiceInstanceScope.Singleton)]
	public class FlagHelper : IFlagHelper
	{
		#region Fields

		private const char _separator = ',';

		#endregion

		#region Properties

		protected internal virtual ConcurrentDictionary<Type, bool> IsFlagCache { get; } = new ConcurrentDictionary<Type, bool>();
		protected internal virtual char Separator => _separator;
		protected internal virtual ConcurrentDictionary<Type, IEnumerable<string>> ValuesCache { get; } = new ConcurrentDictionary<Type, IEnumerable<string>>();

		#endregion

		#region Methods

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual T ConvertFromString<T>(string value) where T : Enum
		{
			this.ValidateGenericTypeParameter<T>();

			T flags = default;

			// ReSharper disable All
			if(!string.IsNullOrWhiteSpace(value))
			{
				var values = this.GetValues<T>().ToArray();
				var flagsValue = 0;

				foreach(var part in value.Split(this.Separator).Select(item => item.Trim()).Where(item => !string.IsNullOrEmpty(item)))
				{
					if(!values.Contains(part, StringComparer.OrdinalIgnoreCase))
						continue;

					var flag = (T) Enum.Parse(typeof(T), part);

					if(flags.HasFlag(flag))
						continue;

					flagsValue += (int) (object) flag;

					flags = (T) (object) flagsValue;
				}
			}
			// ReSharper restore All

			return flags;
		}

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		public virtual string ConvertToString<T>(T flags) where T : Enum
		{
			this.ValidateGenericTypeParameter<T>();

			// ReSharper disable SuspiciousTypeConversion.Global
			return string.Join(this.Separator.ToString(CultureInfo.InvariantCulture), Enum.GetValues(typeof(T)).Cast<Enum>().Where(flags.HasFlag).Cast<int>().Where(item => item > 0));
			// ReSharper restore SuspiciousTypeConversion.Global
		}

		protected internal virtual IEnumerable<string> GetValues<T>() where T : Enum
		{
			this.ValidateGenericTypeParameter<T>();

			return this.ValuesCache.GetOrAdd(typeof(T), key => { return Enum.GetValues(key).Cast<int>().Where(value => value > 0).Select(item => item.ToString(CultureInfo.InvariantCulture)).ToArray(); });
		}

		public virtual bool IsFlag<T>() where T : Enum
		{
			return this.IsFlagCache.GetOrAdd(typeof(T), key => key.GetCustomAttributes(typeof(FlagsAttribute), true).Any());
		}

		[SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters")]
		protected internal virtual void ValidateGenericTypeParameter<T>() where T : Enum
		{
			if(!this.IsFlag<T>())
				throw new ArgumentException("T must have a flag-attribute.");
		}

		#endregion
	}
}