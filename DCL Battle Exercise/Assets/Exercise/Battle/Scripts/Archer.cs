using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Archer : UnitBase
{
    public ArcherArrow arrowPrefab;

    public Archer(GameObject unitObject, Army army, IArmyModel armyModel, Bounds spawnBounds) :
        base(unitObject, army, armyModel, spawnBounds)
    {
        switch (army.armyStrategy)
        {
            case ArmyStrategy.Defensive:
                strategy = new ArcherDefence(this);
                break;
            case ArmyStrategy.Basic:
            default:
                strategy = new ArcherBasic(this);
                break;
        }

        Initialise();
        arrowPrefab = BattleInstantiator.instance.prefabDatabase.GetWeapon(Weapon.ARCHER_ARROW).GetComponent<ArcherArrow>();
    }

    public void Initialise()
    {
        properties.health = 5;
        properties.defense = 0;
        properties.attack = 10;
        properties.maxAttackCooldown = 5f;
        properties.postAttackDelay = 1f;
        properties.attackRange = 20f;
    }

    public override void Attack(UnitBase enemy)
    {
        if ( attackCooldown > 0 )
            return;

        if ( Vector3.Distance(transform.position, enemy.transform.position) > properties.attackRange)
            return;

        attackCooldown = properties.maxAttackCooldown;

        ArcherArrow arrow = Object.Instantiate(arrowPrefab);

        arrow.Initialise(enemy.transform.position, properties.attack, this);
        arrow.transform.position = transform.position;

        animator?.SetTrigger("Attack");
        arrow.GetComponent<Renderer>().material.color = army.color;
    }
}