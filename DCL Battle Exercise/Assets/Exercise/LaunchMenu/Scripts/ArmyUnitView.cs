using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmyUnitView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private Slider _slider;

    private ArmyModelSO _armyModelSO;
    private UnitType _unitType;

    public void Initialize(UnitType unitType, IArmyModel armyModelSO)
    {
        _unitType = unitType;
        _armyModelSO = armyModelSO as ArmyModelSO;

        _title.text = _unitType.ToString();
        _slider.value = _armyModelSO.GetUnitAmount(_unitType);
        _value.text = _slider.value.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        _armyModelSO.SetUnitData(_unitType, (int)value);
        _value.text = value.ToString();
    }
}
