using Exiled.API.Interfaces;
using System.ComponentModel;

namespace VIP
{
	public class Config : IConfig
	{
		public bool IsEnabled { get; set; } = true;
		public float MaxDuration { get; set; } = 600f;
		public bool DisableItemPickup { get; set; } = false;
		public bool DisableItemPickupVip { get; set; } = true;
		public string VipBroadcast { get; set; } = "You are the VIP. Avoid Chaos Insurgency and escape.";
		public string GuardBroadcast { get; set; } = "You are a guard. Kill Chaos Insurgency, escort the VIP to escape.";
		public string AttackerBroadcast { get; set; } = "You are an attacker. Kill the VIP using any means.";
		public int VipStartingHealth { get; set; } = 300;
		public int VipStartingArmor { get; set; } = 250;
		public bool VipArmorDecay { get; set; } = false;
		public float VipArmorDecayRate { get; set; } = 0.75f;
		public float PercentOfGuards { get; set; } = 30;
		public bool GuardsRespawn { get; set; } = true;
		public float GuardRespawnDelay { get; set; } = 10f;
		public bool AttackersRespawn { get; set; } = true;
		public float AttackerRespawnDelay { get; set; } = 5f;
	}
}
