using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherArrow : MonoBehaviour
{
    public float speed;

    [NonSerialized] public Vector3 target;
    [NonSerialized] public float attack;

    public UnitBase source;

    public void Initialise(Vector2 target, float attack, UnitBase source)
    {
        this.target = target;
        this.attack = attack;
        this.source = source;
    }

    public void Update()
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed;
        transform.forward = direction;

        foreach ( UnitBase unit in source.army.enemyArmy.GetUnits() )
        {
            float dist = Vector3.Distance(unit.transform.position, transform.position);

            if (dist < speed)
            {
                unit.Hit(null, attack);
                Destroy(gameObject);
                return;
            }
        }

        if ( Vector3.Distance(transform.position, target) < speed)
        {
            Destroy(gameObject);
        }
    }
}