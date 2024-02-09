using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Unit;
using FireEmblemDuplicate.Scene.Battle.Unit.Enum;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FireEmblemDuplicate.Scene.Battle.BattleSystem.View
{
    public class BattleSystemView : MonoBehaviour
    {
        [SerializeField] private GameObject _battleSystemUi;
        [SerializeField] private TextMeshProUGUI _damageAmount;

        [Header("Ally stats")]
        [SerializeField] private TextMeshProUGUI _allyName;
        [SerializeField] private TextMeshProUGUI _allyCurrentHp;
        [SerializeField] private TextMeshProUGUI _allyFullHp;
        [SerializeField] private TextMeshProUGUI _allyAtk;
        [SerializeField] private TextMeshProUGUI _allyDef;
        [SerializeField] private TextMeshProUGUI _allyRes;
        [SerializeField] private TextMeshProUGUI _allyLuk;
        [SerializeField] private Image _allyUnitType, _allyWeapon;

        [Header("Enemy stats")]
        [SerializeField] private TextMeshProUGUI _enemyName;
        [SerializeField] private TextMeshProUGUI _enemyCurrentHp;
        [SerializeField] private TextMeshProUGUI _enemyFullHp;
        [SerializeField] private TextMeshProUGUI _enemyAtk;
        [SerializeField] private TextMeshProUGUI _enemyDef;
        [SerializeField] private TextMeshProUGUI _enemyRes;
        [SerializeField] private TextMeshProUGUI _enemyLuk;
        [SerializeField] private Image _enemyUnitType, _enemyWeapon;

        public void OnBattleBegin(OnBattleBeginMessage message)
        {
            _battleSystemUi.SetActive(true);
            SetAllyView(message.Ally);
            SetEnemyView(message.Enemy);
        }

        public void OnDecreaseHp(DecreaseHPMessage message)
        {
            StartCoroutine(WaitToDecreaseHp(message));
        }

        public void OnBattleFinish(OnBattleFinishMessage message)
        {
            _damageAmount.gameObject.SetActive(false);
            _battleSystemUi.SetActive(false);
        }

        private IEnumerator WaitToDecreaseHp(DecreaseHPMessage message)
        {
            yield return new WaitForSeconds(1f);

            _damageAmount.gameObject.SetActive(true);
            _damageAmount.text = $"-{message.Amount}";
            BaseUnitStats unitStats = message.Defender.Unit.UnitStats;

            switch (message.Defender.Unit.BaseUnitSO.Side)
            {
                case UnitSide.Player:
                    _damageAmount.rectTransform.anchoredPosition = new Vector2(-200, _damageAmount.rectTransform.anchoredPosition.y);
                    _allyCurrentHp.text = unitStats.BaseHP.ToString();
                    break;

                case UnitSide.Enemy:
                    _damageAmount.rectTransform.anchoredPosition = new Vector2(200, _damageAmount.rectTransform.anchoredPosition.y);
                    _enemyCurrentHp.text = unitStats.BaseHP.ToString();
                    break;

                default: break;
            }
        }

        private void SetAllyView(BaseUnitController unit)
        {
            BaseUnitStats unitStats = unit.Unit.UnitStats;

            _allyName.text = unit.Unit.BaseUnitSO.Name;
            _allyCurrentHp.text = unitStats.BaseHP.ToString();
            _allyFullHp.text = $"/ {unit.Unit.BaseUnitSO.UnitStats.BaseHP}";
            _allyAtk.text = unitStats.BaseATK.ToString();
            _allyDef.text = unitStats.BaseDEF.ToString();
            _allyRes.text = unitStats.BaseRES.ToString();
            _allyLuk.text = (unitStats.BaseLUK * 100f).ToString();
            _allyUnitType.sprite = unit.Unit.UnitTypeSprite;
            _allyWeapon.sprite = unit.Unit.WeaponController.WeaponSO.WeaponSprite;

            switch (unit.Unit.BaseUnitSO.Affinity)
            {
                case UnitAffinity.Red:
                    _allyUnitType.color = Color.red;
                    break;

                case UnitAffinity.Green:
                    _allyUnitType.color = Color.green;
                    break;

                case UnitAffinity.Blue:
                    _allyUnitType.color = Color.blue;
                    break;
            }
        }

        private void SetEnemyView(BaseUnitController unit)
        {
            BaseUnitStats unitStats = unit.Unit.UnitStats;

            _enemyName.text = unit.Unit.BaseUnitSO.Name;
            _enemyCurrentHp.text = unitStats.BaseHP.ToString();
            _enemyFullHp.text = $"/ {unit.Unit.BaseUnitSO.UnitStats.BaseHP}";
            _enemyAtk.text = unitStats.BaseATK.ToString();
            _enemyDef.text = unitStats.BaseDEF.ToString();
            _enemyRes.text = unitStats.BaseRES.ToString();
            _enemyLuk.text = (unitStats.BaseLUK * 100f).ToString();
            _enemyUnitType.sprite = unit.Unit.UnitTypeSprite;
            _enemyWeapon.sprite = unit.Unit.WeaponController.WeaponSO.WeaponSprite;

            switch (unit.Unit.BaseUnitSO.Affinity)
            {
                case UnitAffinity.Red:
                    _enemyUnitType.color = Color.red;
                    break;

                case UnitAffinity.Green:
                    _enemyUnitType.color = Color.green;
                    break;

                case UnitAffinity.Blue:
                    _enemyUnitType.color = Color.blue;
                    break;
            }
        }
    }
}
