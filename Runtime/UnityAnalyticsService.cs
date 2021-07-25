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

	/// <summary>
	/// Unity Analytics implementation
	/// </summary>
	[CreateAssetMenu(menuName = "MSD/Systems/Analytics/Unity Analytics Service", order = 101)]
	public class UnityAnalyticsService : AnalyticsService
	{
		private static readonly string DEBUG_PREFIX = $"[{nameof(UnityAnalyticsService)}]";

		private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>();

		internal override EventNameFormatSpecifier EventNameFormatSpecifier => new EventNameFormatSpecifier(".", 100);

#if UNITY_EDITOR
		internal override string EventNameFormatBlob => "Custom event names should have at most 100 characters.";
#endif

		protected override string DebugPrefix => DEBUG_PREFIX;

		internal override void Bootstrap()
		{
			// Unity Analytics auto-initializes by default
			// but can be flagged to delay initialization
			if (!AnalyticsConfig.ShouldInitializeOnAppStart) {
				UnityAnalytics.initializeOnStartup = false;
			}
		}

		protected override void DoInitialize()
		{
			// If auto-initialization was prevented
			if (!UnityAnalytics.initializeOnStartup) {
				UnityAnalytics.ResumeInitialization();
			}
		}

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
				_parameters.Add(parameter.Key, parameter.Value.ObjectValue);
			}
		}
	}
}
