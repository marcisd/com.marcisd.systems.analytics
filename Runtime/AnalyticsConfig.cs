using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

/*===============================================================
Project:	Analytics
Developer:	Marci San Diego
Company:	Personal - marcisandiego@gmail.com
Date:       16/03/2020 16:30
===============================================================*/

namespace MSD.Systems.Analytics
{
	public class AnalyticsConfig : ScriptableConfig<AnalyticsConfig>
	{
		[Header("Runtime")]
		[SerializeField]
		private bool _shouldInitializeOnAppStart = true;

		[SerializeField]
		private List<AnalyticsService> _services = new List<AnalyticsService>();

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Initialize()
		{
			if (Instance._shouldInitializeOnAppStart) {
				Debugger.Log("Initializing Analytics Services...");
				foreach (AnalyticsService service in Instance._services) {
					service.Initialize();
				}
			}
		}

#if UNITY_EDITOR
		[Header("Editor")]
		[SerializeField]
		private bool _isEditorEventsAllowed;

		public static bool IsEditorEventsAllowed => Instance._isEditorEventsAllowed;

		[MenuItem("MSD/Config/Analytics")]
		private static void ShowConfig()
		{
			Selection.objects = new Object[] { Instance };
		}
#endif

	}
}
