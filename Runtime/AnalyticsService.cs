using System;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		30/01/2019 19:47
===============================================================*/

namespace MSD.Systems.Analytics
{
	public abstract class AnalyticsService : ScriptableObject
	{
		[field: NonSerialized]
		public bool IsInitialized { get; private set; }

		public bool CanLogEvents {
			get {
#if UNITY_EDITOR
				return IsInitialized && AnalyticsConfig.IsEditorEventsAllowed;
#else
				return isInitialized;
#endif
			}
		}

		public abstract EventNameFormatSpecifier EventNameFormatSpecifier { get; }

#if UNITY_EDITOR
		public abstract string EventNameFormatBlob { get; }
#endif

		protected abstract string DebugPrefix { get; }

		internal void Initialize()
		{
			if (IsInitialized) { return; }

			DoInitialize();
			IsInitialized = true;
		}

		public void LogEvent(AnalyticsEvent analyticsEvent)
		{
			if (!CheckLoggingAvailability()) { return; }

			if (analyticsEvent.Parameters.Count == 0) {
				DoLogEvent(analyticsEvent.EventName);
			} else {
				DoLogEvent(analyticsEvent.EventName, analyticsEvent.Parameters);
			}
		}

		public void LogEvent(string eventName)
		{
			if (!CheckLoggingAvailability()) { return; }
			DoLogEvent(eventName);
		}

		public void LogEvent(string eventName, Parameters parameters)
		{
			if (!CheckLoggingAvailability()) { return; }
			DoLogEvent(eventName, parameters);
		}

		protected abstract void DoInitialize();

		protected abstract void DoLogEvent(string eventName);

		protected abstract void DoLogEvent(string eventName, Parameters parameters);

		private bool CheckLoggingAvailability()
		{
			if (!IsInitialized) {
				Debugger.LogWarning(DebugPrefix, "Uninitialized service! Event not logged...");
				return false;
			}

			if (!CanLogEvents) {
				Debugger.LogWarning(DebugPrefix, "Service is initialized, but events logging is disabled.");
				return false;
			}

			return true;
		}
	}
}
