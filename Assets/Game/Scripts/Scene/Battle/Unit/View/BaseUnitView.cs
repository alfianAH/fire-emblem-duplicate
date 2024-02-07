using FireEmblemDuplicate.Message;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.Battle.Unit.View
{
    public class BaseUnitView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _unitName;
        [SerializeField] private TextMeshProUGUI _unitCurrentHp;
        [SerializeField] private TextMeshProUGUI _unitFullHp;
        [SerializeField] private TextMeshProUGUI _unitAtk;
        [SerializeField] private TextMeshProUGUI _unitDef;
        [SerializeField] private TextMeshProUGUI _unitRes;
        [SerializeField] private TextMeshProUGUI _unitLuk;
        [SerializeField] private Image _unitType;
        [SerializeField] private Image _unitWeapon;

        public void SetView(SetCurrentUnitOnClickMessage message)
        {
            BaseUnit unit = message.UnitController.Unit;
            BaseUnitStats unitStats = unit.UnitStats;
            
            _unitName.text = unit.BaseUnitSO.Name;
            _unitCurrentHp.text = unitStats.BaseHP.ToString();
            _unitFullHp.text = $"/ {unit.BaseUnitSO.UnitStats.BaseHP}";
            _unitAtk.text = unitStats.BaseATK.ToString();
            _unitDef.text = unitStats.BaseDEF.ToString();
            _unitRes.text = unitStats.BaseRES.ToString();
            _unitLuk.text = unitStats.BaseLUK.ToString();

            _unitType.sprite = unit.UnitTypeSprite;
            _unitWeapon.sprite = unit.WeaponController.WeaponSO.WeaponSprite;
        }
    }
}