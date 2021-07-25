using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		17/06/2020 08:22
===============================================================*/

namespace MSD.Systems.Analytics
{
	[Serializable]
	public class EventContext
	{
		[SerializeField]
		private List<EventContextComponent> _sections = new List<EventContextComponent>();

		public bool IsValid => IsValidName && IsValidFormat;

		public bool IsValidName => _sections.All(ValidSection);

		public bool IsValidFormat => HasExactlyOneClassification && HasExactlyOneVerb;

		private bool HasExactlyOneClassification => _sections.Where(IsClassificationSection).Take(2).Count() == 1;

		private bool HasExactlyOneVerb => _sections.Where(IsVerbSection).Take(2).Count() == 1;

		public void Validate()
		{
			_sections.Sort(CompareSections);
		}

		public string[] NameSections()
		{
			return _sections.Where(section => section != null).Select(section => section.Name).ToArray();
		}

		private int CompareSections(EventContextComponent lhs, EventContextComponent rhs)
		{
			return lhs == null ? -1 : rhs == null ? 1 : (int)lhs.Type - (int)rhs.Type;
		}

		private bool ValidSection(EventContextComponent eventNameSection)
		{
			// since we ignore null sections
			return eventNameSection == null || eventNameSection.IsValid;
		}

		private bool IsVerbSection(EventContextComponent eventNameSection)
		{
			return eventNameSection != null && eventNameSection.Type == EventContextComponentType.Verb;
		}

		private bool IsClassificationSection(EventContextComponent eventNameSection)
		{
			return eventNameSection != null && eventNameSection.Type == EventContextComponentType.Classification;
		}
	}
}
