using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace HideNSeek
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }
        public bool DisableLights { get; set; }
        public int Max939Count { get; set; } = 3;
        public int Max049Count { get; set; } = 1;
        public string EndRoundBroadcast { get; set; } = "%user is the winner!";
        public ushort EndRoundBroadcastDur { get; set; } = 15;
        public List<string> TeslaIgnore { get; set; } = new List<string>
        {
            "allscp"
        };
        
        public List<RoleType> TeslaIgnoreRoles { get; set; } = new List<RoleType>();

        internal void ParseTeslaIgnore()
        {
            foreach (string s in TeslaIgnore)
            {
                switch (s)
                {
                    case "allscp":
                        TeslaIgnoreRoles.Add(RoleType.Scp049);
                        TeslaIgnoreRoles.Add(RoleType.Scp096);
                        TeslaIgnoreRoles.Add(RoleType.Scp0492);
                        TeslaIgnoreRoles.Add(RoleType.Scp106);
                        TeslaIgnoreRoles.Add(RoleType.Scp173);
                        TeslaIgnoreRoles.Add(RoleType.Scp93953);
                        TeslaIgnoreRoles.Add(RoleType.Scp93989);
                        break;
                    case "allhuman":
                        TeslaIgnoreRoles.Add(RoleType.ClassD);
                        TeslaIgnoreRoles.Add(RoleType.ChaosInsurgency);
                        TeslaIgnoreRoles.Add(RoleType.Scientist);
                        TeslaIgnoreRoles.Add(RoleType.FacilityGuard);
                        TeslaIgnoreRoles.Add(RoleType.NtfCadet);
                        TeslaIgnoreRoles.Add(RoleType.NtfLieutenant);
                        TeslaIgnoreRoles.Add(RoleType.NtfCommander);
                        TeslaIgnoreRoles.Add(RoleType.NtfScientist);
                        break;
                    default:
                        RoleType role = RoleType.None;
                        try
                        {
                            role = (RoleType) Enum.Parse(typeof(RoleType), s, true);
                        }
                        catch (Exception e)
                        {
                            Log.Warn($"Unable to parse {s}, not a valid role.");
                            Log.Debug($"{e}\n{e.StackTrace}", Debug);
                        }

                        if (role == RoleType.None)
                            continue;
                        TeslaIgnoreRoles.Add(role);
                        break;
                }
            }
        }
    }
}