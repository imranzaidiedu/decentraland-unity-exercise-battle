using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Army/Create Army Model", fileName = "ArmyModel", order = 0)]
public class ArmyModelSO : ScriptableObject, IArmyModel
{
    [SerializeField] private string _armyTitle = "Army";
    public string armyTitle => _armyTitle;

    [SerializeField] private ArmyData[] armyDataValue;
    public ArmyData[] armyData
    {
        get => armyDataValue;
        set => armyDataValue = value;
    }

    [SerializeField] private ArmyStrategy strategyValue = ArmyStrategy.Basic;
    public ArmyStrategy strategy
    {
        get => strategyValue;
        set => strategyValue = value;
    }

    private void Reset()
    {
        return;
        ArmyDatabaseSO armyDatabaseSO = Resources.Load<ArmyDatabaseSO>("ArmyDatabase");
        armyDatabaseSO.AddArmyModel(this);
    }

    private void OnDestroy()
    {
        Reset();
    }

    private void OnValidate()
    {
        return;
        UnitsDatabaseSO unitsDatabaseSO = Resources.Load<UnitsDatabaseSO>("UnitDatabase");

        if(armyDataValue == null)
        {
            Initialise(unitsDatabaseSO);
        }
        else if(unitsDatabaseSO.unitModelSOs.Count != armyDataValue.Length)
        {
            Initialise(unitsDatabaseSO);
        }
    }

    private void Initialise(UnitsDatabaseSO unitsDatabaseSO)
    {
        armyDataValue = new ArmyData[unitsDatabaseSO.unitModelSOs.Count];

        for (int i = 0; i < unitsDatabaseSO.unitModelSOs.Count; i++)
        {
            armyData[i] = new ArmyData(unitsDatabaseSO.unitModelSOs[i].unitType, 0);
        }
    }

    public void SetUnitData(UnitType unitType, int amount)
    {
        for (int i = 0; i < armyDataValue.Length; i++)
        {
            if(armyDataValue[i].unitType == unitType)
            {
                armyDataValue[i].amount = amount;
                return;
            }
        }
    }

    public int GetUnitAmount(UnitType unitType)
    {
        for (int i = 0; i < armyDataValue.Length; i++)
        {
            if (armyDataValue[i].unitType == unitType)
            {
                return armyDataValue[i].amount;
            }
        }

        return 0;
    }
}