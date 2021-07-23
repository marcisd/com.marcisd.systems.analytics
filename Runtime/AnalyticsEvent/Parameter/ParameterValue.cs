using System;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	DefaultCompany - marcianosd@dm-ed.com
Date:		30/01/2019 12:23
===============================================================*/

namespace DMED.Systems.AnalyticsSystem 
{
	[Serializable]
	public class ParameterValue
	{
		[SerializeField] private ParameterType _type;

		[SerializeField] private BoolReference _boolReference;
		[SerializeField] private FloatReference _floatReference;
		[SerializeField] private IntReference _intReference;
		[SerializeField] private StringReference _stringReference;

		public ParameterValue(ParameterType type)
		{
			_type = type;
			ResetReferences();
		}

		public ParameterType type {
			get { return _type; }
			set {
				if (_type != value) {
					_type = value;
					ResetReferences();
				}
			}
		}

		public object objectValue {
			get {
				switch (_type) {
					case ParameterType.Bool:
						return _boolReference.Value as object;
					case ParameterType.Float:
						return _floatReference.Value as object;
					case ParameterType.Int:
						return _intReference.Value as object;
					case ParameterType.String:
						return _stringReference.Value as object;
					default:
						return null;
				}
			}
		}

		public bool boolValue {
			get {
				return _boolReference.Value;
			}
		}

		public float floatValue {
			get {
				return _floatReference.Value;
			}
		}

		public int intValue {
			get {
				return _intReference.Value;
			}
		}

		public string stringValue {
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
			switch (_type) {
				case ParameterType.Bool:
					return _boolReference.Value.ToString();
				case ParameterType.Float:
					return _floatReference.Value.ToString();
				case ParameterType.Int:
					return _intReference.Value.ToString();
				case ParameterType.String:
					return _stringReference.Value;
				default:
					return string.Empty;
			}
		}
	}
}
