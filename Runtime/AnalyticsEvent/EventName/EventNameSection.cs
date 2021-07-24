using System.Text.RegularExpressions;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		17/06/2020 08:13
===============================================================*/

namespace MSD.Systems.Analytics
{
	[CreateAssetMenu(menuName = "DMED/Systems/Analytics System/Event Name Section", order = 3)]
	public class EventNameSection : ScriptableObject
	{
		public enum SectionType
		{
			Classification,
			Subject,
			Verb,
		}

		protected static readonly Regex UPPER_CAMEL_CASE_REGEX = new Regex(@"^[A-Z][A-Za-z0-9]+$");

		[SerializeField]
		private string _value = string.Empty;

		[SerializeField]
		private SectionType _type = SectionType.Subject;

		public string Value => _value;

		public SectionType Type => _type;

		public bool IsValid => UPPER_CAMEL_CASE_REGEX.IsMatch(_value);

		public override string ToString() => _value;

		public static implicit operator string(EventNameSection nameSection) => nameSection._value;
	}
}
