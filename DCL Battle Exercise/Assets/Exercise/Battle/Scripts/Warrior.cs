using System;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : UnitBase
{
    public Warrior(GameObject unitObject, Army army, IArmyModel armyModel, Bounds spawnBounds) :
        base(unitObject, army, armyModel, spawnBounds)
    {
        switch (army.armyStrategy)
        {
            case ArmyStrategy.Defensive:
                strategy = new WarriorDefence(this);
                break;
            case ArmyStrategy.Basic:
            default:
                strategy = new WarriorBasic(this);
                break;
        }

        Initialise();
    }

    public void Initialise()
    {
        properties.health = 50;
        properties.defense = 5;
        properties.attack = 20;
        properties.maxAttackCooldown = 1f;
        properties.postAttackDelay = 0;
        properties.attackRange = 2.5f;
    }

    public override void Attack( UnitBase target )
    {
        if ( attackCooldown > 0 )
            return;

        if ( target == null )
            return;

        if ( Vector3.Distance(transform.position, target.transform.position) > properties.attackRange )
            return;

        attackCooldown = properties.maxAttackCooldown;

        animator.SetTrigger("Attack");

        target.Hit(this);
    }
}