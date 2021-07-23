using System.Text.RegularExpressions;
using UnityEngine;

/*===============================================================
Project:	Poop Deck
Developer:	Marci San Diego
Company:	David Morgan Education - marcianosd@dm-ed.com
Date:		17/06/2020 11:41
===============================================================*/

namespace DMED.Systems.AnalyticsSystem
{
	[CreateAssetMenu(menuName = "DMED/Systems/Analytics System/Parameter Preset", order = 4)]
	public class ParameterPreset : ScriptableObject
	{
		protected static readonly Regex LOWER_SNAKE_CASE_REGEX = new Regex(@"^[a-z][a-z0-9_]+$");

		[SerializeField]
		private string _name = string.Empty;

		[SerializeField]
		private ParameterType _type = ParameterType.String;

		public new string name => _name;

		public ParameterType type => _type;

		public bool isValid => LOWER_SNAKE_CASE_REGEX.IsMatch(_name);

		public override string ToString() => $"{_name}:{_type}";
	}
}
