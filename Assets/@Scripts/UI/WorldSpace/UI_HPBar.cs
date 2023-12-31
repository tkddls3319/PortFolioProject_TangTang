using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar
    }
    PlayerController _player;
    public override bool Init()
    {

        if (base.Init() == false)
            return false;

        Bind<GameObject>(typeof(GameObjects));
        _player = Managers.Game.Player;
        return true;
    }

    private void LateUpdate()
    {
        float ratio = _player.Hp / (float)_player.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }

}
