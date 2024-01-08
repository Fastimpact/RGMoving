using System;
using System.Linq;
using System.Collections.Generic;
using static Unity.VisualScripting.Member;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace RunGun.Gameplay
{
    public static class EnumerableExtensions
    {
        static Random rnd = new Random();

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action?.Invoke(item);
            }
        }

        public static T PickRandom<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(enemy => rnd.Next()).FirstOrDefault();
        }

        public static T[] ToArray<T>(this T element)
        {
            return new T[] { element };
        }

        public static void RemoveDiedEnemies(this List<GameObject> gameObjects)
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {

            }
        }
    }
}