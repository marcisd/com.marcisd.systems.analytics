using System;
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
		private AnalyticsService Target => target as AnalyticsService;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			EditorGUILayout.Space();

			string spec = "<b>Event Name Format Specifier</b>" + Environment.NewLine;
			spec += Environment.NewLine;
			spec += $"{Target.EventNameFormatBlob}" + Environment.NewLine;
			spec += Environment.NewLine;
			spec += $"<b>Separator:</b>\t\t\"{Target.EventNameFormatSpecifier.Separator}\"" + Environment.NewLine;
			spec += $"<b>Max Character Count:</b>\t{Target.EventNameFormatSpecifier.MaxCharacterCount}";

			EditorGUILayout.LabelField(spec, Styles.RichHelpBox);
		}
	}
}
