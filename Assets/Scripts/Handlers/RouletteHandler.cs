using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RouletteHandler : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventsHandler.SelectRewardEvent += Spin;
    }

    private void OnDisable()
    {
        GameEventsHandler.SelectRewardEvent -= Spin;
    }

    public void Spin()
    {
        GameEventsHandler.SpinControlEventHandler(false);
        GameManager.Instance.SetSpinCondition(true);
        StartCoroutine(SpinRoutine());
    }

    private IEnumerator SpinRoutine()
    {
        if(BoardManager.Instance.LinkedList.Size == 1)
        {
            var list = BoardManager.Instance.GetAllItems();
            list.First().GetComponent<IFrameable>().MakeLighted(1f);
            yield return new WaitForSeconds(1f);
            list.First().GetComponent<IFrameable>().MakeSelected();
        }
        else
        {
            var randomSpinAmount = Random.Range(10, 30);
            var list = GetList(randomSpinAmount);
            var timeToLightMultiplier = 1f / randomSpinAmount;

            for (int i = 0; i < list.Count(); i++)
            {
                var lightTime = timeToLightMultiplier * (i + 1);
                var item = list[i];
                item.GetComponent<IFrameable>().MakeLighted(lightTime);
                yield return new WaitForSeconds(lightTime);
            }

            var lastItem = list.Last();
            lastItem.GetComponent<IFrameable>().MakeSelected();
        }
    }


    public void Shuffle(List<GameObject> list)
    {
        System.Random rng = new();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    public List<GameObject> GetList(int amount)
    {
        var returnList = new List<GameObject>();
        var list = BoardManager.Instance.GetAllItems();
        Shuffle(list);

        if(amount <= list.Count())
        {
            for(int i = 0; i < amount; i++)
            {
                returnList.Add(list[i]);
            }
        }
        else
        {
            var count = amount / list.Count;

            for(int t = 0; t < count; t++)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    returnList.Add(list[i]);
                }
            }

            amount -= count * list.Count();

            for(int y = 0; y < amount; y++)
            {
                returnList.Add(list[y]);
            }
        }

        return returnList;
    }
}
