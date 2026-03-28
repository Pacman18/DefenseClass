using UnityEngine;
using UnityEngine.SceneManagement;

public class Starter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var manager = GameManager.Instance();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("æ¿¿¸»Ø : ");
            
        }
    }


}
