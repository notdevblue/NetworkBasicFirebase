using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 3번
public class RankVO : MonoBehaviour
{
    static public RankVO Instance  { get; private set; }

    public SortedDictionary<string, object> rankDictionary = new SortedDictionary<string, object>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddRank(string name, object time)
    {
        rankDictionary.Add(name, time);
    }

    public List<string> GetRank()
    {
        List<string> rank = new List<string>();


        foreach (KeyValuePair<string, object> data in rankDictionary.OrderBy(key => key.Value))
        {
            rank.Add($"{data.Key}: {data.Value}");
        }

        return rank;
    }
}
