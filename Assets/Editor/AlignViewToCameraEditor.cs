using UnityEditor;
using UnityEngine;

namespace GameLibNS.Editor
{

    /// <summary>
    /// Helper class so we can easily align the scene view to the camera during play mode.
    /// </summary>
    public class AlignViewToCameraEditor : EditorWindow
    {

        [InitializeOnLoadMethod]
        public static void Enable()
        {
            Debug.Log("AlignViewToCameraEditor ON");
            SceneView.duringSceneGui += OnScene;
        }

        [MenuItem("Window/Scene AlignViewToCamera/Disable")]
        public static void Disable()
        {
            Debug.Log("AlignViewToCameraEditor OFF");
            SceneView.duringSceneGui -= OnScene;
        }

        private static void OnScene(SceneView sceneView)
        {
            Handles.BeginGUI();
            GUILayout.BeginArea(new Rect((EditorGUIUtility.currentViewWidth / 2) - 100, 0, 200, 70));
            if (GUILayout.Button("Align to Cam", GUILayout.Width(200)))
            {
                Camera camera = Camera.main;
                if (camera != null)
                {
                    sceneView.AlignViewToObject(camera.transform);
                }

            }
            GUILayout.EndArea();
            Handles.EndGUI();
        }
    }
}