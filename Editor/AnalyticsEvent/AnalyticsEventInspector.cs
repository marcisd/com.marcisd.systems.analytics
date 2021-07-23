using UnityEngine;
using UnityEditor;

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	DefaultCompany - marcianosd@dm-ed.com
Date:		29/01/2019 20:38
===============================================================*/

namespace DMED.Systems.AnalyticsSystem.Editor 
{
	[CustomEditor(typeof(AnalyticsEvent))]
	public class AnalyticsEventInspector : UnityEditor.Editor 
	{
		AnalyticsEvent analyticsEvent => target as AnalyticsEvent;

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			DrawApplyButton();
			DrawHealthCheck();

			DrawDebugHeader();
			DrawDebugButtons();
		}

		private void DrawApplyButton() 
		{
			if (GUILayout.Button("Apply Format")) {
				analyticsEvent.Apply();
				EditorUtility.SetDirty(target);
				Repaint();
			}
		}

		private void DrawHealthCheck() 
		{
			if (!analyticsEvent.isFormatUpToDate) {
				EditorGUILayout.HelpBox("Format is out of date. Re-apply the format and input the parameter values again.", MessageType.Warning);
			}
		}

		private void DrawDebugHeader()
		{
			EditorGUILayout.LabelField(string.Empty, GUI.skin.horizontalSlider);
			EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);
		}

		private void DrawDebugButtons()
		{
			bool isDisabled = true;
			if(EditorApplication.isPlaying) {
				isDisabled = !analyticsEvent.isServiceInitialized;
			}

			using(new EditorGUI.DisabledGroupScope(isDisabled)) {
				if(GUILayout.Button("Log Analytics Event")) { analyticsEvent.LogAnalyticsEvent(); }
			}
		}
	}
}
