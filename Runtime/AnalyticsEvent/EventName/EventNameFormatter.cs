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
	public class EventNameFormatter
	{
		[SerializeField]
		private List<EventNameSection> _sections = new List<EventNameSection>();

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
			return _sections.Where(section => section != null).Select(section => section.Value).ToArray();
		}

		private int CompareSections(EventNameSection lhs, EventNameSection rhs)
		{
			if (lhs == null) return -1;
			if (rhs == null) return 1;
			return (int)lhs.Type - (int)rhs.Type;
		}

		private bool ValidSection(EventNameSection eventNameSection)
		{
			// since we ignore null sections
			if (eventNameSection == null) return true;
			return eventNameSection.IsValid;
		}

		private bool IsVerbSection(EventNameSection eventNameSection)
		{
			if (eventNameSection == null) return false;
			return eventNameSection.Type == EventNameSection.SectionType.Verb;
		}

		private bool IsClassificationSection(EventNameSection eventNameSection)
		{
			if (eventNameSection == null) return false;
			return eventNameSection.Type == EventNameSection.SectionType.Classification;
		}
	}
}
