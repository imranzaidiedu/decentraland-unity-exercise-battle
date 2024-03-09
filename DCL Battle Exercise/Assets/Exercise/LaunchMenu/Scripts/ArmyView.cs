using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IArmyView
{
    void UpdateWithModel(IArmyModel model);
}

public class ArmyView : MonoBehaviour, IArmyView
{
    [SerializeField] private ArmyUnitView _armyUnitViewPrefab;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TMP_Dropdown _strategyDropdown;
    [SerializeField] private Transform _armyUnitsParent;
    private List<ArmyUnitView> _armyUnitViews = new List<ArmyUnitView>();

    private EnumDropdownWrapper<ArmyStrategy> enumDropdown;
    private IArmyPresenter presenter = null;
    private ArmyModelSO _armyModelSO;

    private void Awake()
    {
        enumDropdown = new EnumDropdownWrapper<ArmyStrategy>(_strategyDropdown);
        enumDropdown.OnValueChanged += OnStrategyChanged;
    }

    public void BindPresenter(IArmyPresenter presenter)
    {
        this.presenter = presenter;
    }

    public void UpdateWithModel(IArmyModel model)
    {
        _armyModelSO = model as ArmyModelSO;
        _title.text = _armyModelSO.armyTitle;

        for (int i = 0; i < _armyModelSO.armyData.Length; i++)
        {
            ArmyUnitView armyUnitView = Instantiate(_armyUnitViewPrefab, _armyUnitsParent);
            armyUnitView.Initialize(_armyModelSO.armyData[i].unitType, _armyModelSO);

            _armyUnitViews.Add(armyUnitView);
        }

        enumDropdown.SetValueWithoutNotify(_armyModelSO.strategy);
    }

    private void OnStrategyChanged(ArmyStrategy strategy)
    {
        presenter?.UpdateStrategy(strategy);
    }

    private void OnDestroy()
    {
        enumDropdown.OnValueChanged -= OnStrategyChanged;
        enumDropdown?.Dispose();
    }
}