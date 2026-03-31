using UnityEditor;
using UnityEditor.UI;

/// <summary>
/// Image는 MaskableGraphic 계열이며 기본 에디터는 ImageEditor입니다.
/// Sprite, Image Type, Raycast Target 등은 base에 맡기고 커스텀 필드만 추가합니다.
/// </summary>
[CustomEditor(typeof(CustomImage))]
public class CustomImageEditor : ImageEditor
{
    SerializedProperty _pulseColorProp;
    SerializedProperty _pulseSpeedProp;

    protected override void OnEnable()
    {
        base.OnEnable();
        _pulseColorProp = serializedObject.FindProperty("_pulseColor");
        _pulseSpeedProp = serializedObject.FindProperty("_pulseSpeed");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUILayout.Space(4f);
        EditorGUILayout.LabelField("Custom Image (교육용 추가 필드)", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_pulseColorProp);
        EditorGUILayout.PropertyField(_pulseSpeedProp);
        serializedObject.ApplyModifiedProperties();
    }
}
