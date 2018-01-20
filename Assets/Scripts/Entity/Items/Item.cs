using UnityEngine;
using System.Collections;
using Assets.Scripts.Entity.CollisionDetection;
using Assets.Scripts.Character;

namespace Assets.Scripts.Entity.Items
{
    public class Item : EntityCollider
    {
        [HideInInspector]
        public Actor.ActorController Holder;
        [HideInInspector]
        public ItemRarities Rarity;
        public ItemDefiniteTypes DefinitiveType;
        [HideInInspector]
        public ItemEquippableTypes EquipType;
        public string ImpericalName;
        public string[] PossibleNames;
        [Range(0, 1)]
        public float PrefixChance = 0;
        [Range(0, 1)]
        public float SuffixChance = 0;
        public float ItemLevelMin;
        public float ItemLevelMax;
        public ItemPrefix Prefix;
        public ItemSuffix Suffix;
        public ItemStats Stats;
        public int MaxStacks = 1;
        public int Count = 1;
        [Space]
        public string UIName;
        [Space]
        public int InventoryPosition = 0;

        // Use this for initialization
        new void Start()
        {
            base.Start();

            EquipType = ItemPSTables.DefinitiveTypeToEquippableType(DefinitiveType);

            Stats.ItemLevel = (int)Random.Range(ItemLevelMin, ItemLevelMax+1);
            float effectiveItemLevel = Stats.ItemLevel - ((ItemLevelMin + ItemLevelMax) / 2); //difference from average

            Stats.Damage += Stats.Damage * effectiveItemLevel * 0.05f;
            Stats.AttackSpeed += Stats.AttackSpeed * effectiveItemLevel * 0.002f;

            Stats.Damage = Round(Stats.Damage);
            Stats.AttackSpeed = Round(Stats.AttackSpeed);

            Stats.BaseAttackSpeed = Stats.AttackSpeed;
            Stats.BaseDamage = Stats.Damage;
            Stats.BaseItemLevel = Stats.ItemLevel;
            Stats.BaseProjectileSpeed = Stats.ProjectileSpeed;
            Stats.BaseRange = Stats.Range;

            Prefix = ItemPSTables.GetMeleePrefix();
            if(Prefix.Name != "" && Random.Range(0f, 1f) < PrefixChance)
            {
                UIName = Prefix.Name + " ";
                Stats = ItemStats.ApplyStats(Stats, Prefix.StatChanges);
            }
            UIName += PossibleNames[UnityEngine.Random.Range(0, PossibleNames.Length)];

            if(Stats.ItemLevel <= 0)
            {
                Rarity = ItemRarities.Junk;
            }
            else if (Stats.ItemLevel < 20)
            {
                Rarity = ItemRarities.Common;
            }
            else if (Stats.ItemLevel < 50)
            {
                Rarity = ItemRarities.Uncommon;
            }
            else if (Stats.ItemLevel < 70)
            {
                Rarity = ItemRarities.Rare;
            }
            else if (Stats.ItemLevel < 90)
            {
                Rarity = ItemRarities.Epic;
            }
            else if (Stats.ItemLevel >= 90)
            {
                Rarity = ItemRarities.Legendary;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Color GetTextColor()
        {
            if (Rarity == ItemRarities.Junk) return new Color(0.6f, 0.6f, 0.6f, 1);
            else if (Rarity == ItemRarities.Common) return new Color(1f, 1f, 1f, 1);
            else if (Rarity == ItemRarities.Uncommon) return new Color(0f, 0.95f, 0f, 1);
            else if (Rarity == ItemRarities.Rare) return new Color(0f, 0f, 0.95f, 1);
            else if (Rarity == ItemRarities.Epic) return new Color(0.65f, 0f, 0.85f, 1);
            else if (Rarity == ItemRarities.Legendary) return new Color(0.85f, 0.1f, 0.1f, 1);

            return new Color(1f, 1f, 1f, 1);
        }

        public override void ActivateWith(Actor.ActorController actor)
        {
            if(actor.GetComponent<Inventory>())
            {
                Inventory inv = actor.GetComponent<Inventory>();
                int throws = inv.AddItemIfAble(this);
                if(throws > 0)
                {
                    if(GetComponent<EntityController>())
                    {
                        transform.position = actor.transform.position;
                        GetComponent<EntityController>().Velocity.y = 20;
                    }
                }
                else
                {
                    transform.position = new Vector3(0, 0, 0);
                    gameObject.SetActive(false);
                }
            }
        }
        private float Round(float f)
        {
            return Mathf.Round(f * 100) / 100;
        }
    }
}
