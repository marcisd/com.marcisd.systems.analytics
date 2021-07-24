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
	[CreateAssetMenu(menuName = "MSD/Systems/Analytics/Analytics Event Format", order = 2)]
	public partial class AnalyticsEventFormat : ScriptableObject
	{
		[SerializeField]
		private AnalyticsService _serviceDestination;

		[Space]
		[SerializeField]
		private EventNameFormatter _nameFormatter;

		[Space]
		[SerializeField]
		private ParameterFormatter _parameterFormatter;

		[HideInInspector]
		[SerializeField]
		private List<string> _validationLogs = new List<string>();

		public AnalyticsService ServiceDestination => _serviceDestination;
		public string EventName => _serviceDestination.EventNameFormatSpecifier.FormatEventName(_nameFormatter.NameSections());
		public IDictionary<string, ParameterType> ParameterFormat => _parameterFormatter.GetParameterFormat();
		public int ParameterCount => _parameterFormatter.count;

		public bool IsValid { get; private set; } = true;

		private void OnValidate()
		{
			_nameFormatter.Validate();
			_parameterFormatter.Validate();

			_validationLogs.Clear();
			IsValid = true;

			Validation.ForEachType((type) => {
				var validation = Validation.Factory(type);
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
