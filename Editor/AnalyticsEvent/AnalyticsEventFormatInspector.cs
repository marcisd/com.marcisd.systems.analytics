using UnityEditor;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		29/01/2019 22:15
===============================================================*/

namespace MSD.Systems.Analytics.Editor 
{
	[CustomEditor(typeof(AnalyticsEventFormat))]
	public class AnalyticsEventFormatInspector : UnityEditor.Editor 
	{
		public override void OnInspectorGUI() 
		{
			base.OnInspectorGUI();
			DrawValidationLogs();
		}

		private void DrawValidationLogs()
		{
			SerializedProperty logsProp = serializedObject.FindProperty("_validationLogs");
			if (logsProp != null && logsProp.isArray) {
				for (int i = 0; i < logsProp.arraySize; i++) {
					SerializedProperty elt = logsProp.GetArrayElementAtIndex(i);
					EditorGUILayout.HelpBox(elt.stringValue, MessageType.Error);
				}
			}
		}
	}
}
