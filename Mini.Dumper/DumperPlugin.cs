﻿using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Reactor;
using Reactor.Extensions;
using UnityEngine;
using Utf8Json;
using System;
using System.Text;
using System.IO;
using Mini.Dumper.Dumpers;


namespace Mini.Dumper
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public class DumperPlugin : BasePlugin
    {
        public const string Id = "at.duikbo.dumper";

        public Harmony Harmony { get; } = new Harmony(Id);

        public TriggerComponent Component { get; private set; }

        public override void Load()
        {
            var gameObj = new GameObject(nameof(DumperPlugin)).DontDestroy();
            Component = gameObj.AddComponent<TriggerComponent>();
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
                    var dump = new Dump();
                    dump.colliders.Dump(ShipStatus.Instance);
                    dump.players.Dump(GameData.Instance);
                    dump.tasks.Dump(ShipStatus.Instance);

                    var dumpstr = JsonSerializer.ToJsonString(dump);

                    StringBuilder outputFile = new StringBuilder();

                    var dumpsDir = Path.Combine(BepInEx.Paths.GameRootPath, "dumps");
                    if (!Directory.Exists(dumpsDir))
                    {
                        Directory.CreateDirectory(dumpsDir);
                    }

                    var mapName = ShipStatus.Instance.GetIl2CppType().Name;
                    var outputPath = Path.Combine(dumpsDir, $"{mapName}.json");

                    using (StreamWriter sw = File.CreateText(outputPath))
                    {
                        sw.WriteLine(dumpstr);
                    }
                    Debug.Log($"dump done, {dumpstr.Length} bytes");
                }
            }
        }
    }
}
