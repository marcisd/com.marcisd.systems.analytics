using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/01/2019 21:51
===============================================================*/

namespace MSD.Systems.Analytics
{
	/// <summary>
	/// Provides the specification for an <see cref="AnalyticsEvent"/>.
	/// </summary>
	[CreateAssetMenu(menuName = "MSD/Systems/Analytics/Analytics Event Format", order = 2)]
	internal partial class AnalyticsEventFormat : ScriptableObject
	{
		[SerializeField]
		private AnalyticsService _serviceDestination;

		[Space]
		[SerializeField]
		private EventContext _eventContext;

		[Space]
		[SerializeField]
		private ParameterFormatter _parameterFormatter;

		[HideInInspector]
		[SerializeField]
		private List<string> _validationLogs = new List<string>();

		/// <summary>
		/// Specifies where the <see cref="AnalyticsEvent"/> will be logged.
		/// </summary>
		public AnalyticsService ServiceDestination => _serviceDestination;

		/// <summary>
		/// Specifies the name of the <see cref="AnalyticsEvent"/>.
		/// </summary>
		public string EventName => _serviceDestination.EventNameFormatSpecifier.FormatEventName(_eventContext);

		/// <summary>
		/// Specifies the parameters to be sent with the <see cref="AnalyticsEvent"/>.
		/// </summary>
		public IDictionary<string, ParameterValueType> ParameterFormat => _parameterFormatter.GetParameterFormat();

		public int ParameterCount => _parameterFormatter.Count;

		[field: NonSerialized]
		public bool IsValid { get; private set; } = true;

		private void OnValidate()
		{
			_eventContext.Validate();
			_parameterFormatter.Validate();

			_validationLogs.Clear();
			IsValid = true;

			Validation.ForEachType((type) => {
				Validation validation = Validation.Factory(type);
				if (validation.Test(this)) {
					IsValid = false;
					_validationLogs.Add(validation.Log);
				}
			});
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(EventName);
			sb.Append(":");
			sb.Append(_parameterFormatter);
			return sb.ToString();
		}
	}
}
