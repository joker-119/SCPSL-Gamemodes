using System;
using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace TeamDeathmatch
{
    public class Config : IConfig
    {
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; }
        
        public List<string> AdditionalItems { get; set; } = new List<string>();
        
        public List<ItemType> AdditionalItemList = new List<ItemType>();

        internal void ParseItems()
        {
            foreach (string s in AdditionalItems)
            {
                ItemType type = ItemType.None;

                try
                {
                    type = (ItemType) Enum.Parse(typeof(ItemType), s, true);
                }
                catch (Exception)
                {
                    Log.Error($"Unable to parse {s} into an item type.");
                }
                
                if (type != ItemType.None)
                    AdditionalItemList.Add(type);
            }
        }
    }
}