using System.Text.RegularExpressions;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		17/06/2020 11:41
===============================================================*/

namespace MSD.Systems.Analytics
{
	/// <summary>
	/// Provides a specification for event parameters.
	/// </summary>
	[CreateAssetMenu(menuName = "MSD/Systems/Analytics/Parameter Preset", order = 5)]
	internal class ParameterPreset : ScriptableObject
	{
		protected static readonly Regex LOWER_SNAKE_CASE_REGEX = new Regex(@"^[a-z][a-z0-9_]+$");

		[SerializeField]
		private string _name = string.Empty;

		[SerializeField]
		private ParameterValueType _type = ParameterValueType.String;

		public string Name => _name;

		public ParameterValueType Type => _type;

		public bool IsValid => LOWER_SNAKE_CASE_REGEX.IsMatch(_name);

		public override string ToString() => $"{_name}: {_type}";
	}
}
