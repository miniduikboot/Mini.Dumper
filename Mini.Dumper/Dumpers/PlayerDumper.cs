using System.Collections.Generic;
using UnityEngine;
using Mini.Dumper.Extensions;

namespace Mini.Dumper.Dumpers
{
    public class PlayerDumper
    {
        public Dictionary<int, Player> players = new Dictionary<int, Player>();

        public class Player
        {
            public string name;
            public int color;
            public float positionX;
            public float positionY;

            public Player(string name, int color, Vector2 position)
            {
                this.name = name;
                this.color = color;
                this.positionX = position.x;
                this.positionY = position.y;
            }
        }

        public void Dump(GameData gd)
        {
            foreach (GameData.PlayerInfo player in gd.AllPlayers)
            {
                var id = player.PlayerId;
                var colorId = player.ColorId;
                var name = player.PlayerName;
                var pos = player.Object.MyPhysics.body.position;
                players.Add(id, new Player(name, colorId, pos));
            }
        }
    }
}
