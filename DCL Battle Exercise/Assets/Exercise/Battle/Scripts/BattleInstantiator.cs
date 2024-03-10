using System.Collections;
using UnityEditor;
using UnityEngine;


public class BattleInstantiator : MonoBehaviour
{
    public static BattleInstantiator instance { get; private set; }

    [SerializeField] private ArmyDatabaseSO _armyDatabase;
    [SerializeField] private UnitsDatabaseSO _unitsDatabase;
    [SerializeField] private PrefabDatabaseSO _prefabsDatabase;
    [SerializeField] private BoxCollider[] _spawnBounds;
    [SerializeField] private GameOverMenu gameOverMenu;

    public PrefabDatabaseSO prefabDatabase => _prefabsDatabase;
    private Army[] _armies;
    private Vector3 _forwardTarget;

    void Awake()
    {
        instance = this;

        Initialise();
        AssignEnemies();
    }

    private void Initialise()
    {
        _armies = new Army[_armyDatabase.armyModelSOs.Count];
        for (int i = 0; i < _armyDatabase.armyModelSOs.Count; i++)
        {
            _armies[i] = new Army(_armyDatabase.armyModelSOs[i], _unitsDatabase,
                _spawnBounds[i].bounds, new GameObject(_armyDatabase.armyModelSOs[i].armyTitle).transform);
        }
    }

    private void AssignEnemies()
    {
        for (int i = 0; i < _armies.Length; i++)
        {
            for (int j = 0; j < _armies.Length; j++)
            {
                if(i != j)
                {
                    _armies[i].enemyArmy = _armies[j];
                    break;
                }
            }
        }
    }

    public Army GetWonArmy()
    {
        for (int i = 0; i < _armies.Length; i++)
        {
            if (_armies[i].GetUnits().Count > 0)
                return _armies[i];
        }

        return null;
    }

    private bool HasAnyArmyWon()
    {
        int count = _armies.Length;
        for (int i = 0; i < _armies.Length; i++)
        {
            if (_armies[i].GetUnits().Count == 0)
                count--;
        }

        return count == 1;
    }

    private Vector3 GetArmiesCenter()
    {
        Vector3 center = new Vector3();
        for (int i = 0; i < _armies.Length; i++)
        {
            center += Utils.GetCenter(_armies[i].GetUnits());
        }

        return center;
    }

    void Update()
    {
        if (HasAnyArmyWon())
        {
            gameOverMenu.gameObject.SetActive(true);
            gameOverMenu.Populate();
        }
        else
        {
            //update all units
            for (int i = 0; i < _armies.Length; i++)
            {
                _armies[i].UpdateArmy();
            }
        }

        Vector3 mainCenter = GetArmiesCenter();
        mainCenter *= 0.5f;

        _forwardTarget = (mainCenter - Camera.main.transform.position).normalized;

        Camera.main.transform.forward += (_forwardTarget - Camera.main.transform.forward) * 0.1f;
    }

    
}