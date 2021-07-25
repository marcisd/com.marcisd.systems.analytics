using System;
using UnityEditor;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		18/06/2020 14:39
===============================================================*/

namespace MSD.Systems.Analytics.Editor
{
	[CustomEditor(typeof(EventContextComponent), true)]
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
			EditorGUILayout.HelpBox("Use UpperCamelCase for Event Context Component names.", MessageType.Info);
			EditorGUILayout.HelpBox("Validation: A-Z, a-z, 0-9 are valid characters for Event Context Component names.", MessageType.Info);

			EditorGUILayout.Space();

			string spec = "<b>Event Context Component Types</b>";
			spec += Environment.NewLine;
			spec += Environment.NewLine;
			spec += $"<b><color=magenta>{nameof(EventContextComponentType.Classification)}</color></b>";
			spec += "\tThe different concepts in your game you want to track. There should be one and only one of these in an Event Context.";
			spec += Environment.NewLine;
			spec += Environment.NewLine;
			spec += $"<b><color=cyan>{nameof(EventContextComponentType.Subject)}</color></b>";
			spec += "\tThe entity at which the action of the verb is directed.";
			spec += Environment.NewLine;
			spec += Environment.NewLine;
			spec += $"<b><color=lime>{nameof(EventContextComponentType.Verb)}</color></b>";
			spec += "\tThe action carried out to the subject. There should be one and only one of these in an Event Context. Use the past tense in naming this component.";

			EditorGUILayout.LabelField(spec, Styles.RichHelpBox);
		}

		public void DrawValidation()
		{
			if (!Target.IsValid) {
				EditorGUILayout.HelpBox("Naming violation!", MessageType.Error);
			}
		}
	}
}
