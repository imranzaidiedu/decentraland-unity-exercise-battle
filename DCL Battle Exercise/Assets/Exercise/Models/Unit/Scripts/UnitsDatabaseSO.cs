using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Unit/Create Unit Datavase", fileName = "UnitDatabase", order = 1)]
public class UnitsDatabaseSO : ScriptableObject
{
    [SerializeField] private List<UnitModelSO> unitModelSOs = new List<UnitModelSO>();

    public void AddUnitModel(UnitModelSO unitModelSO)
    {
        for (int i = 0; i < unitModelSOs.Count; i++)
        {
            if(unitModelSOs[i].unitType == unitModelSO.unitType)
            {
                return;
            }
        }

        unitModelSOs.Add(unitModelSO);
    }
}
