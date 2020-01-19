using UnityEditor;
using UnityEngine;


namespace Util
{
    [CreateAssetMenu(menuName = "Variables/Int")]
    public class IntVariable : Variable<int>
    {

    }

    [CustomEditor(typeof(IntVariable))]
    public class IntVariableEditor : VariableEditor<int>
    {

    }
}