using UnityEngine;
using Santa;

public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.Initialize();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SantaSceneManager.Instance.SceneLoad(SCENENAME.GameScene);
        }
        
    }
}
