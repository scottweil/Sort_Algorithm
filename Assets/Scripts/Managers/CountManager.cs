using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountManager : SettingBase
{
    public override void Init()
    {
        max = 300;
        Value = 100;
        base.Init();
    }

    public override void OnValueChange()
    {
        Value = Mathf.RoundToInt(Slider.value * max);
    }
}
