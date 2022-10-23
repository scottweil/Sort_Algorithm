using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : SettingBase
{
    public override void Init()
    {
        max = 1000;
        Value = 100;
        base.Init();
    }

    public override void OnValueChange()
    {
        Value = Mathf.RoundToInt(Slider.value * max);
    }
}
