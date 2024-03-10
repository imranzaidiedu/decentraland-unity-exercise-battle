using System.Collections.Generic;
using UnityEngine;

public class Army
{
    //public class UnitsByType
    //{
    //    public UnitType unitType { get; private set; }
    //    public UnitBase[] units;

    //    public UnitsByType(UnitType unitType, int unitsCount)
    //    {
    //        this.unitType = unitType;
    //        units = new UnitBase[unitsCount];
    //    }
    //}

    private ArmyModelSO _armyModel;

    public Army enemyArmy;
    public string name => _armyModel.name;
    public Color color => _armyModel.color;
    public ArmyStrategy armyStrategy => _armyModel.strategy;

    //public UnitsByType[] unitsByTypes;

    public List<UnitBase> _allUnits { get; private set; }

    public Army(ArmyModelSO armyModel, UnitsDatabaseSO unitsDatabase, Bounds bounds, Transform parent)
    {
        UnitModelSO unitModel;
        GameObject unitObject;
        _armyModel = armyModel;
        _allUnits = new List<UnitBase>();
        UnitBase unitBase = null;

        for (int i = 0; i < _armyModel.armyData.Length; i++)
        {
            unitModel = unitsDatabase.GetModelByType(_armyModel.armyData[i].unitType);
            for (int j = 0; j < _armyModel.armyData[i].amount; j++)
            {
                unitObject = Object.Instantiate(unitModel.prefab, parent);
                switch (unitModel.unitType)
                {
                    case UnitType.ARCHER:
                        unitBase = new Archer(unitObject, this, armyModel, bounds);
                        break;
                    case UnitType.WARRIOR:
                    default:
                        unitBase = new Warrior(unitObject, this, armyModel, bounds);
                        break;
                }
                _allUnits.Add(unitBase);
            }
        }

    }

    public List<UnitBase> GetUnits()
    {
        return _allUnits;
    }

    public void UpdateArmy()
    {
        for (int i = 0; i < _allUnits.Count; i++)
        {
            _allUnits[i].Update();
        }
    }

    public void RemoveUnit(UnitBase unitBase)
    {
        _allUnits.Remove(unitBase);
        Object.Destroy(unitBase.gameObject);
    }
}