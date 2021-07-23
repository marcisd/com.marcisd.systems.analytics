using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*===============================================================
Project:    Poop Deck
Developer:  Admor Aguilar
Company:    David Morgan Education - admora@dm-ed.com
Date:       16/03/2020 16:30
===============================================================*/

namespace DMED.Systems.AnalyticsSystem
{
	public class AnalyticsConfig : ScriptableConfigBase<AnalyticsConfig>
	{
		[Header("Runtime")]
		[SerializeField]
		private bool _shouldInitializeOnAppStart = true;

		[SerializeField]
		private List<AnalyticsServiceBase> _services = new List<AnalyticsServiceBase>();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			if (Instance._shouldInitializeOnAppStart) {
				Debugger.Log("Initializing Analytics Services...");
				foreach (AnalyticsServiceBase service in Instance._services) {
					service.Initialize();
				}
			}
		}

#if UNITY_EDITOR
		[Header("Editor")]
		[SerializeField]
		private bool _isEditorEventsAllowed = false;

		public static bool isEditorEventsAllowed => Instance._isEditorEventsAllowed;

		[MenuItem("DMED/Config/Analytics Configuration")]
		private static void ShowAnalyticsConfig()
		{
			Selection.objects = new Object[] { Instance };
		}
#endif

	}
}
