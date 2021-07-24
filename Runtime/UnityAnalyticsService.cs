using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		30/01/2019 21:03
===============================================================*/

namespace MSD.Systems.Analytics
{
	using UnityAnalytics = UnityEngine.Analytics.Analytics;

	[CreateAssetMenu(menuName = "MSD/Systems/Analytics/Unity Analytics Service", order = 51)]
	public class UnityAnalyticsService : AnalyticsService
	{
		private static readonly string DEBUG_PREFIX = $"[{nameof(UnityAnalyticsService)}]";

		private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

		public override EventNameFormatSpecifier EventNameFormatSpecifier => new EventNameFormatSpecifier(".", 100);

#if UNITY_EDITOR
		public override string EventNameFormatBlob => "Custom event names should have at most 100 characters.";
#endif

		protected override string DebugPrefix => DEBUG_PREFIX;

		protected override void DoInitialize() { }

		protected override void DoLogEvent(string eventName)
		{
			UnityAnalytics.CustomEvent(eventName);
		}

		protected override void DoLogEvent(string eventName, Parameters parameters)
		{
			ProcessParameters(parameters);
			UnityAnalytics.CustomEvent(eventName, _parameters);
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
