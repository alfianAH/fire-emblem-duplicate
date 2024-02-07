using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Terrain.Pool;
using FireEmblemDuplicate.Scene.Battle.Unit;
using SuperMaxim.Messaging;
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
                Debug.Log("Yok gelud");
            }
            else
            {
                // Publish message to move
                MoveAttackerWithinRange(message.Attacker, message.Defender);
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
            // Check attacker's weapon range
            int weaponRange = attacker.Unit.WeaponController.WeaponSO.Range;
            List<Vector2> inRangePoints = new List<Vector2>();
            BaseTerrain attackerTerrain = attacker.Unit.TerrainController.Terrain;
            BaseTerrain defenderTerrain = defender.Unit.TerrainController.Terrain;

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

            if(inRangePoints.Count == 0)
            {
                Debug.LogError("Attacker is not within range");
                return;
            }

            BaseTerrainController terrain = TerrainPoolController.Instance.TerrainPool.Find(t => t.Terrain.XPos == inRangePoints[0].x && t.Terrain.YPos == inRangePoints[0].y);

            if (terrain == null)
            {
                Debug.LogError("Terrain in range is null");
                return;
            }

            Messenger.Default.Publish(new MoveUnitIntoAttackPointMessage(attacker, terrain));
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
    }
}
