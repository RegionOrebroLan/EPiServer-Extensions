using System;

namespace RegionOrebroLan.EPiServer
{
	public interface IFlagHelper
	{
		#region Methods

		T ConvertFromString<T>(string value) where T : Enum;
		string ConvertToString<T>(T flags) where T : Enum;
		bool IsFlag<T>() where T : Enum;

		#endregion
	}
}