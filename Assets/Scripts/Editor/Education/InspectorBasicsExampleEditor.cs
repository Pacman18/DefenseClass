using UnityEditor;
using UnityEngine;

/// <summary>
/// MonoBehaviour 전용 커스텀 에디터 예제 1: SerializedProperty와 IMGUI 기본 위젯.
/// - Editor: 대상 타입 하나당 하나의 에디터 클래스.
/// - SerializedObject: 인스펙터에서 Undo/다중 선택/프리팹 오버라이드와 동기화되는 래퍼.
/// - Update → 필드 그리기 → ApplyModifiedProperties 순서를 지키면 값 변경이 에셋에 안전하게 반영됩니다.
/// </summary>
[CustomEditor(typeof(InspectorBasicsExample))]
[CanEditMultipleObjects]
public class InspectorBasicsExampleEditor : Editor
{
    SerializedProperty _descriptionProp;
    SerializedProperty _repeatCountProp;
    SerializedProperty _curveProp;

    void OnEnable()
    {
        // 문자열로 찾을 때는 [SerializeField] 필드명과 정확히 일치해야 합니다.
        _descriptionProp = serializedObject.FindProperty("_description");
        _repeatCountProp = serializedObject.FindProperty("_repeatCount");
        _curveProp = serializedObject.FindProperty("_curve");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // HelpBox: 주의사항·단축 설명을 인스펙터에 고정 표시할 때 유용합니다.
        EditorGUILayout.HelpBox(
            "PropertyField는 [SerializeField]와 인스펙터 속성([Range] 등)을 존중합니다. " +
            "커스텀 라벨이 필요하면 GUIContent 두 번째 인자로 툴팁을 줄 수 있습니다.",
            MessageType.Info);

        EditorGUILayout.Space(4f);

        // 기본 필드 그리기: 가장 단순하고 Undo와 호환되는 방법
        EditorGUILayout.PropertyField(_descriptionProp, new GUIContent("설명", "짧은 메모"));
        EditorGUILayout.PropertyField(_repeatCountProp);

        EditorGUILayout.Space(6f);
        EditorGUILayout.LabelField("커브 (AnimationCurve)", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_curveProp, GUIContent.none);

        // 버튼 예시: 에디터 전용 동작. 다중 선택 시 targets 전체에 Undo가 걸리도록 RecordObjects 사용
        if (GUILayout.Button("RepeatCount를 0으로 리셋 (Undo 지원)"))
        {
            Undo.RecordObjects(targets, "Reset Repeat Count");
            serializedObject.Update();
            _repeatCountProp.intValue = 0;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
