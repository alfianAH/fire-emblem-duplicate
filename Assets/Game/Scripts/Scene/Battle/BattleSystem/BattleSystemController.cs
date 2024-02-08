using Croxxing.Utility;
using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage;
using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Terrain.Pool;
using FireEmblemDuplicate.Scene.Battle.Unit;
using FireEmblemDuplicate.Scene.Battle.Unit.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit.Type.Flier;
using FireEmblemDuplicate.Scene.Battle.Weapon.Enum;
using SuperMaxim.Messaging;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.BattleSystem
{
    public class BattleSystemController : MonoBehaviour
    {
        public void Fight(StartBattleMessage message)
        {
            // Check if attacker in range
            if(CheckAttackerInRange(message.Attacker, message.Defender))
            {
                // Fight
                StartCoroutine(StartBattle(message.Attacker, message.Defender));
            }
            else
            {
                // Publish message to move
                MoveAttackerWithinRange(message.Attacker, message.Defender);
                StartCoroutine(StartBattle(message.Attacker, message.Defender));
            }
        }

        private bool CheckAttackerInRange(BaseUnitController attacker, BaseUnitController defender)
        {
            // Check attacker's weapon range
            int weaponRange = attacker.Unit.WeaponController.WeaponSO.Range;
            BaseTerrain attackerTerrain = attacker.Unit.TerrainController.Terrain;
            BaseTerrain defenderTerrain = defender.Unit.TerrainController.Terrain;

            bool isInRange = Mathf.Abs(attackerTerrain.XPos - defenderTerrain.XPos) + Mathf.Abs(attackerTerrain.YPos - defenderTerrain.YPos) == weaponRange;
            
            return isInRange;
        }

        private void MoveAttackerWithinRange(BaseUnitController attacker, BaseUnitController defender)
        {
            bool moveUnit = false;

            // Check attacker's weapon range
            int weaponRange = attacker.Unit.WeaponController.WeaponSO.Range;
            List<Vector2> inRangePoints = new List<Vector2>();
            BaseTerrain attackerTerrain = attacker.Unit.TerrainController.Terrain;
            BaseTerrain defenderTerrain = defender.Unit.TerrainController.Terrain;

            // NOTE: PERLU ADA DIAGONAL
            // Left side
            inRangePoints.Add(new Vector2(defenderTerrain.XPos - weaponRange, defenderTerrain.YPos));
            // Up side
            inRangePoints.Add(new Vector2(defenderTerrain.XPos, defenderTerrain.YPos + weaponRange));
            // Right side
            inRangePoints.Add(new Vector2(defenderTerrain.XPos + weaponRange, defenderTerrain.YPos));
            // Down side
            inRangePoints.Add(new Vector2(defenderTerrain.XPos, defenderTerrain.YPos - weaponRange));

            inRangePoints = CheckTerrainPointsAvailability(inRangePoints);
            inRangePoints = CheckForNearestTerrain(attackerTerrain, inRangePoints);
            inRangePoints = inRangePoints.Distinct().ToList();

            if(inRangePoints.Count == 0)
            {
                Debug.LogError("Attacker is not within range");
                return;
            }

            foreach(Vector2 inRangePoint in inRangePoints)
            {
                BaseTerrainController terrain = TerrainPoolController.Instance.TerrainPool.Find(t => t.Terrain.XPos == inRangePoint.x && t.Terrain.YPos == inRangePoint.y);
                
                if (terrain != null && terrain.Terrain.CanBeUsed)
                {
                    moveUnit = true;
                    Messenger.Default.Publish(new MoveUnitIntoAttackPointMessage(attacker, terrain));
                    break;
                }
            }

            if (!moveUnit)
            {
                string error = "Unit doesn't move. Points: ";
                foreach(Vector2 inRangePoint in inRangePoints)
                {
                    error += inRangePoint.ToString();
                }

                Debug.LogError(error);
            }
        }

        private List<Vector2> CheckTerrainPointsAvailability(List<Vector2> inRangePoints)
        {
            List<Vector2> newInRangePoints = new List<Vector2>(inRangePoints);

            foreach(Vector2 inRangePoint in inRangePoints)
            {
                BaseTerrainController terrain = TerrainPoolController.Instance.TerrainPool.Find(t => t.Terrain.XPos == inRangePoint.x && t.Terrain.YPos == inRangePoint.y);

                if (terrain != null)
                    newInRangePoints.Add(inRangePoint);
            }

            return newInRangePoints;
        }

        private List<Vector2> CheckForNearestTerrain(BaseTerrain attackerTerrain, List<Vector2> inRangePoints)
        {
            List<Vector2> newInRangePoints = new List<Vector2>(inRangePoints);
            List<int> lengthList = new List<int>();

            // Check the length between point and attacker position
            foreach(Vector2 inRangePoint in inRangePoints)
            {
                int currentLength = (int)(Mathf.Abs(attackerTerrain.XPos - inRangePoint.x) + 
                    Mathf.Abs(attackerTerrain.YPos - inRangePoint.y));

                lengthList.Add(currentLength);
            }

            // Get the lowest and record
            int lowestLength = lengthList.Min();
            for(int i = 0; i< lengthList.Count; i++)
            {
                if (lengthList[i] == lowestLength)
                    newInRangePoints.Add(inRangePoints[i]);
            }

            return newInRangePoints;
        }

        private IEnumerator StartBattle(BaseUnitController attacker, BaseUnitController defender)
        {
            Messenger.Default.Publish(new ChangeStageInPhaseMessage(InPhaseEnum.OnBattle));
            switch (StageController.Instance.Stage.Phase)
            {
                case StagePhase.PlayerPhase:
                    Messenger.Default.Publish(new OnBattleBeginMessage(attacker, defender));
                    break;
                case StagePhase.EnemyPhase:
                    Messenger.Default.Publish(new OnBattleBeginMessage(defender, attacker));
                    break;
            }

            yield return new WaitForSeconds(1f);
            float damageAmount = BattleBegin(attacker, defender);

            Messenger.Default.Publish(new DecreaseHPMessage(defender, damageAmount));

            yield return new WaitForSeconds(5f);
            Messenger.Default.Publish(new OnBattleFinishMessage(attacker));
            Messenger.Default.Publish(new ChangeStageInPhaseMessage(InPhaseEnum.Idle));
        }

        private float BattleBegin(BaseUnitController attacker, BaseUnitController defender)
        {
            WeaponType weaponType = attacker.Unit.WeaponController.WeaponSO.Type;
            float baseAttack = attacker.Unit.UnitStats.BaseATK;
            float attackerLuk = attacker.Unit.UnitStats.BaseLUK;
            float defensiveAmount = 0f;

            switch(weaponType)
            {
                case WeaponType.Physical:
                    defensiveAmount = defender.Unit.UnitStats.BaseDEF;
                    break;

                case WeaponType.Magical:
                    defensiveAmount = defender.Unit.UnitStats.BaseRES;
                    break;

                default: break;
            }

            float damageBonus = CalculateDamageBonus(attacker, defender);
            int randomNumber = CustomRandomizer.GetRandomValue(
                new RandomSelection(0, 0, 1 - attackerLuk),
                new RandomSelection(1, 1, attackerLuk)
            );

            float damageAmount = 0;
            if(randomNumber == 1)
            {
                damageAmount = baseAttack + damageBonus - defensiveAmount;
            }

            return Mathf.Round(damageAmount);
        }

        private float CalculateDamageBonus(BaseUnitController attacker, BaseUnitController defender)
        {
            float damageBonus = 0;
            float baseAttack = attacker.Unit.UnitStats.BaseATK;

            // Bow > Flier
            if(attacker.Unit.WeaponController.WeaponSO.DamageRange == WeaponDamageRange.LongRange &&
                defender.GetType() == typeof(FlierUnitController))
            {
                damageBonus += 0.2f * baseAttack;
            }

            // Affinity
            UnitAffinity attackerAffinity = attacker.Unit.BaseUnitSO.Affinity;
            UnitAffinity defenderAffinity = defender.Unit.BaseUnitSO.Affinity;

            if(attackerAffinity != defenderAffinity)
            {
                switch (attackerAffinity)
                {
                    case UnitAffinity.Red:
                        switch (defenderAffinity)
                        {
                            case UnitAffinity.Green:
                                damageBonus += 0.2f * baseAttack;
                                break;

                            case UnitAffinity.Blue:
                                damageBonus -= 0.2f * baseAttack;
                                break;

                            default: break;
                        }
                        break;

                    case UnitAffinity.Green:
                        switch (defenderAffinity)
                        {
                            case UnitAffinity.Red:
                                damageBonus -= 0.2f * baseAttack;
                                break;

                            case UnitAffinity.Blue:
                                damageBonus += 0.2f * baseAttack;
                                break;

                            default: break;
                        }
                        break;

                    case UnitAffinity.Blue:
                        switch (defenderAffinity)
                        {
                            case UnitAffinity.Red:
                                damageBonus += 0.2f * baseAttack;
                                break;

                            case UnitAffinity.Green:
                                damageBonus -= 0.2f * baseAttack;
                                break;

                            default: break;
                        }
                        break;

                    default: break;
                }
            }

            return damageBonus;
        }
    }
}
