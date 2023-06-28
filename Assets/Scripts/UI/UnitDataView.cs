using TMPro;
using Units.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class UnitDataView : ActiveUI
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [FormerlySerializedAs("_healthBar")] [SerializeField] private HealthBarView healthBarView;
        
        public override void Activate() => gameObject.SetActive(true);

        public override void Deactivate() => gameObject.SetActive(false);

        public void UpdateData(Enums.UnitDataType dataType, IUnitData unitData)
        {
            healthBarView.UpdateAmount(unitData.CurrentHealth, unitData.MaxHealth);

            if (dataType == Enums.UnitDataType.Full)
            {
                _icon.sprite = unitData.Icon;
                _name.text = unitData.Name;
            }
        }
    }
}