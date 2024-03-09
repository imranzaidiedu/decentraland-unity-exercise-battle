using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Unit/Create Unit Model", fileName = "UnitModel", order = 0)]
public class UnitModelSO : ScriptableObject
{
    [SerializeField] private UnitType _unitType;
    [SerializeField] private GameObject _prefab;

    public UnitType unitType => _unitType;
    public GameObject prefab => _prefab;

    private void OnValidate()
    {
        return;
        Resources.Load<UnitsDatabaseSO>("UnitDatabase")?.AddUnitModel(this);
    }
}
