using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		17/06/2020 15:01
===============================================================*/

namespace MSD.Systems.Analytics.Editor 
{
	[CustomPropertyDrawer(typeof(EventContext))]
	public class EventContextDrawer : PropertyDrawer
	{
		private SerializedProperty _sectionsProp;
		private ReorderableList _reorderableList;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			LazyInitReoderableList(property, label);

			Rect rListPos = new Rect(position) {
				height = _reorderableList.GetHeight(),
			};

			_reorderableList.DoList(rListPos);

			DragAndDropObjects(rListPos);
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			LazyInitReoderableList(property, label);

			return _reorderableList.GetHeight();
		}

		private void DragAndDropObjects(Rect rect)
		{
			if (rect.Contains(Event.current.mousePosition)) {
				if (Event.current.type == EventType.DragUpdated) {
					DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
					Event.current.Use();
				} else if (Event.current.type == EventType.DragPerform) {
					foreach (var objectReference in DragAndDrop.objectReferences) {
						if (objectReference is EventContextComponent eventContextComponent) {
							Add(eventContextComponent);
						}
					}
					Event.current.Use();
				}
			}
		}

		private void Add(EventContextComponent eventContextComponent)
		{
			_sectionsProp.arraySize += 1;
			_sectionsProp.GetArrayElementAtIndex(_sectionsProp.arraySize - 1).objectReferenceValue = eventContextComponent;
		}

		private void LazyInitReoderableList(SerializedProperty property, GUIContent label)
		{
			if (_reorderableList == null) {
				_sectionsProp = property.FindPropertyRelative("_sections");
				_reorderableList = new ReorderableList(property.serializedObject, _sectionsProp) {
					drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, label),
					// TODO: Convert x to discard when C#9.0 is out
					drawElementCallback = (rect, index, x, _) => DrawElementCallback(rect, index),
					onAddCallback = (_) => OnAddCallback(),
				};
			}
		}

		private void OnAddCallback()
		{
			Add(null);
		}

		private void DrawElementCallback(Rect rect, int index)
		{
			Rect objRect = new Rect(rect) {
				y = rect.y + EditorGUIUtility.standardVerticalSpacing,
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
				EventContextComponentType type = (EventContextComponentType)typeProp.enumValueIndex;
				Color color = type switch {
					EventContextComponentType.Classification => Color.magenta,
					EventContextComponentType.Subject => Color.cyan,
					EventContextComponentType.Verb => Color.green,
					_ => GUI.color,
				};
				using (new GUIColorScope(color))
				using (new EditorGUILabelWidthScope(35f))
				using (new EditorGUI.DisabledScope(true)) {
					EditorGUI.PropertyField(typeRect, typeProp);
				}
			}
		}
	}
}
