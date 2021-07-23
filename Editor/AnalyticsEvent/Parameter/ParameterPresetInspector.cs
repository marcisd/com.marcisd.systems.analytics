using UnityEditor;

/*===============================================================
Project:	Poop Deck
Developer:	Marci San Diego
Company:	David Morgan Education - marcianosd@dm-ed.com
Date:		18/06/2020 14:39
===============================================================*/

namespace DMED.Systems.AnalyticsSystem.Editor
{
	[CustomEditor(typeof(ParameterPreset))]
	[CanEditMultipleObjects]
	public class ParameterPresetInspector : UnityEditor.Editor 
	{
		private new ParameterPreset target => base.target as ParameterPreset;

		public override void OnInspectorGUI() 
		{
			base.OnInspectorGUI();

			DrawInfo();
			DrawValidation();
		}

		public void DrawInfo()
		{
			string parameterNameHelp = "Naming:\t\tUse lower_snake_case for Parameter names";
			parameterNameHelp += "\n\n";
			parameterNameHelp += "Validation:\ta-z, 0-9, Underscore (_) are valid characters for Parameter names.";

			EditorGUILayout.HelpBox(parameterNameHelp, MessageType.Info);
		}

		public void DrawValidation()
		{
			if (!target.isValid) {
				EditorGUILayout.HelpBox("Naming violation!", MessageType.Error);
			}
		}
	}
}
