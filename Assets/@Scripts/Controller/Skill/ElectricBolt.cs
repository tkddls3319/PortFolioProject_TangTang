using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ElectricBolt : RepeatSkill
{
    private void Awake()
    {
        //todo : 타입정의 다시
        SkillType = Define.SkillType.ElectricBolt;
    }

    protected override void DoSkillJob()
    {
        StartCoroutine(SetEnergeBolt());
    }
    IEnumerator SetEnergeBolt()
    {
        if (Managers.Game.Player != null)
        {
            List<MonsterController> targets = Managers.Object.GetNearsMonster(SkillData.ShotCount);
            if (targets == null)
                yield break;

            for (int i = 0; i < targets.Count; i++)
            {
                Vector3 dir = Managers.Game.Player.PlayerDirection;
                dir = Quaternion.AngleAxis((45 + 45 * i) * -1, Vector3.forward) * dir;

                Vector3 startPos = Managers.Game.Player.CenterPosition;

                GenerateProjectile(Owner, startPos, dir, targets[i].CenterPosition, this);

                yield return new WaitForSeconds(SkillData.ShotTime);
            }
        }
    }
}
