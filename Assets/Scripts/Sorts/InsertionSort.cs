using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSort : SortBase
{
    public override void StartSort()
    {
        if (currentCotoutine != null)
        {
            StartCoroutine(coroutine);
            return;
        }
        coroutine = OnInsertion();
        currentCotoutine = StartCoroutine(coroutine);
    }

    IEnumerator OnInsertion()
    {
        for (int i = 1; i < elementList.Count; i++)
        {
            Element newBoss = elementList[i];
            newBoss.ToggleColor(Define.ColorType.Boss);

            for (int j = i - 1; j >= 0; j--)
            {
                elementList[j].ToggleColor(Define.ColorType.Compare);

                yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

                if (newBoss.size < elementList[j].size)
                {
                    int newBossIndex = elementList.IndexOf(newBoss);
                    Vector3 tempPos = newBoss.temp.transform.position;

                    newBoss.temp.transform.position = elementList[j].temp.transform.position;
                    elementList[j].temp.transform.position = tempPos;
                    elementList[j].ToggleColor();

                    elementList[newBossIndex] = elementList[j];
                    elementList[j] = newBoss;

                }
                else
                {
                    elementList[j].ToggleColor();
                    break;
                }
            }
            newBoss.ToggleColor();
        }
    }
}
