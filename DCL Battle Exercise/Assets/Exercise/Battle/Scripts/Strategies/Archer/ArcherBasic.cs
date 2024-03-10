using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBasic : Strategy
{
    public ArcherBasic(UnitBase unitBase) : base(unitBase)
    {
    }

    public override UnitType unitType { get; protected set; } = UnitType.ARCHER;

    public override void Update(List<UnitBase> allies, List<UnitBase> enemies)
    {
        Utils.GetNearestObject(unitBase, enemies, out UnitBase nearestEnemy);

        if (nearestEnemy == null)
            return;

        Vector3 toNearest = (nearestEnemy.transform.position - unitBase.transform.position).normalized;
        toNearest.Scale(new Vector3(1, 0, 1));
        unitBase.Move(toNearest.normalized);

        unitBase.Attack(nearestEnemy);
    }
}
