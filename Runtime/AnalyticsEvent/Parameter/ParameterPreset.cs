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
	[CreateAssetMenu(menuName = "MSD/Systems/Analytics/Parameter Preset", order = 5)]
	public class ParameterPreset : ScriptableObject
	{
		protected static readonly Regex LOWER_SNAKE_CASE_REGEX = new Regex(@"^[a-z][a-z0-9_]+$");

		[SerializeField]
		private string _name = string.Empty;

		[SerializeField]
		private ParameterType _type = ParameterType.String;

		public string Name => _name;

		public ParameterType Type => _type;

		public bool IsValid => LOWER_SNAKE_CASE_REGEX.IsMatch(_name);

		public override string ToString() => $"{_name}: {_type}";
	}
}
