using System;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/01/2019 20:36
===============================================================*/

namespace MSD.Systems.Analytics
{
	/// <summary>
	/// Provides the means of executing an analytics event.
	/// </summary>
	[CreateAssetMenu(menuName = "MSD/Systems/Analytics/Analytics Event", order = 1)]
	public class AnalyticsEvent : ScriptableObject
	{
		private static readonly string DEBUG_PREPEND = $"[{nameof(AnalyticsEvent)}]";

		[HelpBox("Read-only values are derived from the AnalyticsEventFormat.")]
		[ReadOnly]
		[SerializeField]
		private AnalyticsService _serviceDestination;

		[ReadOnly]
		[SerializeField]
		private string _eventName;

		[SerializeField]
		private Parameters _parameters;

		public bool IsServiceInitialized => _serviceDestination.IsInitialized;

		public string EventName => _eventName;

		public Parameters Parameters => _parameters;

		/// <summary>
		/// Logs the defined analytics event to its destination.
		/// </summary>
		public void LogAnalyticsEvent()
		{
			Debugger.Log(DEBUG_PREPEND,
				$"Sending analytics event." + Environment.NewLine +
				$"Name [{_eventName}]" + Environment.NewLine +
				Parameters.ToString()
			);

			_serviceDestination.LogEvent(this);
		}

		public override string ToString()
		{
			return "Name: " + EventName + "; " + Parameters;
		}

#if UNITY_EDITOR

		[SerializeField]
		private AnalyticsEventFormat _format;

		[SerializeField]
		[HideInInspector]
		private string _eventFormatCache = string.Empty;

		public bool IsFormatUpToDate => _eventFormatCache == FormatString;

		private string FormatString => Format != null ? Format.ToString() : string.Empty;

		internal AnalyticsEventFormat Format {
			get { return _format; }
			set {
				if (SetPropertyUtility.SetClass(ref _format, value)) {
					Apply();
				}
			}
		}

		internal void Apply()
		{
			if (_format == null) {
				UnityEditor.EditorUtility.DisplayDialog("Analytics Event", "Assign an Analytics Event Format first.", "Ok");
			} else {
				if (_format.IsValid) {
					ApplyFormat();
				} else {
					UnityEditor.EditorUtility.DisplayDialog("Analytics Event", "Analytics Event Format must be valid.", "Ok");
				}
			}
		}

		private void ApplyFormat()
		{
			if (_format != null) {
				_serviceDestination = Format.ServiceDestination;
				_eventName = _format.EventName;

				_parameters.Clear();
				foreach (KeyValuePair<string, ParameterValueType> paramFormat in _format.ParameterFormat) {
					_parameters.Add(paramFormat.Key, new ParameterValue(paramFormat.Value));
				}

				_eventFormatCache = Format.ToString();
			}
		}

#endif

	}
}
