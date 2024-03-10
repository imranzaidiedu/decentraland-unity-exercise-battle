using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDefence : Strategy
{
    public WarriorDefence(UnitBase unitBase) : base(unitBase)
    {
    }

    public override UnitType unitType { get; protected set; } = UnitType.WARRIOR;

    public override void Update(List<UnitBase> allies, List<UnitBase> enemies)
    {
        Vector3 enemyCenter = Utils.GetCenter(enemies);

        if (Mathf.Abs(enemyCenter.x - unitBase.transform.position.x) > 20)
        {
            if (enemyCenter.x < unitBase.transform.position.x)
                unitBase.Move(Vector3.left);

            if (enemyCenter.x > unitBase.transform.position.x)
                unitBase.Move(Vector3.right);
        }

        Utils.GetNearestObject(unitBase, enemies, out UnitBase nearestObject);

        if (nearestObject == null)
            return;

        if (unitBase.attackCooldown <= 0)
            unitBase.Move((nearestObject.transform.position - unitBase.transform.position).normalized);
        else
        {
            unitBase.Move((nearestObject.transform.position - unitBase.transform.position).normalized * -1);
        }

        unitBase.Attack(nearestObject);
    }
}
