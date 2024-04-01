using Aki.Reflection.Patching;
using EFT;
using MPT.Core.Coop.Matchmaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SSH_Artillery
{
    public class HookThrowablePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameWorld).GetMethod("RegisterGrenade", BindingFlags.Instance | BindingFlags.Public);
        }

        [PatchPostfix]
        private static void PatchPostfix(Throwable grenade)
        {
            // Check to see if we are the server in order to determine if the script should be spawned or not. You only want it on the server as to not have artillery spawn twice.
            if (MatchmakerAcceptPatches.IsServer)
            {
                if (grenade is SmokeGrenade serverSmokeGrenade)
                {
                    GrenadeSpawner spawner = grenade.gameObject.AddComponent<GrenadeSpawner>();
                    spawner.Owner = serverSmokeGrenade.ProfileId;
                }
            }
        }
    }
}
