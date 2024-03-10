using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract partial class UnitBase
{
    public GameObject gameObject { get; private set; }
    public Animator animator { get; private set; }
    public Transform transform { get; private set; }
    public Properties properties { get; protected set; } = new Properties();

    [HideInInspector] public Army army;
    [HideInInspector] public Strategy strategy;
    [HideInInspector] public IArmyModel armyModel;
    [HideInInspector] public float attackCooldown;

    private Vector3 lastPosition;
    List<UnitBase> _allies;
    List<UnitBase> _enemies;

    public abstract void Attack(UnitBase enemy);

    public UnitBase(GameObject unitObject, Army army, IArmyModel armyModel, Bounds spawnBounds)
    {
        gameObject = unitObject;
        this.army = army;
        this.armyModel = armyModel;

        transform = gameObject.transform;
        animator = gameObject.GetComponent<Animator>();
        gameObject.GetComponentInChildren<Renderer>().material.color = army.color;
        transform.position = Utils.GetRandomPosInBounds(spawnBounds);
    }

    public virtual void Move( Vector3 delta )
    {
        if (attackCooldown > properties.maxAttackCooldown - properties.postAttackDelay)
            return;

        transform.position += delta * properties.speed;
    }

    public virtual void Hit(UnitBase source, float damage = 0f)
    {
        float sourceAttack;

        if (source != null)
        {
            sourceAttack = source.properties.attack;
        }
        else
        {
            sourceAttack = damage;
        }

        properties.health -= Mathf.Max(sourceAttack - properties.defense, 0);

        if (properties.health < 0 )
        {
            if (source != null)
                transform.forward = source.transform.position - transform.position;

            army.RemoveUnit(this);
            
            animator?.SetTrigger("Death");
        }
        else
        {
            animator?.SetTrigger("Hit");
        }
    }

    public void Update()
    {
        if (properties.health < 0 )
            return;

        _allies = army.GetUnits();
        _enemies = army.enemyArmy.GetUnits();

        UpdateBasicRules();
        strategy.Update(_allies, _enemies);

        animator.SetFloat("MovementSpeed", (transform.position - lastPosition).magnitude / properties.speed);
        lastPosition = transform.position;
    }

    void UpdateBasicRules()
    {
        attackCooldown -= Time.deltaTime;
        EvadeAllies();
    }

    void EvadeAllies()
    {
        //Todo: Update this logic to use lesser queries and use global variables instead
        //Also use constants instead of using values like 80.0f

        List<UnitBase> allUnits = army.GetUnits().Union(army.enemyArmy.GetUnits()).ToList();

        Vector3 center = Utils.GetCenter(allUnits);

        float centerDist = Vector3.Distance(gameObject.transform.position, center);

        if ( centerDist > 80.0f )
        {
            Vector3 toNearest = (center - transform.position).normalized;
            transform.position -= toNearest * (80.0f - centerDist);
            return;
        }

        foreach ( var obj in allUnits )
        {
            float dist = Vector3.Distance(gameObject.transform.position, obj.transform.position);

            if ( dist < 2f )
            {
                Vector3 toNearest = (obj.transform.position - transform.position).normalized;
                transform.position -= toNearest * (2.0f - dist);
            }
        }
    }
}