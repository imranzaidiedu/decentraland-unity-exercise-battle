using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Unit/Create Unit Datavase", fileName = "UnitDatabase", order = 1)]
public class UnitsDatabaseSO : ScriptableObject
{
    [SerializeField] private List<UnitModelSO> _unitModelSOs = new List<UnitModelSO>();
    public List<UnitModelSO> unitModelSOs => _unitModelSOs;

    public void AddUnitModel(UnitModelSO unitModelSO)
    {
        for (int i = 0; i < _unitModelSOs.Count; i++)
        {
            if(_unitModelSOs[i].unitType == unitModelSO.unitType)
            {
                return;
            }
        }

        _unitModelSOs.Add(unitModelSO);

        UnityEditor.EditorUtility.SetDirty(this);
    }

    public UnitModelSO GetModelByType(UnitType unitType)
    {
        return _unitModelSOs.Find(x => x.unitType == unitType);
    }
}
