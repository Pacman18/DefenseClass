

// 게임 씬에서 동작하는 오브젝트들을 관리할 클래스 
using System.Diagnostics;
using UnityEngine;
using System.Collections.Generic;

public class AgentManager
{
    private int _idNumuser = 0;

    private string _goblinPath = "Prefab/3D/Goblin_0"; 

    private List<Agent> _goblinAgents = new List<Agent>();

    private TableLoader _tableLoader = new TableLoader();

    public void LoadTable()
    {
        _tableLoader.LoadTable();
    }

    public int CreateGolbin(Vector3 position) // 열번만들고 
    {
        _idNumuser++;       
        // Todo :: 진짜 고블린 만드는 코드를 여기 넣을꺼임
        Agent res = Resources.Load<Agent>(_goblinPath); // 애는 나중에 다른 매니저로 壺煞渼?

        UnityEngine.Debug.Log("go: " + res);

        // Unity Scene에 진짜 고블린등장 
        Agent realGolbin = UnityEngine.GameObject.Instantiate(res, position, Quaternion.identity);
        realGolbin.UID = _idNumuser;
        _goblinAgents.Add(realGolbin);

        // 확인용 이름바꾸기 
        realGolbin.gameObject.name = realGolbin.gameObject.name + "_" + _idNumuser;
        realGolbin.State = STATE.IDLE;

        UnityEngine.Debug.Log("고블린이 만들어 짐  ID는 : " + _idNumuser);
        return _idNumuser;
    }

    public int CreateGolbin(int stage, Transform trans) // 열번만들고 
    {
        _idNumuser++;
        // Todo :: 진짜 고블린 만드는 코드를 여기 넣을꺼임
        Agent go = Resources.Load<Agent>(_goblinPath); // 애는 나중에 다른 매니저로 壺煞渼?

        // Unity Scene에 진짜 고블린등장 
        Agent realGolbin = UnityEngine.GameObject.Instantiate(go, trans.position, Quaternion.identity);
        realGolbin.UID = _idNumuser;
        realGolbin.State = STATE.IDLE;
        _goblinAgents.Add(realGolbin);

        // 확인용 이름바꾸기 
        realGolbin.gameObject.name = realGolbin.gameObject.name + "_" + _idNumuser;

        UnityEngine.Debug.Log("고블린이 만들어 짐  ID는 : " + _idNumuser);
        return _idNumuser;
    }

    public void SetState(int uid, STATE state)
    {
        Agent agent = _goblinAgents.Find(x => x.UID == uid);
        if(agent != null)
        {
            agent.State = state;
        }
    }

    public Agent GetAgent(int uid)
    {
        Agent agent = _goblinAgents.Find(x => x.UID == uid);

        if(agent != null)
        {
            return agent;
        }
        return null;
        
    }

    // List에서 ID를 찾아서 지워봅시다. 
    public void DeleteGolbin(int uid)
    {   
        UnityEngine.Debug.Log("고블린 지움");

        Agent removeObj = null;
        
        // 리스트 반복문으로 도는 부분 
        for(int i =0; i < _goblinAgents.Count; i++) // 10 
        {
            if(uid == _goblinAgents[i].UID)
            {
                removeObj = _goblinAgents[i];                
            }
        }

        if(removeObj != null)
        {
            _goblinAgents.Remove(removeObj);
            GameObject.Destroy(removeObj.gameObject);
        }
    }
}

// 메모장이나 엑셀로 만들파일을 읽어올껍니다. 
// Strting 
public class TableLoader
{
    private Dictionary<int, MonsterData> _tableDatas = new Dictionary<int, MonsterData>();
    public void LoadTable()
    {
        TextAsset txtcls = Resources.Load<TextAsset>("Table/ExcelTable");
        UnityEngine.Debug.Log(txtcls.text);
        string[] line = txtcls.text.Split('\n');

        for (int i = 1; i < line.Length - 1; i++)
        {
            string[] rows = line[i].Split(',');

            MonsterData data = new MonsterData();
            data.SetData(int.Parse(rows[0]), rows[1], int.Parse(rows[2]), int.Parse(rows[3]),int.Parse(rows[4]));
            UnityEngine.Debug.Log(line[i]);
        }
    }

    public class MonsterData
    {
        public int ID;
        public string Name;
        public int Hp;
        public int Speed;
        public int Power;
        public void SetData(int id, string name, int hp, int speed, int power)
        {
            ID = id;    
            Name = name;
            Hp = hp;
            Speed = speed;
            Power = power;
        }
    }
}