using System.Collections;
using System.Collections.Generic;

public abstract class Strategy
{
    public abstract UnitType unitType { get; protected set; }
    public UnitBase unitBase { get; private set; }

    public Strategy(UnitBase unitBase)
    {
        this.unitBase = unitBase;
    }

    public abstract void Update(List<UnitBase> allies, List<UnitBase> enemies);
}
