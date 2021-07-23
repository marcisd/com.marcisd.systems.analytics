using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/*===============================================================
Project:	Poop Deck
Developer:	Marci San Diego
Company:	David Morgan Education - marcianosd@dm-ed.com
Date:		17/06/2020 08:22
===============================================================*/

namespace DMED.Systems.AnalyticsSystem
{
	[Serializable]
	public class EventNameFormatter
	{
		[SerializeField]
		private string _separator = ".";

		[SerializeField]
		private List<EventNameSection> _sections = new List<EventNameSection>();

		public bool isValid => isValidName && isValidFormat;

		public bool isValidName => _sections.All(ValidSection);

		public bool isValidFormat => hasExactlyOneClassification && hasExactlyOneVerb;

		private bool hasExactlyOneClassification => _sections.Where(IsClassificationSection).Take(2).Count() == 1;

		private bool hasExactlyOneVerb => _sections.Where(IsVerbSection).Take(2).Count() == 1;

		public void Validate()
		{
			_sections.Sort(CompareSections);
		}

		public override string ToString()
		{
			var nonEmpty = new List<EventNameSection>(_sections);
			nonEmpty.RemoveAll(elt => elt == null);
			return string.Join(_separator, nonEmpty);
		}

		public static implicit operator string(EventNameFormatter nameFormatter)
		{
			return nameFormatter.ToString();
		}

		private bool CompareSections(EventNameSection lhs, EventNameSection rhs)
		{
			if (lhs == null) return true;
			if (rhs == null) return false;
			return (int)lhs.type > (int)rhs.type;
		}

		private bool ValidSection(EventNameSection eventNameSection)
		{
			// since we ignore null sections
			if (eventNameSection == null) return true;
			return eventNameSection.isValid;
		}

		private bool IsVerbSection(EventNameSection eventNameSection)
		{
			if (eventNameSection == null) return false;
			return eventNameSection.type == EventNameSection.Type.Verb;
		}

		private bool IsClassificationSection(EventNameSection eventNameSection)
		{
			if (eventNameSection == null) return false;
			return eventNameSection.type == EventNameSection.Type.Classification;
		}
	}
}
