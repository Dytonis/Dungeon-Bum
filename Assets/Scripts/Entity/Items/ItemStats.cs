using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Entity.Items
{
    [System.Serializable]
    public class ItemStats
    {
        public float BaseAttackSpeed;
        public float AttackSpeed;
        public float BaseDamage;
        public float Damage;
        public float BaseRange;
        public float Range;
        public float BaseProjectileSpeed;
        public float ProjectileSpeed;
        public List<ProjectileEffect> ProjectileEffects;
        public int BaseItemLevel;
        public int ItemLevel;

        public float Scale;

        private static float Round(float f)
        {
            return Mathf.Round(f * 100) / 100;
        }

        public static ItemStats ApplyStats(ItemStats to, ItemStats from)
        {
            ItemStats returns = new ItemStats()
            {
                AttackSpeed = Round(to.AttackSpeed + (to.AttackSpeed * from.AttackSpeed)),
                Damage = Round(to.Damage + (to.Damage * from.Damage)),
                ProjectileSpeed = Round(to.ProjectileSpeed + (to.ProjectileSpeed * from.ProjectileSpeed)),
                Range = Round(to.Range + (to.Range * from.Range)),
                Scale = Round(to.Scale + (to.Scale * from.Scale)),
                ItemLevel = to.ItemLevel + from.ItemLevel,
                BaseAttackSpeed = Round(to.BaseAttackSpeed + from.BaseAttackSpeed),
                BaseDamage = Round(to.BaseDamage + from.BaseDamage),
                BaseItemLevel = to.BaseItemLevel + from.BaseItemLevel,
                BaseProjectileSpeed = Round(to.BaseProjectileSpeed + from.BaseProjectileSpeed),
                BaseRange = Round(to.BaseRange + from.BaseRange)
            };

            returns.ProjectileEffects = new List<ProjectileEffect>();

            if (to.ProjectileEffects != null)
            {
                foreach (ProjectileEffect e in to.ProjectileEffects)
                {
                    returns.ProjectileEffects.Add(e);
                }
            }
            if (from.ProjectileEffects != null)
            {
                foreach (ProjectileEffect e in from.ProjectileEffects)
                {
                    returns.ProjectileEffects.Add(e);
                }
            }

            return returns;
        }

        public static ItemStats operator +(ItemStats left, ItemStats right)
        {
            if(right == null)
            {
                if (left != null)
                    return left;
                else return new ItemStats();
            }
            else if (left == null)
            {
                return right;
            }

            ItemStats returns = new ItemStats()
            {
                AttackSpeed = left.AttackSpeed + right.AttackSpeed,
                Damage = left.Damage + right.Damage,
                ProjectileSpeed = left.ProjectileSpeed + right.ProjectileSpeed,
                Range = left.Range + right.Range,
                Scale = left.Scale + right.Scale,
                ItemLevel = left.ItemLevel + right.ItemLevel,
                BaseAttackSpeed = left.BaseAttackSpeed + right.BaseAttackSpeed,
                BaseDamage = left.BaseDamage + right.BaseDamage,
                BaseItemLevel = left.BaseItemLevel + right.BaseItemLevel,
                BaseProjectileSpeed = left.BaseProjectileSpeed + right.BaseProjectileSpeed,
                BaseRange = left.BaseRange + right.BaseRange
            };

            returns.ProjectileEffects = new List<ProjectileEffect>();

            if (left.ProjectileEffects != null)
            {
                foreach (ProjectileEffect e in left.ProjectileEffects)
                {
                    returns.ProjectileEffects.Add(e);
                }
            }
            if (right.ProjectileEffects != null)
            {
                foreach (ProjectileEffect e in right.ProjectileEffects)
                {
                    returns.ProjectileEffects.Add(e);
                }
            }

            return returns;
        }
    }
}
