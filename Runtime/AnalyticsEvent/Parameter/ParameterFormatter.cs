using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		17/06/2020 16:24
===============================================================*/

namespace MSD.Systems.Analytics
{
	/// <summary>
	/// Specifies <see cref="ParameterPreset"/>s for an <see cref="AnalyticsEventFormat"/>
	/// </summary>
	[Serializable]
	internal class ParameterFormatter : IEnumerable<ParameterPreset>
	{
		internal static readonly int maxParameterCount = 10;

		private static readonly string SEPARATOR = ", ";

		[SerializeField]
		private List<ParameterPreset> _presets = new List<ParameterPreset>();

		public int Count => _presets.Count;

		public bool IsValid => _presets.All(ValidParameterPreset);

		public void Validate()
		{
			if (_presets.Count > maxParameterCount) {
				int diff = _presets.Count - maxParameterCount;
				_presets.RemoveRange(maxParameterCount - 1, diff);
			}
		}

		public IDictionary<string, ParameterValueType> GetParameterFormat()
		{
			List<ParameterPreset> nonEmpty = new List<ParameterPreset>(_presets);
			nonEmpty.RemoveAll(elt => elt == null);
			return nonEmpty.ToDictionary((p) => p.Name, (p) => p.Type);
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
			return parameterPreset == null || parameterPreset.IsValid;
		}
	}
}
