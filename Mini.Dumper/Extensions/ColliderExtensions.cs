using UnityEngine;
using System;
using System.Text;

namespace Mini.Dumper.Extensions
{
    public static class ColliderExtensions
    {

        static int offset = 0;

        static void DumpPoints(StringBuilder sb, Vector2[] points, Transform transform)
        {
            sb.Append("M");
            foreach (var point in points)
            {
                var p = transform.TransformPoint(point);
                sb.Append(' ');
                sb.Append(p.x + offset);
                sb.Append(',');
                sb.Append(p.y + offset);
            }
        }

        /*public static string ToSvg(this Collider2D col)
        {
            / *switch (col)
            {
                case EdgeCollider2D eCol:
                    return eCol.ToSvg();
                case BoxCollider2D bCol:
                    return bCol.ToSvg();
                case PolygonCollider2D pCol:
                    return pCol.ToSvg();
                    }* /
            if (col is EdgeCollider2D eCol)
            {
                return eCol.ToSvg();
            }
            else if (col is PolygonCollider2D pCol)
            {
                return pCol.ToSvg();
            }
            else if (col is BoxCollider2D bCol)
            {
                return bCol.ToSvg();
            }
            Debug.Log($"Collider of type {col.GetType()} not handled");
            return "";
            }*/

        public static string GetName(this Collider2D col)
        {
            var sb = new StringBuilder();
            col.transform.BuildName(sb);
            return sb.ToString();
        }

        static void BuildName(this Transform tf, StringBuilder sb)
        {
            if (tf.parent != null)
            {
                tf.parent.BuildName(sb);
                sb.Append('/');
            }
            sb.Append(tf.name);
        }

        public static string ToSvg(this EdgeCollider2D col)
        {
            var sb = new StringBuilder();
            DumpPoints(sb, col.points, col.transform);
            return sb.ToString();
        }

        public static string ToSvg(this PolygonCollider2D col)
        {
            var sb = new StringBuilder();
            DumpPoints(sb, col.points, col.transform);
            sb.Append(" Z");
            return sb.ToString();
        }

        public static string ToSvg(this BoxCollider2D col)
        {
            var sb = new StringBuilder();
            /*var bounds = col.bounds;
            var points = new Vector2[] {
                new Vector2(bounds.center.x + bounds.extents.x / 2, bounds.center.y + bounds.extents.y / 2),
                new Vector2(bounds.center.x - bounds.extents.x / 2, bounds.center.y + bounds.extents.y / 2),
                new Vector2(bounds.center.x - bounds.extents.x / 2, bounds.center.y - bounds.extents.y / 2),
                new Vector2(bounds.center.x + bounds.extents.x / 2, bounds.center.y - bounds.extents.y / 2),
                };*/
            var points = new Vector2[] {
                new Vector2(col.offset.x + col.size.x / 2, col.offset.y + col.size.y / 2),
                new Vector2(col.offset.x - col.size.x / 2, col.offset.y + col.size.y / 2),
                new Vector2(col.offset.x - col.size.x / 2, col.offset.y - col.size.y / 2),
                new Vector2(col.offset.x + col.size.x / 2, col.offset.y - col.size.y / 2),
                };

            DumpPoints(sb, points, col.transform);
            sb.Append(" Z");
            return sb.ToString();
        }

        public static string ToSvg(this CircleCollider2D col)
        {
            if (col.name == "UpperHandConsole")
            {
                Debug.Log($"Bounds: {col.bounds.ToString()}");
                Debug.Log($"Offset: {col.offset.ToString()}");
                Debug.Log($"TLOffs: {col.transform.TransformPoint(col.offset).ToString()}");
            }
            var center = col.transform.TransformPoint(col.offset);
            center.x += offset;
            center.y += offset;
            var r = col.radius;
            // From https://stackoverflow.com/questions/5737975/circle-drawing-with-svgs-arc-path
            return $"M {center.x} {center.y} m {-r},0 a {r},{r} 0 1 0 {2 * r},0 a {r},{r} 0 1 0 {-2 * r},0";
        }

    }
}
