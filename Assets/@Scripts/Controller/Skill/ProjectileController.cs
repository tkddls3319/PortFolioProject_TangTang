using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.UIElements;

public class ProjectileController : SkillBase
{
    CreatureController _master;
    public SkillBase Skill;
    Vector2 _spawnPos;
    Vector3 _moveDir = Vector3.zero;
    Vector3 _target = Vector3.zero;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        return true;
    }
    public void SetInfo(CreatureController owner, Vector3 startPos, Vector3 moveDir, Vector3 targetPos, SkillBase skill)
    {
        _master = owner;
        Skill = skill;
        SkillData = skill.SkillData;
        _moveDir = moveDir;
        _spawnPos = startPos;
        _target = targetPos;
        gameObject.transform.localScale = Vector3.one * Skill.SkillData.Scala;

        AnimatorController animator = Managers.Resource.Load<AnimatorController>($"{SkillData.AnimatorName}");
        Animator anim = GetComponent<Animator>();

        switch (skill.SkillType)    
        {
            case Define.SkillType.EnergyBolt:
                StartCoroutine( CoEnergyBolt());
                break;

            case Define.SkillType.ElectricBolt:
                StartCoroutine(CoElectricBolt());
                break;
        } 

        anim.runtimeAnimatorController = animator;
        anim.Play(SkillData.AnimationName);

        StartCoroutine(CoDestroy(Skill.SkillData.LifeTime));
    }

    public override void UpdateController()
    {
    }
    #region ��ų �ڷ�ƾ
    IEnumerator CoEnergyBolt()
    {
        while (true)
        {
            Vector3 nextPos = transform.position + _moveDir * Skill.SkillData.Speed * Time.deltaTime;
            GetComponent<Rigidbody2D>().MovePosition(nextPos);
            yield return new WaitForFixedUpdate();
        }
    }
    IEnumerator CoElectricBolt()
    {
        while (true)
        {
            Vector3 nextPos = transform.position + _moveDir * Skill.SkillData.Speed * Time.deltaTime;
            GetComponent<Rigidbody2D>().MovePosition(nextPos);
            yield return new WaitForFixedUpdate();
        }
    }
    #endregion
    IEnumerator CoDestroy(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Managers.Object.Dspawn(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MonsterController mc = collision.gameObject.GetComponent<MonsterController>();
        if (mc.IsMyNotNullActive() == false)
            return;

        if (this.IsMyNotNullActive() == false)
            return;

        mc.OnDamaged(_master, Skill.SkillData.SkillInfos[Skill.Level].Damage * _master.Damage);
        Managers.Object.Dspawn(this);
    }
}
