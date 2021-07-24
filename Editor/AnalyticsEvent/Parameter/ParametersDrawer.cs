using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		30/01/2019 14:26
===============================================================*/

namespace MSD.Systems.Analytics.Editor 
{
	[CustomPropertyDrawer(typeof(Parameters))]
	public class ParametersDrawer : PropertyDrawer
	{
		private SerializedObject _serializedObject;
		private SerializedProperty _keysProperty;
		private SerializedProperty _valuesProperty;
		private ReorderableList _reorderableList;

		private void Init(SerializedProperty property)
		{
			InitRelativeProperties(property);
			InitReorderableList();
		}

		private void InitRelativeProperties(SerializedProperty property)
		{
			_serializedObject = property.serializedObject;
			_keysProperty = property.FindPropertyRelative("_keys");
			_valuesProperty = property.FindPropertyRelative("_values");
		}

		private void InitReorderableList()
		{
			if (_reorderableList == null) {
				_reorderableList = new ReorderableList(_serializedObject, _keysProperty, false, true, false, false) {
					drawHeaderCallback = (rect) => EditorGUI.PrefixLabel(rect, new GUIContent("Parameters")),
					// TODO: Convert x to discard when C#9.0 is out
					drawElementCallback = (rect, index, x, _) => DrawElementCallback(rect, index),
					elementHeightCallback = (_) => ParameterEditor.StandardHeight,
				};
			}
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			Init(property);
			_reorderableList.DoList(position);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			Init(property);
			return _reorderableList.GetHeight();
		}

		private void DrawElementCallback(Rect rect, int index)
		{
			rect.y += EditorGUIUtility.standardVerticalSpacing;
			ParameterEditor editor = new ParameterEditor(_keysProperty.GetArrayElementAtIndex(index), _valuesProperty.GetArrayElementAtIndex(index));
			editor.Draw(rect);
		}
	}
}
