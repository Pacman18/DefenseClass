using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

/// <summary>
/// SliderEditor 확장. Slider의 Min/Max, Whole Numbers 등은 base에서 처리하고
/// 프로젝트 전용 옵션만 아래에 붙이는 형태가 유지보수에 유리합니다.
/// </summary>
[CustomEditor(typeof(CustomSlider))]
public class CustomSliderEditor : SliderEditor
{
    SerializedProperty _showValueLabelProp;
    SerializedProperty _valueFormatProp;

    protected override void OnEnable()
    {
        base.OnEnable();
        _showValueLabelProp = serializedObject.FindProperty("_showValueLabel");
        _valueFormatProp = serializedObject.FindProperty("_valueFormat");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUILayout.Space(4f);
        EditorGUILayout.LabelField("Custom Slider (교육용 추가 필드)", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_showValueLabelProp, new GUIContent("값 라벨 표시"));
        // 문자열 포맷은 빈 문자열이면 런타임에서 기본값 처리하도록 안내
        using (new EditorGUI.DisabledScope(!_showValueLabelProp.boolValue))
        {
            EditorGUILayout.PropertyField(_valueFormatProp, new GUIContent("표시 포맷", "string.Format / ToString에 넘길 포맷 예: F0, F2"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
