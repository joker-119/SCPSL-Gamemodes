using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace Juggernaut
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool InfiniteJuggGrenades { get; set; } = true;
        public bool CommandersGetMicro { get; set; } = true;
        public int JuggernautHealth { get; set; } = 10000;
        public List<string> JuggernautInventory { get; set; } = new List<string>();

        public List<ItemType> JuggernautInv  = new List<ItemType>();

        internal void ParseInventory()
        {
            foreach (string s in JuggernautInventory)
            {
                ItemType item = ItemType.None;
                try
                {
                    item = (ItemType) Enum.Parse(typeof(ItemType), s, true);
                }
                catch (Exception)
                {
                    Log.Warn($"Unable to parse {s} into an item.");
                }
                
                if (item != ItemType.None)
                    JuggernautInv.Add(item);
            }
        }
    }
}