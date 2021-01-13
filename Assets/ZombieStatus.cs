using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Zombie", menuName = "My Menu/Create Zombie", order = int.MaxValue)]
public class ZombieStatus : ScriptableObject
{
    [SerializeField] private int _hp;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _attackSpeed;
    [SerializeField] private int _damage;

    public int Hp { get { return _hp; } }
    public int MoveSpeed { get { return _moveSpeed; } }
    public int AttackSpeed { get { return _attackSpeed; } }
    public int Damage { get { return _damage; } }
}
