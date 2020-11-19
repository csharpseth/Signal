using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerStats))]
public class PlayerStatsAdjuster : MonoBehaviour
{
    public StatAdjustment[] timeBasedAdjuster;

    private PlayerStats stats;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        for (int i = 0; i < timeBasedAdjuster.Length; i++)
        {
            StatAdjustment adj = timeBasedAdjuster[i];
            adj.timer += Time.deltaTime;
            if(adj.timer >= adj.delay)
            {
                adj.timer = 0f;
                stats.FindStatAndAdjustValue(adj.targetStat, adj.value);
            }
            adj.uiBar.fillAmount = stats.FindStatPercent(adj.targetStat);
            timeBasedAdjuster[i] = adj;
        }
    }
}

[System.Serializable]
public struct StatAdjustment
{
    public string targetStat;
    public int value;
    public float delay;
    public Image uiBar;

    [HideInInspector]
    public float timer;
}


