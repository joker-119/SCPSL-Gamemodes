using Exiled.API.Interfaces;
using System.ComponentModel;

namespace VIP
{
	public class Config : IConfig
	{
		[Description("Whether or not this plugin is enabled.")]
		public bool IsEnabled { get; set; } = true;

		[Description("The maximum duration of the round in seconds.")]
		public float MaxDuration { get; set; } = 600f;

		[Description("Whether or not to disable item pickup for everyone.")]
		public bool DisableItemPickup { get; set; } = false;

		[Description("Whether or not to disable item pickup for the VIP. Disregarded if no one can pickup items.")]
		public bool DisableItemPickupVip { get; set; } = true;

		[Description("The broadcast sent to the VIP.")]
		public string VipBroadcast { get; set; } = "You are the VIP. Avoid Chaos Insurgency and escape.";

		[Description("The broadcast sent to the guards.")]
		public string GuardBroadcast { get; set; } = "You are a guard. Kill Chaos Insurgency, escort the VIP to escape.";

		[Description("The broadcast sent to the attackers.")]
		public string AttackerBroadcast { get; set; } = "You are an attacker. Kill the VIP using any means.";

		[Description("The VIP's starting health.")]
		public int VipStartingHealth { get; set; } = 300;

		[Description("The VIP's starting armor.")]
		public int VipStartingArmor { get; set; } = 250;

		[Description("Whether or not the VIP's armor decays.")]
		public bool VipArmorDecay { get; set; } = false;

		[Description("The decay rate of armor in armor/sec. Disregarded if the armor doesn't decay.")]
		public float VipArmorDecayRate { get; set; } = 0.75f;

		[Description("The percentage of players that are guards.")]
		public float PercentOfGuards { get; set; } = 30;

		[Description("Whether or not guards respawn on death.")]
		public bool GuardsRespawn { get; set; } = true;

		[Description("The time it takes for guards to respawn. Disregarded if guard respawn is disabled.")]
		public float GuardRespawnDelay { get; set; } = 10f;

		[Description("Whether or not attackers respawn on death.")]
		public bool AttackersRespawn { get; set; } = true;

		[Description("The time it takes for attackers to respawn. Disregarded if attacker respawn is disabled.")]
		public float AttackerRespawnDelay { get; set; } = 5f;
	}
}
