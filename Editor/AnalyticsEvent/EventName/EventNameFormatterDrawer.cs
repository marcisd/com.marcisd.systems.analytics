using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using DMED.Editor;
using System.Reflection;

/*===============================================================
Project:	Poop Deck
Developer:	Marci San Diego
Company:	David Morgan Education - marcianosd@dm-ed.com
Date:		17/06/2020 15:01
===============================================================*/

namespace DMED.Systems.AnalyticsSystem.Editor 
{
	[CustomPropertyDrawer(typeof(EventNameFormatter))]
	public class EventNameFormatterDrawer : PropertyDrawer
	{
		SerializedProperty _separatorProp = null;
		SerializedProperty _sectionsProp = null;
		ReorderableList _reorderableList = null;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			LazyInitReoderableList(property, label);

			Rect rListPos = new Rect(position) {
				height = _reorderableList.GetHeight(),
			};

			Rect separatorPos = new Rect(position) {
				y = rListPos.y + rListPos.height + EditorGUIUtility.standardVerticalSpacing,
				height = EditorGUIUtility.singleLineHeight
			};

			Rect fullNamePos = new Rect(position) {
				y = separatorPos.y + separatorPos.height + EditorGUIUtility.standardVerticalSpacing,
				height = EditorGUIUtility.singleLineHeight,
			};

			_reorderableList.DoList(rListPos);

			DragAndDropObjects(rListPos);

			_separatorProp = property.FindPropertyRelative("_separator");
			EditorGUI.PropertyField(separatorPos, _separatorProp, new GUIContent("Separator"));

			object instance = property.GetObjectInstance();
			MethodInfo toStringMethodInfo = fieldInfo.FieldType.GetMethod("ToString");

			EditorGUI.LabelField(fullNamePos, "Full Name", (string)toStringMethodInfo.Invoke(instance, null));
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			LazyInitReoderableList(property, label);

			return _reorderableList.GetHeight() + EditorGUIUtility.standardVerticalSpacing +
				((EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) * 2f);

		}

		private void DragAndDropObjects(Rect rect)
		{
			if (rect.Contains(Event.current.mousePosition)) {
				if (Event.current.type == EventType.DragUpdated) {
					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					Event.current.Use();
				} else if (Event.current.type == EventType.DragPerform) {
					foreach (var objectReference in DragAndDrop.objectReferences) {
						if (objectReference is EventNameSection eventNameSection) {
							Add(eventNameSection);
						}
					}
					Event.current.Use();
				}
			}
		}

		private void Add(EventNameSection eventNameSection)
		{
			_sectionsProp.arraySize += 1;
			_sectionsProp.GetArrayElementAtIndex(_sectionsProp.arraySize - 1).objectReferenceValue = eventNameSection;
		}

		private void LazyInitReoderableList(SerializedProperty property, GUIContent label)
		{
			if (_reorderableList == null) {
				_sectionsProp = property.FindPropertyRelative("_sections");
				_reorderableList = new ReorderableList(property.serializedObject, _sectionsProp) {
					drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, label),
					drawElementCallback = DrawElementCallback,
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
				width = rect.width * 0.7f,
			};
			Rect typeRect = new Rect(objRect) {
				x = objRect.x + objRect.width + EditorGUIUtility.standardVerticalSpacing,
				width = rect.width * 0.3f - EditorGUIUtility.standardVerticalSpacing,
			};

			SerializedProperty elementProp = _sectionsProp.GetArrayElementAtIndex(index);
			using (var changeCheck = new EditorGUI.ChangeCheckScope()) {
				EditorGUI.PropertyField(objRect, elementProp, GUIContent.none);
				if (changeCheck.changed) {
					elementProp.serializedObject.ApplyModifiedProperties();
				}
			}

			if (elementProp != null && elementProp.objectReferenceValue != null) {
				SerializedObject elementObj = new SerializedObject(elementProp.objectReferenceValue);
				SerializedProperty typeProp = elementObj.FindProperty("_type");
				using (new EditorGUILabelWidthScope(50f))
				using (new EditorGUI.DisabledScope(true)) {
					EditorGUI.PropertyField(typeRect, typeProp);
				}
			}
		}
	}
}
