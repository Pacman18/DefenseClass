using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

/// <summary>
/// Unity UI Toggle용 기본 인스펙터(ToggleEditor)를 확장하는 패턴.
/// base.OnInspectorGUI()로 원래 Toggle 필드(전환 그래픽, 전환 그룹 등)를 그린 뒤
/// 커스텀 SerializedProperty만 추가로 그립니다.
/// </summary>
[CustomEditor(typeof(CustomToggle))]
public class CustomToggleEditor : ToggleEditor
{
    SerializedProperty _onTintProp;
    SerializedProperty _onStateTooltipProp;

    protected override void OnEnable()
    {
        base.OnEnable();
        _onTintProp = serializedObject.FindProperty("_onTint");
        _onStateTooltipProp = serializedObject.FindProperty("_onStateTooltip");
    }

    public override void OnInspectorGUI()
    {
        // 1) 기본 Toggle 인스펙터 전체(Interactable, Transition, Navigation 등)
        base.OnInspectorGUI();

        // 2) 커스텀 필드: 반드시 serializedObject와 동기화
        serializedObject.Update();
        EditorGUILayout.Space(4f);
        EditorGUILayout.LabelField("Custom Toggle (교육용 추가 필드)", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_onTintProp, new GUIContent("On Tint", "켜졌을 때 참고용 색(런타임 로직은 예제에 없음)"));
        EditorGUILayout.PropertyField(_onStateTooltipProp);
        serializedObject.ApplyModifiedProperties();
    }
}
