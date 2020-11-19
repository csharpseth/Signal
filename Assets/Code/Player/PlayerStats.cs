using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public List<Stat> stats;

    private void Awake()
    {
        instance = this;
    }

    public void ConsumeResource(Resource r)
    {
        FindStatAndAdjustValue(r.identifier, r.value);
        r.Consume();
    }

    public bool TryConsumeResource()
    {
        GameObject lookingAt = PlayerMouseController.instance.LookingAt();
        if (lookingAt == null) return false;

        Resource res = lookingAt.GetComponent<Resource>();
        if (res == null) return false;

        FindStatAndAdjustValue(res.identifier, res.value);
        res.Consume();

        return true;
    }

    public float FindStatPercent(string id)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if (stats[i].identifier.ToLower() == id.ToLower())
            {
                return (float)stats[i].value / stats[i].maxValue;
            }
        }

        return 1f;
    }

    public void FindStatAndAdjustValue(string id, int adjustment)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            if(stats[i].identifier.ToLower() == id.ToLower())
            {
                stats[i].Adjust(adjustment);
                if(stats[i].value <= stats[i].minValue)
                {
                    //Die
                    ScreenController.instance.Die();
                }
                break;
            }
        }
    }

}

[System.Serializable]
public class Stat
{
    public string identifier;
    public int value;
    public int maxValue;
    public int minValue;
    public StatValueAction minValueAction;


    public void Adjust(int amount)
    {
        value += amount;
        value = Mathf.Clamp(value, minValue, maxValue);
    }
}

public enum StatValueAction
{
    KillPlayer
}
