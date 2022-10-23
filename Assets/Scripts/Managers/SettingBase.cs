using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class SettingBase : MonoBehaviour
{
    [SerializeField] protected Slider Slider;
    [SerializeField] TMP_Text Text;
    protected int max;
    protected int curValue;

    public int Value
    {
        get { return curValue; }
        protected set
        {
            curValue = value;
            Text.text = $"{curValue}";
        }
    }

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        Slider.minValue = (float)1 / max;
        Slider.value = (float)Value / max;
    }

    public abstract void OnValueChange();
}
