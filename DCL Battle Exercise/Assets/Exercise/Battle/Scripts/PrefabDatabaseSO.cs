using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/Prefab/Create Prefabs Database", fileName = "PrefabsDatabase", order = 0)]
public class PrefabDatabaseSO : ScriptableObject
{
    [SerializeField] private List<WeaponPrefab> weaponPrefabs = new List<WeaponPrefab>();

    public GameObject GetWeapon(Weapon weapon)
    {
        return weaponPrefabs.Find(x => x.weapon == weapon)?.weaponPrefab;
    }
}

[System.Serializable]
public class WeaponPrefab
{
    public Weapon weapon;
    public GameObject weaponPrefab;
}