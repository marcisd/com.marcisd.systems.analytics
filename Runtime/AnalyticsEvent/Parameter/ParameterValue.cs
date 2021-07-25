using System;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		30/01/2019 12:23
===============================================================*/

namespace MSD.Systems.Analytics 
{
	/// <summary>
	/// Stores serialized values for the parameters.
	/// </summary>
	[Serializable]
	public class ParameterValue
	{
		[SerializeField]
		private ParameterValueType _type;

		[SerializeField]
		private BoolReference _boolReference;

		[SerializeField]
		private FloatReference _floatReference;

		[SerializeField]
		private IntReference _intReference;

		[SerializeField]
		private StringReference _stringReference;

		public ParameterValue(ParameterValueType type)
		{
			_type = type;
			ResetReferences();
		}

		public ParameterValueType Type {
			get => _type;
			set {
				if (_type != value) {
					_type = value;
					ResetReferences();
				}
			}
		}

		public object ObjectValue => _type switch {
			ParameterValueType.Bool => _boolReference.Value,
			ParameterValueType.Float => _floatReference.Value,
			ParameterValueType.Int => _intReference.Value,
			ParameterValueType.String => _stringReference.Value,
			_ => null,
		};

		public bool BoolValue => _boolReference.Value;

		public float FloatValue => _floatReference.Value;

		public int IntValue => _intReference.Value;

		public string StringValue => _stringReference.Value;

		private void ResetReferences()
		{
			_boolReference = new BoolReference();
			_floatReference = new FloatReference();
			_intReference = new IntReference();
			_stringReference = new StringReference();
		}

		public override string ToString() => _type switch {
			ParameterValueType.Bool => _boolReference.Value.ToString(),
			ParameterValueType.Float => _floatReference.Value.ToString(),
			ParameterValueType.Int => _intReference.Value.ToString(),
			ParameterValueType.String => _stringReference.Value,
			_ => string.Empty,
		};
	}
}
