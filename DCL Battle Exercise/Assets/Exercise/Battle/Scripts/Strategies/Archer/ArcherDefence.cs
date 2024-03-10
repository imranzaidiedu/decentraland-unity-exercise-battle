using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherDefence : Strategy
{
    public ArcherDefence(UnitBase unitBase) : base(unitBase)
    {
        
    }

    public override UnitType unitType { get; protected set; } = UnitType.ARCHER;

    public override void Update(List<UnitBase> allies, List<UnitBase> enemies)
    {
        Vector3 enemyCenter = Utils.GetCenter(enemies);
        float distToEnemyX = Mathf.Abs(enemyCenter.x - unitBase.transform.position.x);

        if (distToEnemyX > unitBase.properties.attackRange)
        {
            if (enemyCenter.x < unitBase.transform.position.x)
                unitBase.Move(Vector3.left);

            if (enemyCenter.x > unitBase.transform.position.x)
                unitBase.Move(Vector3.right);
        }

        float distToNearest = Utils.GetNearestObject(unitBase, enemies, out UnitBase nearestEnemy);

        if (nearestEnemy == null)
            return;

        if (distToNearest < unitBase.properties.attackRange)
        {
            Vector3 toNearest = (nearestEnemy.transform.position - unitBase.transform.position).normalized;
            toNearest.Scale(new Vector3(1, 0, 1));

            Vector3 flank = Quaternion.Euler(0, 90, 0) * toNearest;
            unitBase.Move(-(toNearest + flank).normalized);
        }
        else
        {
            Vector3 toNearest = (nearestEnemy.transform.position - unitBase.transform.position).normalized;
            toNearest.Scale(new Vector3(1, 0, 1));
            unitBase.Move(toNearest.normalized);
        }

        unitBase.Attack(nearestEnemy);
    }
}
