using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSort : SortBase
{
    public override void StartSort()
    {
        if (currentCotoutine != null)
        {
            StartCoroutine(coroutine);
            return;
        }
        coroutine = Partition(0, elementList.Count - 1);
        currentCotoutine = StartCoroutine(coroutine);
    }

    IEnumerator OnMerge(int start, int end, int mid)
    {
        //List.Add�� �� ��� �ε��� ������ �ߴµ� �ٺ����� �� �׷����� �𸣰� ��̴�.
        //�翬�� Add�� ���� �ε����� 0���� �������� start���� �����ϴ� part_index�� �����ϸ� ������ ��� �� �ۿ�...
        Element[] tempList = new Element[elementList.Count];
        Vector3[] tempVectors = new Vector3[elementList.Count];
        for (int i = start; i <= end; i++)
        {
            tempList[i] = elementList[i];
            tempVectors[i] = elementList[i].temp.transform.position;
        }

        int part1_index = start;
        int part2_index = mid + 1;
        int origin_index = start;


        elementList[mid].ToggleColor(Define.ColorType.Boss);
        elementList[end].ToggleColor(Define.ColorType.Boss);

        while (part1_index <= mid && part2_index <= end)
        {
            elementList[part1_index].ToggleColor(Define.ColorType.Compare);
            elementList[part2_index].ToggleColor(Define.ColorType.Compare);

            yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

            if (tempList[part1_index].size < tempList[part2_index].size)
            {
                elementList[origin_index] = tempList[part1_index];
                part1_index++;
            }
            else
            {
                elementList[origin_index] = tempList[part2_index];
                part2_index++;
            }
            origin_index++;
        }

        for (int i = 0; i <= mid - part1_index; i++)
        {
            yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

            elementList[origin_index + i].ToggleColor(Define.ColorType.Compare);
            elementList[origin_index + i] = tempList[part1_index + i];
        }

        for (int i = start; i <= end; i++)
        {
            yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

            elementList[i].temp.transform.position = tempVectors[i];
            elementList[i].ToggleColor();
        }

    }

    IEnumerator Partition(int start, int end)
    {
        if (start < end)
        {
            int mid = (start + end) / 2;

            yield return StartCoroutine(Partition(start, mid));
            yield return StartCoroutine(Partition(mid + 1, end));
            yield return StartCoroutine(OnMerge(start, end, mid));
        }
    }
}
