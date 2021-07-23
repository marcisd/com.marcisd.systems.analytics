using System;
using System.Collections.Generic;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	DefaultCompany - marcianosd@dm-ed.com
Date:		29/01/2019 20:36
===============================================================*/

namespace DMED.Systems.AnalyticsSystem
{
	[CreateAssetMenu(menuName = "DMED/Systems/Analytics System/Analytics Event", order = 1)]
	public class AnalyticsEvent : ScriptableObject
	{
		[HelpBox("Read-only values are derived from the AnalyticsEventFormat.")]
		[ReadOnly] [SerializeField] private AnalyticsServiceBase _serviceDestination;
		[ReadOnly] [SerializeField] private string _eventName;
		[SerializeField] private Parameters _parameters = null;

		public bool isServiceInitialized => _serviceDestination.isInitialized;

		public string eventName => _eventName;

		public Parameters parameters => _parameters;

		public void LogAnalyticsEvent()
		{
			Debugger.Log(
				$"[Analytics] Sending analytics event." + Environment.NewLine +
				$"Name [{_eventName}]" + Environment.NewLine +
				parameters.ToString()
			);

			_serviceDestination.LogEvent(this);
		}

		public override string ToString()
		{
			return "Name: " + eventName + "; " + parameters;
		}

#if UNITY_EDITOR
		[SerializeField] private AnalyticsEventFormat _format;

		[SerializeField]
		[HideInInspector]
		private string _eventFormatCache = string.Empty;

		public bool isFormatUpToDate => _eventFormatCache == formatString;

		private string formatString => format != null ? format.ToString() : string.Empty;

		public AnalyticsEventFormat format {
			get { return _format; }
			set {
				if (SetPropertyUtility.SetClass(ref _format, value)) {
					Apply();
				}
			}
		}

		public void Apply()
		{
			if (_format == null) {
				UnityEditor.EditorUtility.DisplayDialog("Analytics Event", "Assign an Analytics Event Format first.", "Ok");
			} else {
				if (_format.isValid) {
					ApplyFormat();
				} else {
					UnityEditor.EditorUtility.DisplayDialog("Analytics Event", "Analytics Event Format must be valid.", "Ok");
				}
			}
		}

		private void ApplyFormat()
		{
			if (_format != null) {
				_serviceDestination = format.serviceDestination;
				_eventName = _format.eventName;

				_parameters.Clear();
				foreach (var paramFormat in _format.parameterFormat) {
					_parameters.Add(paramFormat.Key, new ParameterValue(paramFormat.Value));
				}

				_eventFormatCache = format.ToString();
			}
		}
#endif
	}
}
