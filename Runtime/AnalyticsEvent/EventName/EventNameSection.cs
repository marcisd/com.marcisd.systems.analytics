using System.Text.RegularExpressions;
using UnityEngine;

/*===============================================================
Project:	Poop Deck
Developer:	Marci San Diego
Company:	David Morgan Education - marcianosd@dm-ed.com
Date:		17/06/2020 08:13
===============================================================*/

namespace DMED.Systems.AnalyticsSystem
{
	[CreateAssetMenu(menuName = "DMED/Systems/Analytics System/Event Name Section", order = 3)]
	public class EventNameSection : ScriptableObject
	{
		protected static readonly Regex UPPER_CAMEL_CASE_REGEX = new Regex(@"^[A-Z][A-Za-z0-9]+$");

		public enum Type
		{
			Classification,
			Subject,
			Verb,
		}

		[SerializeField]
		private string _value = string.Empty;

		[SerializeField]
		private Type _type = Type.Subject;

		public string value => _value;

		public Type type => _type;

		public bool isValid => UPPER_CAMEL_CASE_REGEX.IsMatch(_value);

		public override string ToString() => _value;

		public static implicit operator string(EventNameSection nameSection)
		{
			return nameSection._value;
		}
	}
}
