using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Obstacle))]
public class MushroomEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        Obstacle handler = (Obstacle) target;
        if (GUILayout.Button("Damage Obstacle")) handler.Damage(1);
    }
}
