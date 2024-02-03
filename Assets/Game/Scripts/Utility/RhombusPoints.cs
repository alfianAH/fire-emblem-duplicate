using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireEmblemDuplicate.Utility
{
    public static class RhombusPoints
    {
        private static List<Vector2> GenerateRhombusVertices(Vector2 point, int radius)
        {
            List<Vector2> vertices = new List<Vector2>
            {
                // left
                new Vector2(point.x - radius, point.y),
                // up
                new Vector2(point.x, point.y + radius),
                // right
                new Vector2(point.x + radius, point.y),
                // down
                new Vector2(point.x, point.y - radius),
            };

            return vertices;
        }

        private static List<Vector2> GeneratePointsBetween(Vector2 point1, Vector2 point2)
        {
            List<Vector2> pointsBetween = new List<Vector2>();

            Vector2 direction = new Vector2(
                Mathf.Abs(point2.x - point1.x),
                Mathf.Abs(point2.y - point1.y)
            );
            int stepX = point1.x < point2.x ? 1 : -1;
            int stepY = point1.y < point2.y ? 1 : -1;
            int error = (int) (direction.x - direction.y);
            
            while(point1.x != point2.x || point1.y != point2.y)
            {
                pointsBetween.Add(point1);

                int newError = 2 * error;

                if(newError > -direction.x)
                {
                    error -= (int) direction.y;
                    point1 = new Vector2(point1.x + stepX, point1.y);
                }

                if(newError < direction.x)
                {
                    error += (int) direction.x;
                    point1 = new Vector2(point1.x, point1.y + stepY);
                }
            }

            pointsBetween.Add(point2);
            
            return pointsBetween;
        }

        public static List<Vector2> GeneratePointsOnEdges(Vector2 point, int radius)
        {
            List<Vector2> vertices = GenerateRhombusVertices(point, radius);
            List<Vector2> pointsOnEdges = new List<Vector2>();

            for (int i = 0; i < vertices.Count; i++)
            {
                // Add the current vertex
                pointsOnEdges.Add(vertices[i]);
                Debug.Log("vertice: " + vertices[i]);

                // Add points between current and next vertex
                int nextIndex = (i + 1) % vertices.Count;
                List<Vector2> pointsBetween = GeneratePointsBetween(vertices[i], vertices[nextIndex]);
                
                pointsOnEdges.AddRange(pointsBetween);
            }

            return pointsOnEdges.Distinct().ToList();
        }
    }
}
