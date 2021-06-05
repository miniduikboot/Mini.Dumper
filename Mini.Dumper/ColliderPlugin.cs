using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Extensions;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using Mini.Dumper.Dumpers;
using Newtonsoft.Json;

namespace Mini.Dumper
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class ColliderPlugin : BasePlugin
    {
        public const string Id = "at.duikbo.dumper";

        public Harmony Harmony { get; } = new Harmony(Id);

        public ConfigEntry<string> Path { get; private set; }

        public TriggerComponent Component { get; private set; }

        public override void Load()
        {
            RegisterInIl2CppAttribute.Register();

            var gameObj = new GameObject(nameof(ColliderPlugin)).DontDestroy();
            Component = gameObj.AddComponent<TriggerComponent>();
            //Name = Config.Bind("Fake", "Name", ":>");

            //            Harmony.PatchAll();
        }

        public class Dump
        {
            public ColliderDumper colliders = new ColliderDumper();
            public PlayerDumper players = new PlayerDumper();
            public TaskDumper tasks = new TaskDumper();
        }


        [RegisterInIl2Cpp]
        public class TriggerComponent : MonoBehaviour
        {
            public TriggerComponent(IntPtr ptr) : base(ptr)
            {
            }

            private void Update()
            {
                if (Input.GetKeyDown(KeyCode.F1))
                {
                    Debug.Log("Starting collider dump");
                    // start collision dumper
                    var dump = Dump();
                    using (StreamWriter sw = File.CreateText(@"C:\colliders.json"))
                    {
                        sw.WriteLine(dump);
                    }
                    Debug.Log($"dump done, {dump.Length} bytes");
                }
            }

            private string Dump()
            {
                var dump = new Dump();
                // dump.colliders.Dump(ShipStatus.Instance);
                // dump.players.Dump(GameData.Instance);
                dump.tasks.Dump(ShipStatus.Instance);

                return JsonConvert.SerializeObject(dump, Formatting.Indented);
            }
        }
    }
}
