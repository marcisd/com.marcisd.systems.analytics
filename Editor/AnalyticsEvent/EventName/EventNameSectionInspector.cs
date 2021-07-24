using UnityEditor;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		18/06/2020 14:39
===============================================================*/

namespace MSD.Systems.Analytics.Editor
{
	[CustomEditor(typeof(EventNameSection))]
	[CanEditMultipleObjects]
	public class EventNameSectionInspector : UnityEditor.Editor 
	{
		private EventNameSection EventNameSection => target as EventNameSection;

		public override void OnInspectorGUI() 
		{
			base.OnInspectorGUI();

			DrawInfo();
			DrawValidation();
		}

		public void DrawInfo()
		{
			string eventNameHelp = "Naming:\t\tUse UpperCamelCase for Event names";
			eventNameHelp += "\n\n";
			eventNameHelp += "Validation:\tA-Z, a-z, 0-9 are valid characters for Event names.";

			EditorGUILayout.HelpBox(eventNameHelp, MessageType.Info);
		}

		public void DrawValidation()
		{
			if (!EventNameSection.IsValid) {
				EditorGUILayout.HelpBox("Naming violation!", MessageType.Error);
			}
		}
	}
}
