using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Army/Create Army Database", fileName = "ArmyDatabase", order = 1)]
public class ArmyDatabaseSO : ScriptableObject
{
    [SerializeField] private List<ArmyModelSO> _armyModelSOs = new List<ArmyModelSO>();
    public List<ArmyModelSO> armyModelSOs => _armyModelSOs;

    public void AddArmyModel(ArmyModelSO unitModelSO)
    {
        if (!_armyModelSOs.Contains(unitModelSO))
        {
            _armyModelSOs.Add(unitModelSO);
        }

        for (int i = 0; i < _armyModelSOs.Count; i++)
        {
            if(_armyModelSOs[i] == null)
            {
                _armyModelSOs.RemoveAt(i);
                i = 0;
            }
        }

        UnityEditor.EditorUtility.SetDirty(this);
    }
}
