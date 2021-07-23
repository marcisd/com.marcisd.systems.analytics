using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

/*===============================================================
Project:	Poop Deck
Developer:	Marci San Diego
Company:	David Morgan Education - marcianosd@dm-ed.com
Date:		17/06/2020 16:24
===============================================================*/

namespace DMED.Systems.AnalyticsSystem
{
	[Serializable]
	public class ParameterFormatter : IEnumerable<ParameterPreset>
	{
		public static readonly int maxParameterCount = 10;

		private static readonly string SEPARATOR = ", ";

		[SerializeField]
		private List<ParameterPreset> _presets = new List<ParameterPreset>();

		public int count => _presets.Count;

		public bool isValid => _presets.All(ValidParameterPreset);

		public void Validate()
		{
			if (_presets.Count > maxParameterCount) {
				int diff = _presets.Count - maxParameterCount;
				_presets.RemoveRange(maxParameterCount - 1, diff);
			}
		}

		public IDictionary<string, ParameterType> GetParameterFormat()
		{
			var nonEmpty = new List<ParameterPreset>(_presets);
			nonEmpty.RemoveAll(elt => elt == null);
			return nonEmpty.ToDictionary((p) => p.name, (p) => p.type);
		}

		public override string ToString()
		{
			return string.Join(SEPARATOR, _presets);
		}

		public IEnumerator<ParameterPreset> GetEnumerator()
		{
			return _presets.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _presets.GetEnumerator();
		}

		private bool ValidParameterPreset(ParameterPreset parameterPreset)
		{
			// since we ignore null presets
			if (parameterPreset == null) return true;
			return parameterPreset.isValid;
		}
	}
}
