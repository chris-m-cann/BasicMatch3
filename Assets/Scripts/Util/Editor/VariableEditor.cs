using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Util
{
    public abstract class VariableEditor<T> : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            using (new EditorGUI.DisabledGroupScope(serializedObject.isEditingMultipleObjects))
            {
                if (GUILayout.Button("Capture Runtime Value"))
                {
                    ((Variable<T>)target).CaptureRuntimeValues();
                }
            }
        }
    }
}