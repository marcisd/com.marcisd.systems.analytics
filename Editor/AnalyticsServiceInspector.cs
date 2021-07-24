using UnityEditor;
using UnityEngine;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:		24/07/2021142:12
===============================================================*/

namespace MSD.Systems.Analytics.Editor
{
	[CustomEditor(typeof(AnalyticsService), true)]
	public class AnalyticsServiceInspector : UnityEditor.Editor
	{
		private static GUIStyle s_style;
		private static GUIStyle Style {
			get {
				if (s_style == null) {
					s_style = new GUIStyle(EditorStyles.helpBox) {
						richText = true,
					};
				}
				return s_style;
			}
		}

		private AnalyticsService AnalyticsService => target as AnalyticsService;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			EditorGUILayout.Space();

			string spec = "<b>Event Name Format Specifier</b>\n";
			spec += "\n";
			spec += $"{AnalyticsService.EventNameFormatBlob}\n";
			spec += "\n";
			spec += $"<b>Separator:</b>\t\t\"{AnalyticsService.EventNameFormatSpecifier.Separator}\"\n";
			spec += $"<b>Max Character Count:</b>\t{AnalyticsService.EventNameFormatSpecifier.MaxCharacterCount}";

			EditorGUILayout.LabelField(spec, Style);
		}
	}
}
