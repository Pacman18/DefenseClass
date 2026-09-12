using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //var manager = GameManager.Instance();

        UIManager.Instance.CreateUI<EditorConnectUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            
        }
    }


}
