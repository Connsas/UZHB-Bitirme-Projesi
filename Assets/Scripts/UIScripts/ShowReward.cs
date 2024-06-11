using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowReward : MonoBehaviour
{
    [SerializeField] private TMP_Text rewardValue;

    void Awake()
    {
        rewardValue.text = "0";
    }

    void Update()
    {
        rewardValue.text = PlayerStats.reward.ToString();
    }
}
