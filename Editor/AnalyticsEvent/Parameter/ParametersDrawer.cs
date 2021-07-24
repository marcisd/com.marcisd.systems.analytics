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
			if (_reorderableList != null)
				return;

			_reorderableList = new ReorderableList(_serializedObject, _keysProperty, false, true, false, false) {
				drawHeaderCallback = DrawHeaderCallback,
				drawElementCallback = DrawElementCallback,
				elementHeightCallback = ElementHeightCallback
			};
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

		#region ReorderableList

		private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
		{
			var editor = new ParameterEditor(_keysProperty.GetArrayElementAtIndex(index), _valuesProperty.GetArrayElementAtIndex(index));
			editor.Draw(rect);
		}

		private float ElementHeightCallback(int index)
		{
			return ParameterEditor.standardHeight;
		}

		private void DrawHeaderCallback(Rect rect)
		{
			EditorGUI.PrefixLabel(rect, new GUIContent("Parameters"));
		}

		#endregion ReorderableList
	}
}
