using System;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	DefaultCompany - marcianosd@dm-ed.com
Date:		30/01/2019 19:47
===============================================================*/

namespace DMED.Systems.AnalyticsSystem
{
	public abstract class AnalyticsServiceBase : ScriptableObject
	{
		[field: NonSerialized] public bool isInitialized { get; private set; }

		public bool canLogEvents {
			get {
#if UNITY_EDITOR
				return isInitialized && AnalyticsConfig.isEditorEventsAllowed;
#else
				return isInitialized;
#endif
			}
		}

		internal void Initialize()
		{
			if (isInitialized) { return; }

			DoInitialize();
			isInitialized = true;
		}

		protected abstract void DoInitialize();

		public void LogEvent(AnalyticsEvent analyticsEvent)
		{
			if (!CheckLoggingAvailability()) { return; }

			if (analyticsEvent.parameters.Count == 0) {
				DoLogEvent(analyticsEvent.eventName);
			} else {
				DoLogEvent(analyticsEvent.eventName, analyticsEvent.parameters);
			}
		}

		public void LogEvent(string eventName)
		{
			if (!CheckLoggingAvailability()) { return; }
			DoLogEvent(eventName);
		}

		protected abstract void DoLogEvent(string eventName);

		public void LogEvent(string eventName, Parameters parameters)
		{
			if (!CheckLoggingAvailability()) { return; }
			DoLogEvent(eventName, parameters);
		}

		protected abstract void DoLogEvent(string eventName, Parameters parameters);

		private bool CheckLoggingAvailability()
		{
			if (!isInitialized) {
				Debugger.LogWarning("Uninitialized service, aborting event logging...");
				return false;
			}

			if (!canLogEvents) {
				Debugger.LogWarning("Service is initialized, but events logging is disabled...");
				return false;
			}

			return true;
		}
	}
}
