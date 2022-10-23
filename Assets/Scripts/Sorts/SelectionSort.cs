using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSort : SortBase
{
    public override void StartSort()
    {
        if (currentCotoutine != null)
        {
            StartCoroutine(coroutine);
            return;
        }
        coroutine = OnSelection();
        currentCotoutine = StartCoroutine(coroutine);
    }

    IEnumerator OnSelection()
    {
        for (int i = 0; i < elementList.Count - 1; i++)
        {
            Element newBoss = elementList[i];
            elementList[i].ToggleColor(Define.ColorType.Boss);

            for (int j = i + 1; j < elementList.Count; j++)
            {
                elementList[j].ToggleColor(Define.ColorType.Compare);

                yield return new WaitForSeconds((float)1 / SortManager.SpeedManager.Value);

                if(newBoss.size > elementList[j].size)
                {
                    if (newBoss != elementList[i]) newBoss.ToggleColor();

                    newBoss = elementList[j];
                    newBoss.ToggleColor(Define.ColorType.newBoss);
                }
                else
                {
                    elementList[j].ToggleColor();
                }
            }

            elementList[i].ToggleColor();
            newBoss.ToggleColor();

            int newBossIndex = elementList.IndexOf(newBoss);
            Vector3 newBossPos = newBoss.temp.transform.position;

            newBoss.temp.transform.position = elementList[i].temp.transform.position;
            elementList[i].temp.transform.position = newBossPos;

            elementList[newBossIndex] = elementList[i];
            elementList[i] = newBoss;
        }
    }
}
