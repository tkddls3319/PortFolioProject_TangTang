using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
   public Dictionary<int, SkillData> SkillData { get; private set; }

    public void Init()
    {
        //TODO ������ ����
    }

    void SkillSetting()
    {
        SkillData sd = new SkillData(1, 10, 1.0f);
        SkillData.Add(1,sd);
    }
}
