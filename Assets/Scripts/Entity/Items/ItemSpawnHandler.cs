using UnityEngine;
using System.Collections;
using System.Linq;

namespace Assets.Scripts.Entity.Items
{
    [System.Serializable]
    public struct ItemSpawnHandler
    {
        [Range(1, ushort.MaxValue)]
        public ushort RarityWeight;

        public string ItemPathRepresenting;

        public static ItemSpawnHandler ChooseFromWeight(ItemSpawnHandler[] from)
        {
            int total = 0;
            ItemSpawnHandler[] ordered = from.OrderBy(x => x.RarityWeight).ToArray();

            foreach(ItemSpawnHandler i in ordered)
            {
                total += i.RarityWeight;
            }

            //debug loop
            foreach(ItemSpawnHandler i in ordered)
            {
                Debug.Log("Item " + i.ItemPathRepresenting + " relative chance is (" + Round(1f/((float)total / (float)i.RarityWeight)*100) + "%)");
            }

            int r = Random.Range(1, total);
            int run = 0;

            for(int i = 0; i < ordered.Length; i++) //we can assume i+1 is always greater than i
            {
                if(i == 0)
                {
                    run = ordered[i].RarityWeight;
                    if(r <= ordered[i].RarityWeight)
                    {
                        return ordered[i];
                    }
                }
                else
                {
                    int rangeLow = run + 1;
                    int rangeHigh = rangeLow + ordered[i].RarityWeight - 1;
                    run = rangeHigh;
                    if(r >= rangeLow && r <= rangeHigh)
                    {
                        return ordered[i];
                    }
                }
            }

            return new ItemSpawnHandler();
        }

        private static float Round(float f)
        {
            return Mathf.Round(f * 1000) / 1000;
        }
    }
}
