using UnityEditor;
using UnityEngine;

namespace Util
{
    public abstract class Variable<T> : ScriptableObject, Resetable
    {

        [SerializeField]
        private T initialValue;


        public T Value;

        [SerializeField]
        private bool resetOnSceneLoad = true;

        private void OnEnable()
        {
            if (resetOnSceneLoad)
                FindObjectOfType<Resetter>()?.Register(this);
        }

        public void Reset()
        {
            Value = initialValue;
        }

        public void CaptureRuntimeValues()
        {
            initialValue = Value;
        }
    }

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