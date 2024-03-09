public interface IArmyModel
{
    ArmyData[] armyData { get; set; }
    ArmyStrategy strategy { get; set; }
}

[System.Serializable]
public class ArmyData
{
    public UnitType unitType;
    public int amount;

    public ArmyData(UnitType unitType, int amount)
    {
        this.unitType = unitType;
        this.amount = amount;
    }
}

public enum ArmyStrategy
{
    Basic = 0,
    Defensive = 1
}