using UnityEngine;
using System.Collections.Generic;

public class AgentManager
{

    public enum Team
    {
        ATeam,
        BTeam
    }


    private List<Agent> ATeamAgents = new List<Agent>();
    private List<Agent> BTeamAgents = new List<Agent>();

    private const string AGENT_PREFAB_PATH = "Prefab/Game/Goblin";

    public void CreateAgent(Vector3 position, Team team = Team.BTeam )
    {

        GameObject agentPrefab = Resources.Load<GameObject>(AGENT_PREFAB_PATH);

        if (agentPrefab == null)
        {
            Debug.LogError("Agent prefab not found");
            return;
        }

        GameObject agentObject = GameObject.Instantiate(agentPrefab, position, Quaternion.identity);
        Agent agent = agentObject.GetComponent<Agent>();
        agent.Team = team;

        Debug.Log("Agent created: " + agent.Team);       

        if (team == Team.ATeam)
        {
            ATeamAgents.Add(agent);
        }
        else
        {
            BTeamAgents.Add(agent);
        }
    }

    
    
}
