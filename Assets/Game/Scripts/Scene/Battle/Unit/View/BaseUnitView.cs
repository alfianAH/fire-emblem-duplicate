using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Unit.Enum;
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
        [SerializeField] private Image _unitProfile;
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

            switch (unit.BaseUnitSO.Affinity)
            {
                case UnitAffinity.Red:
                    _unitType.color = Color.red;
                    break;

                case UnitAffinity.Green:
                    _unitType.color = Color.green;
                    break;

                case UnitAffinity.Blue:
                    _unitType.color = Color.blue;
                    break;
            }

            switch (unit.BaseUnitSO.Side)
            {
                case UnitSide.Player:
                    ColorUtility.TryParseHtmlString("#1AE700", out Color allyProfileColor);
                    _unitProfile.color = allyProfileColor;
                    break;

                case UnitSide.Enemy:
                    ColorUtility.TryParseHtmlString("#E72400", out Color enemyProfileColor);
                    _unitProfile.color = enemyProfileColor;
                    break;
            }
        }
    }
}