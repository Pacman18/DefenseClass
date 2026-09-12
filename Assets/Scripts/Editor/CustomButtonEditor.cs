using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(CustomButton))]
public class CustomButtonEditor : ButtonEditor
{
    SerializedProperty _testColorProp;

    protected override void OnEnable()
    {
        base.OnEnable();
        _testColorProp = serializedObject.FindProperty("_testColor");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(_testColorProp);
        serializedObject.ApplyModifiedProperties();
    }
}
