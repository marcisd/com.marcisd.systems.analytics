using UnityEngine;
using UnityEditor;
using MSD.Editor;

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
		private SerializedProperty _keyProp;
		private SerializedProperty _valueProp;

		private SerializedProperty _typeProp;

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

				switch ((ParameterType)_typeProp.intValue) {
					case ParameterType.Bool:
						EditorGUI.PropertyField(valueRect, _valueProp.FindPropertyRelative("_boolReference"), valueLabel);
						break;
					case ParameterType.Float:
						EditorGUI.PropertyField(valueRect, _valueProp.FindPropertyRelative("_floatReference"), valueLabel);
						break;
					case ParameterType.Int:
						EditorGUI.PropertyField(valueRect, _valueProp.FindPropertyRelative("_intReference"), valueLabel);
						break;
					case ParameterType.String:
						EditorGUI.PropertyField(valueRect, _valueProp.FindPropertyRelative("_stringReference"), valueLabel);
						break;
				}
			}
		}

		public static float standardHeight
		{
			get {
				return EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing * 2;
			}
		}
	}
}
