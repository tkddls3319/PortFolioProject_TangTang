using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SkillBase : BaseController
{
    public CreatureController Owner { get; set; }
    public Define.SkillType SkillType { get; set; }

    int level = 0;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    [SerializeField]
    public SkillData _skillData;
    public SkillData SkillData
    {
        get
        {
            return _skillData;
        }
        set
        {
            _skillData = value;
        }
    }
    public virtual void ActivateSkill()
    {
        UpdateSkillData();
    }

    public SkillData UpdateSkillData()
    {
        SkillData skillData = new SkillData();

        if(Managers.Data.SkillDatas.TryGetValue((int)SkillType, out skillData) == false)
            return SkillData;


        SkillData = skillData.DeepCopy();

        return SkillData;
    }
    public virtual void OnLevelUp()
    {
        if (Level == 0)
            ActivateSkill();
        Level++;
        UpdateSkillData();
    }

    protected virtual void GenerateProjectile(CreatureController owner, Vector3 startPos, Vector3 dir, Vector3 targetPos, SkillBase skill)
    {
        ProjectileController pc = Managers.Object.Spawn<ProjectileController>(startPos, (int)SkillType);
        pc.SetInfo(owner, startPos, dir, targetPos, skill);
    }
}
