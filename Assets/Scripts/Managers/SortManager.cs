using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SortManager : MonoBehaviour
{
    private static SortManager instance;
    private static SortManager Instance { get { Init(); return instance; } }

    [SerializeField] GameObject elePrefab;
    [SerializeField] CountManager CountManager_temp;
    [SerializeField] SpeedManager SpeedManager_temp;

    public static CountManager CountManager { get { return Instance.CountManager_temp; } }
    public static SpeedManager SpeedManager { get { return Instance.SpeedManager_temp; } }
    public static GameObject ElementPrefab { get { return Instance.elePrefab; } }

    [SerializeField]
    SortBase[] SortMenu;
    int sortIndex = 0;

    SortBase nowSort;

    [SerializeField]
    TMP_Text sortMenuTitle;

    Vector3 camOriginPos;

    public int SortIndex
    {
        get { return sortIndex; }
        private set
        {
            sortIndex = value;
            if (sortIndex >= SortMenu.Length)
            {
                sortIndex = SortMenu.Length - 1;
                return;
            }

            if (sortIndex < 0)
            {
                sortIndex = 0;
                return;
            }
            if (nowSort == null) nowSort = SortMenu[sortIndex];
            nowSort.ClearSort();
            sortMenuTitle.text = SortMenu[sortIndex].gameObject.name;
            nowSort = Instance.SortMenu[sortIndex];
            nowSort.Init();
        }
    }

    private void Awake()
    {
        Init();
    }

    private static void Init()
    {
        if(instance == null)
        {
            GameObject go = FindObjectOfType<SortManager>().gameObject;

            if(go == null)
            {
                go = new GameObject("@SortManager");
                go.AddComponent<SortManager>();
            }

            instance = go.GetComponent<SortManager>();
            instance.SortMenu = new SortBase[instance.transform.childCount];
            for (int i = 0; i < instance.transform.childCount; i++)
            {
                instance.SortMenu[i] = instance.transform.GetChild(i).GetComponent<SortBase>();
            }
            instance.SortIndex = 0;
            instance.camOriginPos = Camera.main.transform.position;
        }
    }

    public void OnCountChange()
    {
        CountManager.OnValueChange();
        nowSort.Init();

        if(CountManager.Value > 200)
        {
            Camera.main.fieldOfView = 100f;
        }
        else
        {
            Camera.main.fieldOfView = 60f;
        }
    }

    public void OnSpeedChange()
    {
        SpeedManager.OnValueChange();
    }

    void CameraAdjust()
    {
        Camera.main.fieldOfView = CountManager.Value * 0.3f + 30f;
    }

    public void OnPrevButton()
    {
        SortIndex--;
    }

    public void OnNextButton()
    {
        SortIndex++;
    }

    public void OnStartButton()
    {
        SortMenu[SortIndex].StartSort();
    }

    public void OnPauseButton()
    {
        nowSort.PauseSort();
    }
    public void OnExitButton()
    {
        Application.Quit();
    }
}
