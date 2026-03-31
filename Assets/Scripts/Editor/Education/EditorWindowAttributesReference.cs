using UnityEditor;
using UnityEngine;

/// <summary>
/// 에디터 스크립트에서 자주 쓰는 어트리뷰트만 한 파일에 모아 둔 참고용입니다.
/// (이 클래스는 창을 띄우지 않으며, 컴파일·주석 확인용입니다.)
/// </summary>
static class EditorWindowAttributesReference
{
    // ── [MenuItem] ─────────────────────────────────────────────────────────────
    // 파일: 아무 Editor 폴더 스크립트의 static 메서드에 붙입니다.
    // 시그니처: static void Name() 또는 검증용 static bool Name()
    //
    // [MenuItem("경로", isValidateFunction, priority)]
    //   isValidateFunction == false → 클릭 시 실행
    //   isValidateFunction == true  → 메뉴 항목의 회색 처리 여부만 판단(return false면 비활성)

    // ── [InitializeOnLoad] ────────────────────────────────────────────────────
    // static 생성자와 함께 쓰면 에디터가 로드될 때 한 번 실행됩니다.
    // 에디터 전역 훅(EditorApplication.update 등) 등록 시 사용. 남용 시 에디터 느려질 수 있음.

    // ── [CustomEditor(typeof(T))] ───────────────────────────────────────────────
    // class X : Editor — 특정 컴포넌트의 인스펙터만 교체할 때.

    // ── [CanEditMultipleObjects] ────────────────────────────────────────────────
    // CustomEditor 클래스에 붙이면 인스펙터에서 여러 오브젝트 동시 선택 시에도 동작.

    // ── [InitializeOnLoadMethod] ────────────────────────────────────────────────
    // static void Method() — 에디터 로드 시 한 번 호출 (Unity 2017.1+).

    // ── EditorWindow 관련 (어트리뷰트는 MenuItem으로 여는 것이 일반적) ───────────
    // EditorWindow 자체에는 필수 어트리뷰트가 없습니다. MenuItem + GetWindow 조합으로 연다.

    [MenuItem("Education/어트리뷰트 참고 (콘솔 로그)", false, 200)]
    static void LogAttributeSummary()
    {
        Debug.Log(
            "[MenuItem] 메뉴 항목\n" +
            "[CustomEditor(typeof(T))] 인스펙터 교체\n" +
            "[CanEditMultipleObjects] 다중 선택 인스펙터\n" +
            "[InitializeOnLoad] 에디터 로드 시 정적 초기화\n" +
            "EditorWindow: MenuItem에서 GetWindow<YourWindow>()");
    }
}
