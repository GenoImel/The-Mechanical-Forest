using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Akashic.Runtime.Utilities.Grid
{
    internal static class GridGenerator
    {
        /// <summary>
        /// Uses a predefined <paramref name="plane2D"/> to generate a 2D grid
        /// with <paramref name="numRow"/> rows and <paramref name="numCol"/> columns.
        /// </summary>
        public static List<Vector2> Create2DGrid(Plane2D plane2D, int numRow, int numCol)
        {
            if (numRow == 0 || numCol == 0)
            {
                throw new Exception($"The number of rows or columns in the grid cannot be zero.");
            }

            var pointsAlongX = GeneratePoints(
                new Vector2(plane2D.MinBounds.x, 0), 
                new Vector2(plane2D.MaxBounds.x, 0), 
                numRow
                );
            
            var pointsAlongY = GeneratePoints(
                new Vector2(0, plane2D.MinBounds.y), 
                new Vector2(0, plane2D.MaxBounds.y), 
                numCol
                );

            var gridPoints = Generate2DGrid(pointsAlongX, pointsAlongY);
            return gridPoints;
        }

        /// <summary>
        /// Uses a <paramref name="meshPlane"/> from a primitive plane
        /// to define a plane across the XY dimensions.
        /// </summary>
        public static Plane2D DefineXYPlane(MeshFilter meshPlane)
        {
            var meshBounds = new MeshBounds2D(meshPlane);

            var plane = new Plane2D(
                new Vector2(meshBounds.MinBounds.x, meshBounds.MinBounds.y), 
                new Vector2(meshBounds.MaxBounds.x, meshBounds.MaxBounds.y)
            );
            return plane;
        }

        /// <summary>
        /// Uses a <paramref name="meshPlane"/> from a primitive plane
        /// to define a plane across the XZ dimensions.
        /// </summary>
        public static Plane2D DefineXZPlane(MeshFilter meshPlane)
        {
            var meshBounds = new MeshBounds2D(meshPlane);

            var plane = new Plane2D(
                new Vector2(meshBounds.MinBounds.x, meshBounds.MinBounds.z), 
                new Vector2(meshBounds.MaxBounds.x, meshBounds.MaxBounds.z)
                );
            
            return plane;
        }
        
        /// <summary>
        /// Uses a <paramref name="meshPlane"/> from a primitive plane
        /// to define a plane across the YZ dimensions.
        /// </summary>
        public static Plane2D DefineYZPlane(MeshFilter meshPlane)
        {
            var meshBounds = new MeshBounds2D(meshPlane);

            var plane = new Plane2D(
                new Vector2(meshBounds.MinBounds.y, meshBounds.MinBounds.z), 
                new Vector2(meshBounds.MaxBounds.y, meshBounds.MaxBounds.z)
            );
            return plane;
        }
        
        /// <summary>
        /// Generates <paramref name="numPoints"/> across a line starting at <paramref name="startPoint"/>
        /// and ending at <param name="endPoint"></param>.
        /// </summary>
        private static IEnumerable<Vector2> GeneratePoints(Vector3 startPoint, Vector3 endPoint, int numPoints)
        {
            var result = new List<Vector2>();

            if (numPoints == 0)
            {
                Debug.Log("Variable numPoints cannot be zero.");
                return result;
            }

            var fraction = 1f / numPoints;
            var point = 0f;

            if (numPoints == 1)
            {
                result.Add(Vector2.Lerp(startPoint, endPoint, 0.5f));
                return result;
            }
            
            for (var i = 0; i < numPoints; i++)
            {
                if (i == 0)
                {
                    point = fraction / 2;
                }
                else
                {
                    point += fraction;
                }
                result.Add(Vector2.Lerp(startPoint, endPoint, point));
            }
      
            return result;
        }

        /// <summary>
        /// Generates a 2D grid. Agnostic to orientation.
        /// </summary>
        /// <param name="pointsAlongM">Number of points along the M dimension.</param>
        /// <param name="pointsAlongN">Number of points along the N dimension</param>
        /// <returns></returns>
        private static List<Vector2> Generate2DGrid(IEnumerable<Vector2> pointsAlongM, IEnumerable<Vector2> pointsAlongN)
        {
            return (from pointM in pointsAlongM from pointN in pointsAlongN 
                select new Vector2(pointM.x, pointN.y)).ToList();
        }

        internal readonly struct Plane2D
        {
            public Vector2 MinBounds { get; }
            public Vector2 MaxBounds { get; }

            public Plane2D(Vector2 minBounds, Vector2 maxBounds)
            {
                MinBounds = minBounds;
                MaxBounds = maxBounds;
            }
        }

        private readonly struct MeshBounds2D
        {
            public readonly Vector3 MinBounds { get; }
            public readonly Vector3 MaxBounds { get; }

            public MeshBounds2D(MeshFilter meshPlane)
            {
                var bounds = meshPlane.mesh.bounds;
                MinBounds = meshPlane.transform.TransformPoint(bounds.min);
                MaxBounds = meshPlane.transform.TransformPoint(bounds.max);
            }
        }
    }
}