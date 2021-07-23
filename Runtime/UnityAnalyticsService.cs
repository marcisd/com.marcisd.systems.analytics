using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	DefaultCompany - marcianosd@dm-ed.com
Date:		30/01/2019 21:03
===============================================================*/

namespace DMED.Systems.AnalyticsSystem 
{
	using Analytics = UnityEngine.Analytics.Analytics;

	[CreateAssetMenu(menuName = "DMED/Systems/Analytics System/Unity Analytics Service", order = 51)]
	public class UnityAnalyticsService : AnalyticsServiceBase
	{
		private Dictionary<string, object> _parameters = new Dictionary<string, object>();

		protected override void DoInitialize() { }

		protected override void DoLogEvent(string eventName)
		{
			Analytics.CustomEvent(eventName);
		}

		protected override void DoLogEvent(string eventName, Parameters parameters)
		{
			ProcessParameters(parameters);
			Analytics.CustomEvent(eventName, _parameters);
		}

		private void ProcessParameters(Parameters parameters)
		{
			_parameters.Clear();
			foreach (var parameter in parameters) {
				_parameters.Add(parameter.Key, parameter.Value.objectValue);
			}
		}
	}
}
