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
	[Serializable]
	public class ParameterValue
	{
		[SerializeField]
		private ParameterType _type;

		[SerializeField]
		private BoolReference _boolReference;

		[SerializeField]
		private FloatReference _floatReference;

		[SerializeField]
		private IntReference _intReference;

		[SerializeField]
		private StringReference _stringReference;

		public ParameterValue(ParameterType type)
		{
			_type = type;
			ResetReferences();
		}

		public ParameterType Type {
			get { return _type; }
			set {
				if (_type != value) {
					_type = value;
					ResetReferences();
				}
			}
		}

		public object ObjectValue {
			get {
				return _type switch {
					ParameterType.Bool => _boolReference.Value,
					ParameterType.Float => _floatReference.Value,
					ParameterType.Int => _intReference.Value,
					ParameterType.String => _stringReference.Value,
					_ => null,
				};
			}
		}

		public bool BoolValue {
			get {
				return _boolReference.Value;
			}
		}

		public float FloatValue {
			get {
				return _floatReference.Value;
			}
		}

		public int IntValue {
			get {
				return _intReference.Value;
			}
		}

		public string StringValue {
			get {
				return _stringReference.Value;
			}
		}

		private void ResetReferences()
		{
			_boolReference = new BoolReference();
			_floatReference = new FloatReference();
			_intReference = new IntReference();
			_stringReference = new StringReference();
		}

		public override string ToString()
		{
			return _type switch {
				ParameterType.Bool => _boolReference.Value.ToString(),
				ParameterType.Float => _floatReference.Value.ToString(),
				ParameterType.Int => _intReference.Value.ToString(),
				ParameterType.String => _stringReference.Value,
				_ => string.Empty,
			};
		}
	}
}
