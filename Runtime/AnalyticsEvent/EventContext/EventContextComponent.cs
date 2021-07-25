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
	/// <summary>
	/// Building blocks of an <see cref="EventContext"/> grammar.
	/// </summary>
	internal abstract class EventContextComponent : ScriptableObject
	{
		protected static readonly Regex UPPER_CAMEL_CASE_REGEX = new Regex(@"^[A-Z][A-Za-z0-9]+$");

		[SerializeField]
		private string _name = string.Empty;

		[SerializeField]
		[ReadOnly]
		protected EventContextComponentType _type = EventContextComponentType.Subject;

		public string Name => _name;

		public EventContextComponentType Type => _type;

		public bool IsValid => UPPER_CAMEL_CASE_REGEX.IsMatch(_name);

		public override string ToString() => _name;

		public static implicit operator string(EventContextComponent nameSection) => nameSection._name;
	}
}
