using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

namespace Assets.Scripts.Entity.Items
{
    public class ItemPSTables
    {
        /// <summary>
        /// Gets the descriptor attribute via reflection
        /// </summary>
        /// <param name="en">the enum to retrieve</param>
        /// <returns></returns>
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(Description), false);

                if (attrs != null && attrs.Length > 0)
                    return ((Description)attrs[0]).Text;
            }
            return en.ToString();
        }

        public static ItemPrefix GetMeleePrefix()
        {
            int pick = UnityEngine.Random.Range(0, ItemMeleePrefixes.Length);
            return ItemMeleePrefixes[pick];
        }

        public static ItemEquippableTypes DefinitiveTypeToEquippableType(ItemDefiniteTypes t)
        {
            switch(t)
            {
                case ItemDefiniteTypes.ChainArmorChest: return ItemEquippableTypes.Chest;
                case ItemDefiniteTypes.ChainArmorPants: return ItemEquippableTypes.Pants;
                case ItemDefiniteTypes.ChainArmorShoes: return ItemEquippableTypes.Shoes;
                case ItemDefiniteTypes.ClothArmorChest: return ItemEquippableTypes.Chest;
                case ItemDefiniteTypes.ClothArmorPants: return ItemEquippableTypes.Pants;
                case ItemDefiniteTypes.ClothArmorShoes: return ItemEquippableTypes.Shoes;
                case ItemDefiniteTypes.Hat: return ItemEquippableTypes.Hat;
                case ItemDefiniteTypes.OffhandBlunt: return ItemEquippableTypes.Offhand;
                case ItemDefiniteTypes.OffhandMagical: return ItemEquippableTypes.Offhand;
                case ItemDefiniteTypes.OffhandSharp: return ItemEquippableTypes.Offhand;
                case ItemDefiniteTypes.OneHandedBlunt: return ItemEquippableTypes.OneHanded;
                case ItemDefiniteTypes.OneHandedSharp: return ItemEquippableTypes.OneHanded;
                case ItemDefiniteTypes.OneHandedMagical: return ItemEquippableTypes.OneHanded;
                case ItemDefiniteTypes.TwoHandedBlunt: return ItemEquippableTypes.TwoHanded;
                case ItemDefiniteTypes.TwoHandedSharp: return ItemEquippableTypes.TwoHanded;
                case ItemDefiniteTypes.TwoHandedMagical: return ItemEquippableTypes.TwoHanded;
                case ItemDefiniteTypes.PlateArmorChest: return ItemEquippableTypes.Chest;
                case ItemDefiniteTypes.PlateArmorPants: return ItemEquippableTypes.Pants;
                case ItemDefiniteTypes.PlateArmorShoes: return ItemEquippableTypes.Shoes;
                case ItemDefiniteTypes.RangedConsumable: return ItemEquippableTypes.Ranged;
                case ItemDefiniteTypes.RangedUnconsumable: return ItemEquippableTypes.Ranged;
                case ItemDefiniteTypes.SpellBook: return ItemEquippableTypes.Spellbook;
                case ItemDefiniteTypes.Wand: return ItemEquippableTypes.Wand;
            }

            return ItemEquippableTypes.Offhand;
        }

        public static ItemPrefix[] ItemMeleePrefixes = new ItemPrefix[]
        {
            new ItemPrefix()
            {
                Name = "Intelligent",
                ActorChanges = new ActorStats()
                {
                    Mana = 0.07f
                },
                StatChanges = new ItemStats()
                {
                    ItemLevel = 1
                }
            },
            new ItemPrefix()
            {
                Name = "Long",
                StatChanges = new ItemStats()
                {
                    Damage = 0.4f,
                    ItemLevel = 1
                }
            },
            new ItemPrefix()
            {
                Name = "Broken",
                StatChanges = new ItemStats()
                {
                    Damage = -0.3f,
                    AttackSpeed = -0.4f,
                    ItemLevel = -3
                }
            },
            new ItemPrefix()
            {
                Name = "Dull",
                StatChanges = new ItemStats()
                {
                    Damage = -0.2f,
                    ItemLevel = -1
                }
            },
            new ItemPrefix()
            {
                Name = "Slow",
                StatChanges = new ItemStats()
                {
                    AttackSpeed = -0.3f,
                    ItemLevel = -1
                }
            },
            new ItemPrefix()
            {
                Name = "Pointy",
                StatChanges = new ItemStats()
                {
                    Damage = 0.13f,
                    ItemLevel = 1
                }
            },
            new ItemPrefix()
            {
                Name = "Agile",
                StatChanges = new ItemStats()
                {
                    AttackSpeed = 0.3f,
                    ItemLevel = 1
                }
            },
            new ItemPrefix()
            {
                Name = "Wild",
                StatChanges = new ItemStats()
                {
                    AttackSpeed = 0.5f,
                    ItemLevel = 2
                }
            },
            new ItemPrefix()
            {
                Name = "Infested",
                StatChanges = new ItemStats()
                {
                    ItemLevel = 3,
                    ProjectileEffects = new System.Collections.Generic.List<ProjectileEffect>()
                    {
                        new ProjectileEffectInfested()
                    }
                }
            }
        };
    }

    public struct ItemPrefix
    {
        public string Name;
        public ItemStats StatChanges;
        public ActorStats ActorChanges;
    }

    public struct ItemSuffix
    {

    }

    public enum ItemRarities
    {
        Junk,
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public enum ItemDefiniteTypes
    {
        OneHandedSharp,
        OneHandedBlunt,
        OneHandedMagical,
        TwoHandedSharp,
        TwoHandedBlunt,
        TwoHandedMagical,
        OffhandSharp,
        OffhandBlunt,
        OffhandMagical,
        RangedConsumable,
        RangedUnconsumable,
        SpellBook,
        Wand,
        ClothArmorChest,
        ClothArmorPants,
        ClothArmorShoes,
        ChainArmorChest,
        ChainArmorPants,
        ChainArmorShoes,
        PlateArmorChest,
        PlateArmorPants,
        PlateArmorShoes,
        Hat
    }

    public enum ItemEquippableTypes
    {
        [Description("One Handed")]
        OneHanded,
        [Description("Two Handed")]
        TwoHanded,
        [Description("Offhand")]
        Offhand,
        [Description("Ranged")]
        Ranged,
        [Description("Spellbook")]
        Spellbook,
        [Description("Wand")]
        Wand,
        [Description("Chest")]
        Chest,
        [Description("Pants")]
        Pants,
        [Description("Shoes")]
        Shoes,
        [Description("Hat")]
        Hat
    }

    public enum InventorySlots : byte
    {
        One = 0,
        Two = 1,
        Three = 2,
        Four = 3,
        Five = 4,
        Six = 5,
        Seven = 6,
        Eight = 7,
        Nine = 8,
        Ten = 9,
        Eleven = 10,
        Twelve = 11,
        Thirteen = 12,
        Fourteen = 13,
        Weapon = 14,
        Offhand = 15,
        Ranged = 16,
        Head = 17,
        Chest = 18,
        Pants = 19,
        Shoes = 20
    }

    class Description : Attribute
    {
        public string Text;

        public Description(string text)    
        {
            Text = text;
        }
    }
}
