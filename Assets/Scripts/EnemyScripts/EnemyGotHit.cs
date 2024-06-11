using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGotHit : MonoBehaviour
{

    public enum BodyParts
    {
        HEAD,
        BODY
    }

    [SerializeField] private Enemy _gameObject;
    [SerializeField] private Enemy.Direction _direction;
    [SerializeField] private BodyParts _bodyPart;


    public void Hit(int damage)
    {
        switch (_bodyPart)
        {
            case BodyParts.HEAD:
                damage = damage * 2;
                break;
            case BodyParts.BODY:
                break;
        }
        _gameObject.EnemyGotHit(damage, _direction);
    }
}
