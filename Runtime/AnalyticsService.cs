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
	/// <summary>
	/// The abstraction of an Analytics Service implementation.
	/// This allows the system to log analytics events into diffetent services.
	/// </summary>
	public abstract class AnalyticsService : ScriptableObject
	{
		[field: NonSerialized]
		internal bool IsInitialized { get; private set; }

		internal bool CanLogEvents {
			get {
#if UNITY_EDITOR
				return IsInitialized && AnalyticsConfig.IsEditorEventsAllowed;
#else
				return isInitialized;
#endif
			}
		}

		internal abstract EventNameFormatSpecifier EventNameFormatSpecifier { get; }

#if UNITY_EDITOR
		internal abstract string EventNameFormatBlob { get; }
#endif

		protected abstract string DebugPrefix { get; }

		internal virtual void Bootstrap() { }

		internal void Initialize()
		{
			if (!IsInitialized) {
				DoInitialize();
				IsInitialized = true;
			}
		}

		internal void LogEvent(AnalyticsEvent analyticsEvent)
		{
			if (CheckLoggingAvailability()) {
				if (analyticsEvent.Parameters.Count == 0) {
					DoLogEvent(analyticsEvent.EventName);
				} else {
					DoLogEvent(analyticsEvent.EventName, analyticsEvent.Parameters);
				}
			}
		}

		internal void LogEvent(string eventName)
		{
			if (CheckLoggingAvailability()) {
				DoLogEvent(eventName);
			}
		}

		internal void LogEvent(string eventName, Parameters parameters)
		{
			if (CheckLoggingAvailability()) {
				DoLogEvent(eventName, parameters);
			}
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
