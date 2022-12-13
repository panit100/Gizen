using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "NewEnemy")]
public class EnemySetting : ScriptableObject
{
    [SerializeField] float health = 10;
    [SerializeField] float speed = 5;
    [SerializeField] AttackType enemyType;

    public float Health {get {return health;}}
    public float Speed {get {return speed;}}
    public AttackType EnemyType {get {return enemyType;}}
}
