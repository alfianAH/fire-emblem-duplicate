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

        public static List<Vector2> GeneratePointsInsideRhombus(Vector2 point, int radius)
        {
            List<Vector2> pointsOnEdges = new List<Vector2>();
            
            for(int i = 0; i <= radius; i++)
            {
                pointsOnEdges.AddRange(GeneratePointsInRhombusEdge(point, i));
            }

            return pointsOnEdges.Distinct().ToList();
        }

        /// <summary>
        /// Generate points in rhombus edge
        /// Based on Bresenham's Line Algorithm
        /// </summary>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static List<Vector2> GeneratePointsInRhombusEdge(Vector2 point, int radius)
        {
            List<Vector2> pointsOnEdges = new List<Vector2>();
            List<Vector2> vertices = GenerateRhombusVertices(point, radius);

            for (int j = 0; j < vertices.Count; j++)
            {
                // Add the current vertex
                pointsOnEdges.Add(vertices[j]);

                // Add points between current and next vertex
                int nextIndex = (j + 1) % vertices.Count;
                List<Vector2> pointsBetween = GeneratePointsBetween(vertices[j], vertices[nextIndex]);

                pointsOnEdges.AddRange(pointsBetween);
            }

            return pointsOnEdges;
        }
    }
}
