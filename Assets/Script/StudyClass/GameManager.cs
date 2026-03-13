using UnityEngine;



public class GameManager : SingleTone<GameManager>
{
    private AgentManager agentManager;

    public void Initialize()
    {
        Debug.Log("GameManager Initialize");
    }


    public void StartGame()
    {
        Debug.Log(" 게임이 시작됩니다. ");

        agentManager = new AgentManager();
        agentManager.CreateAgent(new Vector3(0, 1, 0), AgentManager.Team.ATeam);        
    }
    

    public void EndGame()
    {
        Debug.Log(" 게임이 종료됩니다. ");
    }
    
}
