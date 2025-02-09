#if !ODIN_INSPECTOR

using UnityEditor;

namespace Zenject
{
	[CustomEditor(typeof(ProjectContext))]
	[NoReflectionBaking]
	public class ProjectContextEditor : ContextEditor
	{
		SerializedProperty _buildsReflectionBakingCoverageModeProperty;
		SerializedProperty _editorReflectionBakingCoverageModeProperty;
		SerializedProperty _parentNewObjectsUnderContextProperty;
		SerializedProperty _settingsProperty;

		public override void OnEnable()
		{
			base.OnEnable();

			_settingsProperty = serializedObject.FindProperty("_settings");
			_editorReflectionBakingCoverageModeProperty = serializedObject.FindProperty("_editorReflectionBakingCoverageMode");
			_buildsReflectionBakingCoverageModeProperty = serializedObject.FindProperty("_buildsReflectionBakingCoverageMode");
			_parentNewObjectsUnderContextProperty = serializedObject.FindProperty("_parentNewObjectsUnderContext");
		}

		protected override void OnGui()
		{
			base.OnGui();

			EditorGUILayout.PropertyField(_settingsProperty, true);
			EditorGUILayout.PropertyField(_editorReflectionBakingCoverageModeProperty, true);
			EditorGUILayout.PropertyField(_buildsReflectionBakingCoverageModeProperty, true);
			EditorGUILayout.PropertyField(_parentNewObjectsUnderContextProperty);
		}
	}
}

#endif