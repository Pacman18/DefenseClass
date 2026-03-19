using UnityEngine;
using System.Collections.Generic;

public class MonsterData
{
    public int ID;
    public string Name;
    public int HP;
    public int Speed;
    public int CoolTime;
    public string Path;
}

public class TableLoader
{
    public static List<MonsterData> Load(string path)
    {
        List<MonsterData> list = new List<MonsterData>();

        // txt도 TextAsset으로 로드됨
        TextAsset txtFile = Resources.Load<TextAsset>(path);

        if (txtFile == null)
        {
            Debug.LogError("파일 못찾음: " + path);
            return list;
        }

        string[] lines = txtFile.text.Split('\n');

        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] values = line.Split(',');

            if (!int.TryParse(values[0].Trim(), out _))
                continue;

            MonsterData data = new MonsterData();
            data.ID = int.Parse(values[0].Trim());
            data.Name = values[1].Trim();
            data.HP = int.Parse(values[2].Trim());
            data.Speed = int.Parse(values[3].Trim());
            data.CoolTime = int.Parse(values[4].Trim());
            data.Path = values[5].Trim();

            list.Add(data);

            Debug.Log("Loaded Monster: " + data.Name);
        }

        return list;
    }
}
