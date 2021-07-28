using System;
using System.Collections.Generic;
using System.Text;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		30/01/2019 01:16
===============================================================*/

namespace MSD.Systems.Analytics 
{
	[Serializable]
	public class Parameters : SerializableDictionary<string, ParameterValue>
	{
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder("Params [");
			foreach (KeyValuePair<string, ParameterValue> pair in this) {
				sb.Append(pair.Key);
				sb.Append(": ");
				sb.Append(string.Format("({0})", pair.Value.Type));
				sb.Append(pair.Value.ObjectValue);
				sb.Append(", ");
			}
			sb.Append("]");
			return sb.ToString();
		}
	}
}
