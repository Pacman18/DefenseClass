using UnityEditor;
using UnityEngine;

/// <summary>
/// MonoBehaviour 전용 커스텀 에디터 예제 2: Foldout·가로 배치·조건부 표시.
/// - EditorGUILayout.BeginHorizontal/Vertical: 한 줄에 여러 컨트롤을 배치할 때 사용합니다.
/// - BeginFoldoutHeaderGroup: 섹션을 접었다 펼 수 있게 할 때 사용합니다(에디터 버전에 따라 API 이름이 다를 수 있음).
///   Unity 2020.1+ 에서는 EditorGUILayout.BeginFoldoutHeaderGroup 권장.
/// </summary>
[CustomEditor(typeof(InspectorLayoutExample))]
public class InspectorLayoutExampleEditor : Editor
{
    SerializedProperty _useAdvancedProp;
    SerializedProperty _minValueProp;
    SerializedProperty _maxValueProp;
    SerializedProperty _layerMaskProp;

    bool _advancedFoldout = true;

    void OnEnable()
    {
        _useAdvancedProp = serializedObject.FindProperty("_useAdvanced");
        _minValueProp = serializedObject.FindProperty("_minValue");
        _maxValueProp = serializedObject.FindProperty("_maxValue");
        _layerMaskProp = serializedObject.FindProperty("_layerMask");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.HelpBox(
            "조건부 UI: _useAdvanced가 true일 때만 아래 Min/Max를 보여줍니다. " +
            "런타임 검증과 별개로 인스펙터 가독성을 높이는 용도입니다.",
            MessageType.None);

        EditorGUILayout.PropertyField(_useAdvancedProp, new GUIContent("고급 옵션 사용"));

        // 고급 옵션이 켜졌을 때만 Min/Max 노출
        if (_useAdvancedProp.boolValue)
        {
            EditorGUILayout.Space(2f);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("범위 (Min / Max)");
            EditorGUILayout.PropertyField(_minValueProp, GUIContent.none);
            EditorGUILayout.PropertyField(_maxValueProp, GUIContent.none);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(8f);

        // 접이식 헤더: 긴 인스펙터를 섹션으로 나눌 때
        _advancedFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(_advancedFoldout, "레이어 마스크 섹션");
        if (_advancedFoldout)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(_layerMaskProp);
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        serializedObject.ApplyModifiedProperties();
    }
}
