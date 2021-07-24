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
	[CreateAssetMenu(menuName = "MSD/Systems/Analytics/Event Context Component", order = 3)]
	public class EventContextComponent : ScriptableObject
	{
		protected static readonly Regex UPPER_CAMEL_CASE_REGEX = new Regex(@"^[A-Z][A-Za-z0-9]+$");

		[SerializeField]
		private string _value = string.Empty;

		[SerializeField]
		private EventContextComponentType _type = EventContextComponentType.Subject;

		public string Value => _value;

		public EventContextComponentType Type => _type;

		public bool IsValid => UPPER_CAMEL_CASE_REGEX.IsMatch(_value);

		public override string ToString() => _value;

		public static implicit operator string(EventContextComponent nameSection) => nameSection._value;
	}
}
