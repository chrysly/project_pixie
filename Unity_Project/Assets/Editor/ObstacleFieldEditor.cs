using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleField))]
public class ObstacleFieldEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        ObstacleField handler = (ObstacleField) target;
        if (GUILayout.Button("Regenerate Obstacles")) handler.RegenerateMap();
    }
}
