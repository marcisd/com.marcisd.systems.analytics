using System;
using System.Text;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	DefaultCompany - marcianosd@dm-ed.com
Date:		30/01/2019 01:16
===============================================================*/

namespace DMED.Systems.AnalyticsSystem 
{
	[Serializable]
	public class Parameters : SerializableDictionary<string, ParameterValue>
	{
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder("Params [");
			foreach (var pair in this) {
				sb.Append(pair.Key);
				sb.Append(": ");
				sb.Append(string.Format("({0})", pair.Value.type));
				sb.Append(pair.Value.objectValue);
				sb.Append(", ");
			}
			sb.Append("]");
			return sb.ToString();
		}
	}
}
