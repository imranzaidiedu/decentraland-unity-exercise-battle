using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private ArmyDatabaseSO _armyDatabase;
    [SerializeField] private ArmyView _armyViewPrefab;
    [SerializeField] private Transform _armiesParent;

    private ArmyPresenter[] armyPresenters;

    void Start()
    {
        _startButton.onClick.AddListener(OnStart);
        ArmyView tempArmyView;
        armyPresenters = new ArmyPresenter[_armyDatabase.armyModelSOs.Count];

        for (int i = 0; i < _armyDatabase.armyModelSOs.Count; i++)
        {
            tempArmyView = Instantiate(_armyViewPrefab, _armiesParent);
            armyPresenters[i] = new ArmyPresenter(_armyDatabase.armyModelSOs[i], tempArmyView);
            tempArmyView.BindPresenter(armyPresenters[i]);
        }
    }

    void OnStart()
    {
        SceneManager.LoadScene(1);
    }
}