using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    
    // Variables
    //  -   -   -   -   -   -   -   -   -   -
    public struct ViewCastInfo
    {
        public bool Hit;
        public Vector3 Point;
        public float Distance;
        public float Angle;

        public ViewCastInfo(bool newHit, Vector3 newPoint, float newDistance, float newAngle)
        {
            Hit = newHit;
            Point = newPoint;
            Distance = newDistance;
            Angle = newAngle;
        }
    }

    public struct EdgeInfo
    {
        public Vector3 PointA;
        public Vector3 PointB;

        public EdgeInfo(Vector3 NewPointA, Vector3 NewPointB)
        {
            PointA = NewPointA;
            PointB = NewPointB;
        }
    }

    public float ViewRadius;
    [Range(0, 360)]
    public float ViewAngle;

    public float DelayInSeconds = 0.2f;

    public LayerMask TargetMask;
    public LayerMask ObstacleMask;

    [HideInInspector]
    public List<Transform> VisibleTargets;

    public float MeshResolution = 0.0f;

    public MeshFilter ViewMeshFilter;
    private Mesh viewMesh;

    public int EdgeResolveIterations = 0;

    public float EdgeDistanceThreshold = 0.0f;
    

    // Methods
    //  -   -   -   -   -   -   -   -   -   -
    private void Start()
    {
        ObstacleMask = ~ObstacleMask;

        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        ViewMeshFilter.mesh = viewMesh;

        StartCoroutine("FindTargetsWithDelay", DelayInSeconds);
    }
    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    IEnumerator FindTargetsWithDelay(float DelayInSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(DelayInSeconds);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        VisibleTargets.Clear();

        Collider[] targetsInView = Physics.OverlapSphere(transform.position, ViewRadius, TargetMask);

        for (int i = 0; i < targetsInView.Length; ++i)
        {
            Transform target = targetsInView[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < ViewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast (transform.position, directionToTarget, distanceToTarget, ObstacleMask))
                {
                    VisibleTargets.Add(target);
                }
            }
        }
    }//*/

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(ViewAngle * MeshResolution);
        float stepAngleSize = ViewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo oldViewCast = new ViewCastInfo();

        for (int i = 0; i < stepCount; ++i)
        {
            float angle = transform.eulerAngles.y - ViewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);

            if (i > 0)
            {
                bool edgeDistanceThresholdExceeded = Mathf.Abs(oldViewCast.Distance - newViewCast.Distance) > EdgeDistanceThreshold;

                if (oldViewCast.Hit != newViewCast.Hit || oldViewCast.Hit && newViewCast.Hit && edgeDistanceThresholdExceeded)
                {
                    EdgeInfo edge = FindEdge(oldViewCast, newViewCast);

                    if (edge.PointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.PointA);
                    }

                    if (edge.PointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.PointB);
                    }
                }
            }

            viewPoints.Add(newViewCast.Point);
            oldViewCast = newViewCast;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; ++i)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    public Vector3 DirectionFromAngle(float AngleInDegrees, bool AngleIsGlobal)
    {
        if (!AngleIsGlobal)
        {
            AngleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(AngleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(AngleInDegrees * Mathf.Deg2Rad));
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirectionFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, ViewRadius, ObstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }

        return new ViewCastInfo(false, transform.position + direction * ViewRadius, ViewRadius, globalAngle);
    }

    private EdgeInfo FindEdge(ViewCastInfo minimumViewCast, ViewCastInfo maximumViewCast)
    {
        float minimumAngle = minimumViewCast.Angle;
        float maximumAngle = maximumViewCast.Angle;
        Vector3 minimumPoint = Vector3.zero;
        Vector3 maximumPoint = Vector3.zero;

        for (int i = 0; i < EdgeResolveIterations; ++i)
        {
            float angle = (minimumAngle + maximumAngle) / 2.0f;
            ViewCastInfo newViewCast = ViewCast(angle);

            bool edgeDistanceThresholdExceeded = Mathf.Abs(minimumViewCast.Distance - newViewCast.Distance) > EdgeDistanceThreshold;


            if (newViewCast.Hit == minimumViewCast.Hit && !edgeDistanceThresholdExceeded)
            {
                minimumAngle = angle;
                minimumPoint = newViewCast.Point;
            }
            else
            {
                maximumAngle = angle;
                maximumPoint = newViewCast.Point;
            }
        }

        return new EdgeInfo(minimumPoint, maximumPoint);
    }
}
