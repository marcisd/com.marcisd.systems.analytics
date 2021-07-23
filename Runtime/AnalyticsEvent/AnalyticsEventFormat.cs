using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	DefaultCompany - marcianosd@dm-ed.com
Date:		29/01/2019 21:51
===============================================================*/

namespace DMED.Systems.AnalyticsSystem
{
	[CreateAssetMenu(menuName = "DMED/Systems/Analytics System/Analytics Event Format", order = 2)]
	public partial class AnalyticsEventFormat : ScriptableObject
	{
		[SerializeField] private AnalyticsServiceBase _serviceDestination = null;

		[Space]
		[SerializeField] private EventNameFormatter _nameFormatter = null;

		[Space]
		[SerializeField] private ParameterFormatter _parameterFormatter = null;

		[HideInInspector]
		[SerializeField] private List<string> _validationLogs = new List<string>();

		public AnalyticsServiceBase serviceDestination => _serviceDestination;
		public string eventName => _nameFormatter.ToString();
		public IDictionary<string, ParameterType> parameterFormat => _parameterFormatter.GetParameterFormat();
		public int parameterCount => _parameterFormatter.count;

		public bool isValid { get; private set; } = true;

		private void OnValidate()
		{
			_nameFormatter.Validate();
			_parameterFormatter.Validate();

			_validationLogs.Clear();
			isValid = true;

			Validation.ForEachType((type) => {
				var validation = Validation.Factory(type);
				if (validation.Test(this)) {
					isValid = false;
					_validationLogs.Add(validation.log);
				}
			});
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(eventName);
			sb.Append(":");
			sb.Append(_parameterFormatter);
			return sb.ToString();
		}
	}
}
