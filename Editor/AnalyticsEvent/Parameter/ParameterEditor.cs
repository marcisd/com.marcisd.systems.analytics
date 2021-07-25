using UnityEngine;
using UnityEditor;
using System;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		30/01/2019 14:44
===============================================================*/

namespace MSD.Systems.Analytics.Editor 
{
	public class ParameterEditor 
	{
		private readonly SerializedProperty _keyProp;
		private readonly SerializedProperty _valueProp;

		private readonly SerializedProperty _typeProp;

		public ParameterEditor(SerializedProperty keyProp, SerializedProperty valueProp)
		{
			_keyProp = keyProp;
			_valueProp = valueProp;

			_typeProp = _valueProp.FindPropertyRelative("_type");
		}

		public void Draw(Rect position)
		{
			Rect valueRect = new Rect(position) {
				height = EditorGUIUtility.singleLineHeight,
				y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
			};
			Rect nameRect = new Rect(position) {
				height = EditorGUIUtility.singleLineHeight,
				width = position.width * 0.5f,
			};
			Rect typeRect = new Rect(nameRect) {
				x = nameRect.x + nameRect.width + EditorGUIUtility.standardVerticalSpacing,
				width = nameRect.width - EditorGUIUtility.standardVerticalSpacing,
			};

			using (new EditorGUILabelWidthScope(50f)) {
				using (new EditorGUI.DisabledScope(true)) {
					EditorGUI.PropertyField(nameRect, _keyProp, new GUIContent("Name"));
					EditorGUI.PropertyField(typeRect, _typeProp, new GUIContent("Type"));
				}

				GUIContent valueLabel = new GUIContent("Value");

				switch ((ParameterValueType)_typeProp.enumValueIndex) {
					case ParameterValueType.Bool:
						EditorGUI.PropertyField(valueRect, _valueProp.FindPropertyRelative("_boolReference"), valueLabel);
						break;
					case ParameterValueType.Float:
						EditorGUI.PropertyField(valueRect, _valueProp.FindPropertyRelative("_floatReference"), valueLabel);
						break;
					case ParameterValueType.Int:
						EditorGUI.PropertyField(valueRect, _valueProp.FindPropertyRelative("_intReference"), valueLabel);
						break;
					case ParameterValueType.String:
						EditorGUI.PropertyField(valueRect, _valueProp.FindPropertyRelative("_stringReference"), valueLabel);
						break;
					default:
						throw new NotSupportedException();
				}
			}
		}

		public static float StandardHeight => EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing * 2;
	}
}
