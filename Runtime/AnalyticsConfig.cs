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
		private static readonly string DEBUG_PREPEND = $"[{nameof(AnalyticsConfig)}]";

		[Header("Runtime")]
		[SerializeField]
		private bool _shouldInitializeOnAppStart = true;

		[SerializeField]
		private List<AnalyticsService> _services = new List<AnalyticsService>();

		internal static bool ShouldInitializeOnAppStart => Instance._shouldInitializeOnAppStart;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void OnAppStart()
		{
			foreach (AnalyticsService service in Instance._services) {
				service.OnAppStart();
			}

			if (Instance._shouldInitializeOnAppStart) {
				Initialize();
			}
		}

		/// <summary>
		/// Called on app start if auto-initialization is enabled.
		/// Initialization can be triggered after app start but cannot perform actual initialization more than once.
		/// </summary>
		public static void Initialize()
		{
			Debugger.Log(DEBUG_PREPEND, "Initializing Analytics Services...");
			foreach (AnalyticsService service in Instance._services) {
				service.Initialize();
			}
		}

#if UNITY_EDITOR

		[Header("Editor")]
		[SerializeField]
		private bool _isEditorEventsAllowed;

		internal static bool IsEditorEventsAllowed => Instance._isEditorEventsAllowed;

		[MenuItem("MSD/Config/Analytics")]
		private static void ShowConfig()
		{
			Selection.objects = new Object[] { Instance };
		}

#endif

	}
}
