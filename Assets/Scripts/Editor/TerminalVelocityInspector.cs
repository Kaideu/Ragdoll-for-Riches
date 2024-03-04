using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerminalVelocity)), CanEditMultipleObjects]
public class TerminalVelocityInspector : Editor
{
    private SerializedProperty m_physM;

    private void OnEnable()
    {
        m_physM = serializedObject.FindProperty("_physM");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(m_physM);
        serializedObject.ApplyModifiedProperties();
    }
}
