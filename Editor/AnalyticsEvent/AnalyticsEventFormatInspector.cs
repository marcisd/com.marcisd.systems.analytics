using UnityEditor;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	DefaultCompany - marcianosd@dm-ed.com
Date:		29/01/2019 22:15
===============================================================*/

namespace DMED.Systems.AnalyticsSystem.Editor 
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
			var logsProp = serializedObject.FindProperty("_validationLogs");
			if (logsProp != null && logsProp.isArray) {
				for (int i = 0; i < logsProp.arraySize; i++) {
					var elt = logsProp.GetArrayElementAtIndex(i);
					EditorGUILayout.HelpBox(elt.stringValue, MessageType.Error);
				}
			}
		}
	}
}
