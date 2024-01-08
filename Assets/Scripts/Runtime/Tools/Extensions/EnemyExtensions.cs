using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RunGun.Gameplay
{
    public static class EnemyExtensions
    {
        private static readonly Collider[] _results = new Collider[50];

        public static List<GameObject> GetObjectsInAttackField(this List<GameObject> gameObjects, Transform characterTransform, float attackAngle)
        {
            var objectsInAttackField = new List<GameObject>();
            
            foreach (GameObject gameObject in gameObjects)
            {
                Vector3 dirToTarget = (gameObject.transform.position - characterTransform.position).normalized;
                if (Vector3.Angle(characterTransform.forward, dirToTarget) < attackAngle)
                {
                }
            }

            objectsInAttackField.RemoveDiedEnemies();
            return objectsInAttackField;
        }

        public static List<GameObject> FindObjectsNear(this Transform characterTransform, float damageDistance)
        {
            var size = Physics.OverlapSphereNonAlloc(characterTransform.position, damageDistance, _results);
            var gameObjects = new List<GameObject>();

            for (int i = 0; i < size; i++)
            {
            }

            gameObjects.RemoveDiedEnemies();
            return gameObjects;
        }

        public static void LookAtTarget(this Transform characterTransform, GameObject closestEnemy)
        {
            Vector3 rotCharacter = new Vector3(closestEnemy.transform.position.x - characterTransform.position.x, 0, closestEnemy.transform.position.z - characterTransform.position.z);
            characterTransform.rotation = Quaternion.LookRotation(rotCharacter.normalized);
        }
    }
}