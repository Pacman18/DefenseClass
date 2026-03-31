using UnityEditor;
using UnityEngine;

public class OpenWindow : EditorWindow
{
    [MenuItem("Test/OpenWindow", false, 10)]
    static void OpenTestWindow()
    {
        var win = GetWindow<OpenWindow>();
        win.titleContent = new GUIContent("EW 교육", "OpenWindow 예제 창");
        win.minSize = new Vector2(50f, 50f);
        win.maxSize = new Vector2(800f, 600f);

    }

    int _counter = 10;
    string _memo = "메모메모";

    // 스크롤 위치 저장용 
    Vector2 _scroll;

    void OnGUI()
    {
        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        // 스크롤 영역 안에 그릴 위젯들
        for (int i = 0; i < 3; i++)
        {   
            EditorGUILayout.LabelField("스크롤 안 내용" + i);
            EditorGUILayout.Space(6f);
        }

        _memo = EditorGUILayout.TextField("메모", _memo);   
        _counter = EditorGUILayout.IntField("카운터", _counter);

        if(GUILayout.Button("UI 생성"))
        {
            var ui = UIManager.Instance.GetUI<EditorConnectUI>();
            if(ui != null)
            {
                ui.SetUIData(_memo);
            }
        }

        

        // 한쌍임 
        EditorGUILayout.EndScrollView();
    }

    // 같은 메뉴 경로에 반드시 2개가 짝이 됨:
    //   (true)  검증 — return true일 때만 메뉴가 살아 있음(회색 아님)
    //   (false) 실행 — 클릭 시 호출
    [MenuItem("Test/OpenWindow 플레이중에 열기", true)]
    static bool ValidateOpenWhenPlayingWindow()
    {
        // 플레이 중에만 메뉴 활성화 → true = "이 메뉴 쓸 수 있음"
        return EditorApplication.isPlaying;
    }

    [MenuItem("Test/OpenWindow 플레이중에 열기", false)]
    static void OpenWhenPlayingWindow()
    {
        var win = GetWindow<OpenWindow>();
        win.titleContent = new GUIContent("EW 교육 (Play)", "플레이 중에 연 창");
        win.minSize = new Vector2(320f, 200f);
    }
}
