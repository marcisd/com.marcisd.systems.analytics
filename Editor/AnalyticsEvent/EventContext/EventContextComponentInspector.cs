using UnityEditor;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		18/06/2020 14:39
===============================================================*/

namespace MSD.Systems.Analytics.Editor
{
	[CustomEditor(typeof(EventContextComponent))]
	[CanEditMultipleObjects]
	public class EventContextComponentInspector : UnityEditor.Editor 
	{
		private EventContextComponent Target => target as EventContextComponent;

		public override void OnInspectorGUI() 
		{
			base.OnInspectorGUI();

			DrawInfo();
			DrawValidation();
		}

		public void DrawInfo()
		{
			EditorGUILayout.HelpBox("Use UpperCamelCase for Event names.", MessageType.Info);
			EditorGUILayout.HelpBox("Validation: A-Z, a-z, 0-9 are valid characters for Event names.", MessageType.Info);
		}

		public void DrawValidation()
		{
			if (!Target.IsValid) {
				EditorGUILayout.HelpBox("Naming violation!", MessageType.Error);
			}
		}
	}
}
