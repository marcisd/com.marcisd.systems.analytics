using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using DMED.Editor;

/*===============================================================
Project:	Poop Deck
Developer:	Marci San Diego
Company:	David Morgan Education - marcianosd@dm-ed.com
Date:		17/06/2020 21:55
===============================================================*/

namespace DMED.Systems.AnalyticsSystem.Editor
{
	[CustomPropertyDrawer(typeof(ParameterFormatter))]
	public class ParameterFormatterDrawer : PropertyDrawer
	{
		SerializedProperty _presetsProp = null;
		ReorderableList _reorderableList = null;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			LazyInitReoderableList(property, label);

			Rect rListPos = new Rect(position) {
				height = _reorderableList.GetHeight(),
			};

			Rect countPos = new Rect(position) {
				y = rListPos.y + rListPos.height,
				height = EditorGUIUtility.singleLineHeight,
			};


			_reorderableList.DoList(rListPos);

			DragAndDropObjects(rListPos);

			EditorGUI.LabelField(countPos, "Parameter Count", $"{_presetsProp.arraySize}/{ParameterFormatter.maxParameterCount}");
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			LazyInitReoderableList(property, label);

			return _reorderableList.GetHeight() + EditorGUIUtility.standardVerticalSpacing + EditorGUIUtility.singleLineHeight;
		}

		private void DragAndDropObjects(Rect rect)
		{
			if (rect.Contains(Event.current.mousePosition)) {
				if (Event.current.type == EventType.DragUpdated) {
					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					Event.current.Use();
				} else if (Event.current.type == EventType.DragPerform) {
					foreach (var objectReference in DragAndDrop.objectReferences) {
						if (objectReference is ParameterPreset parameterPreset) {
							Add(parameterPreset);
						}
					}
					Event.current.Use();
				}
			}
		}

		private void Add(ParameterPreset parameterPreset)
		{
			_presetsProp.arraySize += 1;
			_presetsProp.GetArrayElementAtIndex(_presetsProp.arraySize - 1).objectReferenceValue = parameterPreset;
		}

		private void LazyInitReoderableList(SerializedProperty property, GUIContent label)
		{
			if (_reorderableList == null) {
				_presetsProp = property.FindPropertyRelative("_presets");
				_reorderableList = new ReorderableList(property.serializedObject, _presetsProp) {
					drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, label),
					drawElementCallback = DrawElementCallback,
					elementHeight = EditorGUIUtility.singleLineHeight * 2 + EditorGUIUtility.standardVerticalSpacing * 2,
					onAddCallback = OnAddCallback,
				};
			}
		}

		private void OnAddCallback(ReorderableList reorderableList)
		{
			Add(null);
		}

		private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
		{
			Rect objRect = new Rect(rect) {
				height = EditorGUIUtility.singleLineHeight,
			};
			Rect nameRect = new Rect(objRect) {
				y = objRect.y + objRect.height + EditorGUIUtility.standardVerticalSpacing,
				width = objRect.width * 0.5f,
			};
			Rect typeRect = new Rect(nameRect) {
				x = nameRect.x + nameRect.width + EditorGUIUtility.standardVerticalSpacing,
				width = nameRect.width - EditorGUIUtility.standardVerticalSpacing,
			};

			SerializedProperty elementProp = _presetsProp.GetArrayElementAtIndex(index);
			using (var changeCheck = new EditorGUI.ChangeCheckScope()) {
				EditorGUI.PropertyField(objRect, elementProp, GUIContent.none);
				if (changeCheck.changed) {
					elementProp.serializedObject.ApplyModifiedProperties();
				}
			}

			if (elementProp != null && elementProp.objectReferenceValue != null) {
				SerializedObject elementObj = new SerializedObject(elementProp.objectReferenceValue);
				SerializedProperty nameProp = elementObj.FindProperty("_name");
				SerializedProperty typeProp = elementObj.FindProperty("_type");
				using (new EditorGUILabelWidthScope(50f))
				using (new EditorGUI.DisabledScope(true)) {
					EditorGUI.PropertyField(nameRect, nameProp);
					EditorGUI.PropertyField(typeRect, typeProp);
				}
			}
		}
	}

}