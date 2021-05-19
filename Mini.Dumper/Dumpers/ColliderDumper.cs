using System.Collections.Generic;
using UnityEngine;
using Mini.Dumper.Extensions;

namespace Mini.Dumper.Dumpers
{
    public class ColliderDumper
    {
        public Dictionary<int, Layer> layers = new Dictionary<int, Layer>();

        public class Layer
        {
            public string name;
            public List<Collider> colliders = new List<Collider>();

            public Layer(string name)
            {
                this.name = name;
            }
        }

        public class Collider
        {
            public string name;
            public string path;

            public Collider(string name, string path)
            {
                this.name = name;
                this.path = path;
            }
        }

        private void AddCollider(Collider2D collider, string path)
        {
            var layer = collider.gameObject.layer;
            if (!layers.ContainsKey(layer))
            {
                layers.Add(layer, new Layer(LayerMask.LayerToName(layer)));
            }
            var parsedCollider = new Collider(collider.GetName(), path);
            layers[layer].colliders.Add(parsedCollider);
        }

        public void Dump(ShipStatus ship)
        {
            var edgeColliders = ship.GetComponentsInChildren<EdgeCollider2D>();
            foreach (var collider in edgeColliders)
            {
                var svg = collider.ToSvg();
                AddCollider(collider, svg);
            }
            var boxColliders = ship.GetComponentsInChildren<BoxCollider2D>();
            foreach (var collider in boxColliders)
            {
                var svg = collider.ToSvg();
                AddCollider(collider, svg);
            }
            var polygonColliders = ship.GetComponentsInChildren<PolygonCollider2D>();
            foreach (var collider in polygonColliders)
            {
                var svg = collider.ToSvg();
                AddCollider(collider, svg);
            }
            var circleColliders = ship.GetComponentsInChildren<CircleCollider2D>();
            foreach (var collider in circleColliders)
            {
                var svg = collider.ToSvg();
                AddCollider(collider, svg);
            }

        }
    }
}
