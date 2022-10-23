using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element
{
    public GameObject temp;
    public int size;
    public Color originColor;

    public Element(GameObject temp, int size)
    {
        this.temp = temp;
        this.size = size;
    }

    public void ToggleColor(Define.ColorType type)
    {
        switch (type)
        {
            case Define.ColorType.Boss:
                temp.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
                break;
            case Define.ColorType.Compare:
                temp.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                break;
            case Define.ColorType.newBoss:
                temp.GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
                break;
            case Define.ColorType.Booking:
                temp.GetComponentInChildren<MeshRenderer>().material.color = Color.white;
                break;
            default:
                break;
        }
    }
    public void ToggleColor()
    {
        temp.GetComponentInChildren<MeshRenderer>().material.color = originColor;
    }
}

public abstract class SortBase : MonoBehaviour
{

    protected Coroutine currentCotoutine;
    protected IEnumerator coroutine;
    int _count;
    float _space = 0.3f;

    GameObject _elementPrefab;

    protected List<Element> elementList = new List<Element>();

    //private void Start()
    //{
    //    Init();
    //}

    public void Init()
    {
        ClearSort();

        //씬 배치 및 초기화
        _count = SortManager.CountManager.Value;
        _elementPrefab = SortManager.ElementPrefab;

        List<int> randomArr = new List<int>();
        for (int i = 1; i < _count + 1; i++)
        {
            randomArr.Add(i);
        }

        transform.position = -transform.forward * _space * _count / 2;

        Color tempColor = new Color(1f / _count, 1f / _count, 1f / _count);

        while(randomArr.Count > 0)
        {
            int randomIndex = Random.Range(0, randomArr.Count);
            GameObject temp = Instantiate(_elementPrefab, transform);
            Element ele = new Element(temp, randomArr[randomIndex]);

            ele.temp.transform.SetPositionAndRotation(transform.position + transform.forward * _space * elementList.Count, Quaternion.identity);
            ele.temp.transform.localScale = new Vector3(1, ele.size, 1);
            ele.temp.GetComponentInChildren<MeshRenderer>().material.color = tempColor * ele.size;
            ele.originColor = tempColor * ele.size;

            elementList.Add(ele);
            randomArr.RemoveAt(randomIndex);
        }
    }

    public abstract void StartSort();

    public void ClearSort()
    {
        if (currentCotoutine != null) StopAllCoroutines();

        foreach (Element item in elementList)
        {
            Destroy(item.temp);
        }
        elementList.Clear();
        currentCotoutine = null;
    }

    public void PauseSort()
    {
        StopAllCoroutines();
    }
}
