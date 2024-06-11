using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _textEnemyCount;

    public static int EnemyCount = 23;

    private void Update()
    {
        _textEnemyCount.text = EnemyCount.ToString();
    }
}
