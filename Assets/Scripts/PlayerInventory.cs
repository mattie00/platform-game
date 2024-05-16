using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> fruitCounter = new Dictionary<string, int>();

    public void AddFruit(string fruitType)
    {
        if (!fruitCounter.ContainsKey(fruitType))
        {
            fruitCounter[fruitType] = 0;
        }
        fruitCounter[fruitType]++;
        Debug.Log($"{fruitType} count: {fruitCounter[fruitType]}");
    }

    public int GetCollectedFruits(string fruitType)
    {
        if (fruitCounter.ContainsKey(fruitType))
        {
            return fruitCounter[fruitType];
        }
        return 0;
    }

}
