using HarmonyLib;
using ResoniteModLoader;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using FrooxEngine;

namespace FixGrabWorld
{
    public class FixGrabWorld : ResoniteMod
    {
        public override string Name => "FixGrabWorld";
        public override string Author => "art0007i";
        public override string Version => "2.0.0";
        public override string Link => "https://github.com/art0007i/FixGrabWorld/";
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.art0007i.FixGrabWorld");
            harmony.PatchAll();

        }
        [HarmonyPatch(typeof(GrabWorldLocomotion), "TryActivate")]
        class FixGrabWorldPatch
        {
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codes)
            {
                var lookfor = typeof(ILocomotionReference).GetMethod("get_DirectionReference");
                foreach(var code in codes)
                {
                    if(code.operand is MethodInfo mi && mi == lookfor)
                    {
                        code.operand = typeof(ILocomotionReference).GetMethod("get_GripReference");
                    }
                    yield return code;
                }
            }
        }
    }
}