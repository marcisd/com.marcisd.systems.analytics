using UnityEditor;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		18/06/2020 14:39
===============================================================*/

namespace MSD.Systems.Analytics.Editor
{
	[CustomEditor(typeof(ParameterPreset))]
	[CanEditMultipleObjects]
	public class ParameterPresetInspector : UnityEditor.Editor 
	{
		private ParameterPreset Target => target as ParameterPreset;

		public override void OnInspectorGUI() 
		{
			base.OnInspectorGUI();

			DrawInfo();
			DrawValidation();
		}

		public void DrawInfo()
		{
			EditorGUILayout.HelpBox("Use lower_snake_case for Parameter names.", MessageType.Info);
			EditorGUILayout.HelpBox("Validation: a-z, 0-9, Underscore (_) are valid characters for Parameter names.", MessageType.Info);
		}

		public void DrawValidation()
		{
			if (!Target.IsValid) {
				EditorGUILayout.HelpBox("Naming violation!", MessageType.Error);
			}
		}
	}
}
