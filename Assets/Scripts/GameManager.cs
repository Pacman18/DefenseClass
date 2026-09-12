using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance; 

    private int _stageIndex = 1;

    [SerializeField]
    Transform _spawnPoint;

    public static GameManager Instance()
    {
        if (_instance == null)
        {
            GameObject go = new GameObject("GameManager");
            _instance = go.AddComponent<GameManager>();
            DontDestroyOnLoad(go);
        }
        return _instance;
    }

    private SantaSceneManager _sceneManager;
    private AgentManager _agentManager;

    void Start()
    {
        _sceneManager = new SantaSceneManager();
        _agentManager = new AgentManager();

        //StartPageUI startpage = UIManager.Instance.CreateUI<StartPageUI>();
        //startpage.SetButtonEvent(ChangeGameScene);
    }

    public void ChangeGameScene()
    {
        _sceneManager.ChangeGameScene();
    }


    int _id = 0;


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            _id = _agentManager.CreateGolbin(_stageIndex, _spawnPoint);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {   
            _id = _agentManager.CreateGolbin(_stageIndex, _spawnPoint);
        }

        if(Input.GetKeyDown(KeyCode.F3))
        {
            _agentManager.SetState(_id, STATE.WALK);
        }

        if(Input.GetKeyDown(KeyCode.F4))
        {
            Agent agent = _agentManager.GetAgent(_id);
            
            if(agent != null)
            {
                Debug.Log("agent: " + agent.State);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _agentManager.DeleteGolbin(6);
        }

        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            _agentManager.LoadTable();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var ui = UIManager.Instance;
            ui.CreateUI<TestImage>();
            ui.CreateUI<ShopUI>();
            ui.CreateUI<MailUI>();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            var ui = UIManager.Instance;
            ui.RemoveUI<TestImage>();
            ui.RemoveUI<ShopUI>();
            ui.RemoveUI<MailUI>();
        }
    }
}
