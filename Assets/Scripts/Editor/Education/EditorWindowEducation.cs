using UnityEditor;
using UnityEngine;

/// <summary>
/// EditorWindow: 독립된 에디터 창을 띄울 때 사용하는 클래스입니다.
/// (인스펙터용 CustomEditor와 다름 — 대상 컴포넌트 없이 메뉴 등에서 엽니다.)
/// </summary>
public class EditorWindowEducation : EditorWindow
{
    // ── 윈도우 안에서만 쓰는 임시 상태 (에디터 세션 동안 유지, 재시작 시 초기화)
    string _memo = "메모 입력";
    int _counter;
    Vector2 _scroll;

    // ═══════════════════════════════════════════════════════════════════════════
    // [MenuItem] — 이 메서드가 상단 메뉴에 항목을 만듭니다. (에디터 전용)
    //
    // 인자:
    //   1) 경로: "상위/하위/이름" 슬래시로 구분. 마지막이 클릭 시 실행되는 항목입니다.
    //   2) isValidateFunction: true면 "검증 전용" 함수 — 메뉴 활성/비활성만 결정하고 클릭 시 호출되지 않음.
    //   3) priority: 작을수록 위에 배치(기본 0). 같은 섹션에서 순서 조절용.
    //
    // 단축키(경로 문자열 끝에 공백 뒤 붙임):
    //   % = Ctrl(Windows) / Cmd(Mac)
    //   # = Shift
    //   & = Alt
    //   _ = (구분자) 단축키 앞에 옴
    //   예: "Education/Editor Window %#e" → Ctrl+Shift+E
    //
    // 주의: 프로젝트/에셋 메뉴와 겹치면 단축키가 안 먹을 수 있으니 교육용은 경로만 써도 됩니다.
    // ═══════════════════════════════════════════════════════════════════════════
    [MenuItem("Education/Editor Window 예제", false, 10)]
    static void OpenWindow()
    {
        // GetWindow<T>() — 이미 열린 창이 있으면 포커스만, 없으면 생성.
        // titleContent: 탭 제목
        var win = GetWindow<EditorWindowEducation>();
        win.titleContent = new GUIContent("EW 교육", "EditorWindowEducation 예제 창");
        win.minSize = new Vector2(320f, 200f);
    }

    // 검증용 MenuItem: 플레이 모드일 때만 메뉴가 활성화되는 예시
    [MenuItem("Education/Editor Window (플레이 중에만 열기)", true)]
    static bool ValidateOpenWhenPlaying()
    {
        return EditorApplication.isPlaying;
    }

    [MenuItem("Education/Editor Window (플레이 중에만 열기)", false, 11)]
    static void OpenWhenPlaying()
    {
        GetWindow<EditorWindowEducation>(true, "EW 교육 (Play)");
    }

    void OnEnable()
    {
        // 창이 열리거나 도메인 리로드 후 다시 붙을 때 호출
    }

    void OnGUI()
    {
        // EditorWindow는 OnGUI에서 IMGUI로 그립니다. (CustomEditor의 OnInspectorGUI와 동일 계열)
        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        EditorGUILayout.HelpBox(
            "EditorWindow는 [MenuItem]으로 연 Static 메서드에서 GetWindow로 엽니다.\n" +
            "CustomEditor는 [CustomEditor(typeof(...))]로 특정 컴포넌트 인스펙터만 바꿉니다.",
            MessageType.Info);

        EditorGUILayout.Space(6f);
        EditorGUILayout.LabelField("간단 입력", EditorStyles.boldLabel);
        _memo = EditorGUILayout.TextField("메모", _memo);
        _counter = EditorGUILayout.IntField("카운터", _counter);

        EditorGUILayout.Space(4f);
        if (GUILayout.Button("카운터 +1 (Undo 없음 — 필요 시 Undo.RecordObject 대상 지정)"))
        {
            _counter++;
        }

        if (GUILayout.Button("EditorUtility.DisplayDialog 예제"))
        {
            if (EditorUtility.DisplayDialog("제목", "확인 메시지입니다.", "확인", "취소"))
            {
                Debug.Log("[EditorWindowEducation] 확인 클릭");
            }
        }

        EditorGUILayout.EndScrollView();
    }
}
