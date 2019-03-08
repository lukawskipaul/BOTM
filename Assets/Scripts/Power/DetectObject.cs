using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    // Event that is raised when an object is detected
    public static event Action<GameObject> LevObjectDetected;
    public static event Action<GameObject> EnemyObjDetected;
    public static event Action<GameObject> TKPullTargetDetected;
    // Event that is rasied when an object is no longer in sight
    public static event Action LevObjectGone;
    public static event Action EnemyObjGone;

    //A boolean that InputCameraChange modifies to inform this class to run the private method CastSphere
    public static bool EnemySearchNeeded;
    public static bool TKPullTargetSearchNeeded;
    public static float SearchDirection;

    [SerializeField]
    Transform detectPoint;
    [SerializeField]
    float detectRadius, detectRange;

    Vector3 detectVector;

    private void Update()
    {
        detectVector = detectPoint.position;
        CastSphere("LevitatableObject");
        if (EnemySearchNeeded && EnemyObjDetected != null)
        {
            InputCameraChange cameraChange = (InputCameraChange)EnemyObjDetected.Target;
            if (cameraChange.lockOn) {
                FindSecondEnemy();
            }

            else CastSphere("Enemy");
        }

        if (TKPullTargetSearchNeeded)
        {
            CastSphere("Enemy");
        }
    }

    //Spherecast To detect objects We can use telekinesis on, then do a Raycast along the path between the detectVector and the levitation object to make sure it's clear
    private void CastSphere(String FindTag)
    {
        Ray ray = new Ray(detectVector, detectPoint.forward);
        RaycastHit hit;
        int IgnoreRayCastLayer = 1 << LayerMask.NameToLayer("Ignore Raycast");
        int PlayerLayer = 1 << LayerMask.NameToLayer("Player");
        int SearchingLayerMask = ~((IgnoreRayCastLayer) | (PlayerLayer));
        if (FindTag == "Enemy") SearchingLayerMask = 1 << LayerMask.NameToLayer("Enemies");
        if (Physics.SphereCast(ray, detectRadius, out hit, detectRange, SearchingLayerMask))
        {
            if (hit.collider.gameObject.tag == FindTag)
            {
                bool pathToObjClear;
                RaycastHit nonTagObjHit;
                float distToLevObj = (hit.point - detectVector).magnitude;
                if (Physics.Raycast(detectVector, hit.point - detectVector, out nonTagObjHit, distToLevObj, SearchingLayerMask))
                {
                    if (nonTagObjHit.collider.gameObject.tag != FindTag && nonTagObjHit.collider.gameObject.tag != "Checkpoint" && nonTagObjHit.collider.gameObject.tag != "Untagged")
                    {
                        pathToObjClear = false;
                        //the path to the enemy is blocked, give up searching
                        if (FindTag == "Enemy") EnemySearchNeeded = false;
                    }
                    else
                    {
                        pathToObjClear = true;
                    }
                }
                else
                {
                    pathToObjClear = true;
                }
                if (pathToObjClear)
                {
                    if (FindTag == "LevitatableObject")
                    {
                        OnLevObjectDetected(hit.collider.gameObject);
                        hit.collider.GetComponent<MeshRenderer>().material.color = Color.green;
                    }
                    else if (FindTag == "Enemy")
                    {
                        if (TKPullTargetSearchNeeded)
                        {
                            OnTKPullTargetDetected(hit.collider.gameObject);
                            //We found an enemy, we don't need to search anymore
                            TKPullTargetSearchNeeded = false;
                        }
                        else
                        {
                            OnEnemyObjDetected(hit.collider.gameObject);
                            //We found an enemy, we don't need to search anymore
                            EnemySearchNeeded = false;
                        }
                    }
                }
            }

        }
        else
        {
            Debug.Log("No" + FindTag);
            if (FindTag == "LevitatableObject")
            {
                OnLevObjectGone();
            }

            if (FindTag == "Enemy")
            {
                OnEnemyObjGone();
                //there was no enemy, don't keep searching
                EnemySearchNeeded = false;
            }
        }
    }


    private void FindSecondEnemy()
    {
        int EnemyLayer = 1 << LayerMask.NameToLayer("Enemies");
        Camera LockOnCamera = GameObject.Find("Main Camera").GetComponent<Cinemachine.CinemachineBrain>().OutputCamera;
        Plane[] CameraFrame = new Plane[1];
        if (LockOnCamera != null) CameraFrame = GeometryUtility.CalculateFrustumPlanes(LockOnCamera);
        else Debug.Log("Camera Not Found");
        Collider[] EnemiesInRange = Physics.OverlapSphere(transform.position, detectRange, EnemyLayer);
        List<GameObject> EnemiesInFrame = new List<GameObject>();
        GameObject EnemyToReturn = null;

        if (CameraFrame.Length == 6)
        {
            foreach (Collider EnemyCollider in EnemiesInRange)
            {
                bool InCamera = GeometryUtility.TestPlanesAABB(CameraFrame, EnemyCollider.bounds);
                if (InCamera) EnemiesInFrame.Add(EnemyCollider.gameObject);
            }

            EnemiesInFrame.Sort(CompareDistanceToCenterScreen);

            foreach (GameObject Enemy in EnemiesInFrame)
            {
                int PlaneIndexToTranslate;
                if (SearchDirection == -1)
                {
                    PlaneIndexToTranslate = (int)CameraFrameWalls.right;
                }
                else PlaneIndexToTranslate = (int)CameraFrameWalls.left;
                if (EnemiesInFrame.Count != 0)
                {
                    Vector3 CameraCenter = LockOnCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, LockOnCamera.nearClipPlane));
                    Plane SearchPlane = Plane.Translate(CameraFrame[PlaneIndexToTranslate], CameraCenter);
                    if (SearchDirection < 0)
                    {
                        SearchPlane.SetNormalAndPosition(-(detectPoint.right), detectPoint.transform.position);
                    }
                    else
                    {
                        SearchPlane.SetNormalAndPosition(detectPoint.right, detectPoint.transform.position);
                    }
                    if (SearchPlane.GetDistanceToPoint(EnemiesInFrame[0].transform.position) > 0)
                    {
                        EnemyToReturn = EnemiesInFrame[0].gameObject;
                    }
                    
                }
            }
            if (EnemyToReturn == null && EnemiesInFrame.Count > 0)
            {
                EnemyToReturn = EnemiesInFrame[EnemiesInFrame.Count - 1].gameObject;
            }
        }
        if (EnemyToReturn != null)
        {
            OnEnemyObjDetected(EnemyToReturn);
        }
        EnemySearchNeeded = false;
    }

    private int CompareDistanceToCenterScreen(GameObject x, GameObject y)
    {
        Camera LockOnCamera = GameObject.Find("Main Camera").GetComponent<Cinemachine.CinemachineBrain>().OutputCamera;
        Vector3 CameraCenter = LockOnCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, LockOnCamera.nearClipPlane));
        Plane SearchPlane;
        if (SearchDirection < 0)
        {
            SearchPlane = Plane.Translate(GeometryUtility.CalculateFrustumPlanes(LockOnCamera)[(int)CameraFrameWalls.right], CameraCenter);
            SearchPlane.SetNormalAndPosition(-(detectPoint.right), detectPoint.transform.position);
        }
        else
        {
            SearchPlane = Plane.Translate(GeometryUtility.CalculateFrustumPlanes(LockOnCamera)[(int)CameraFrameWalls.left], CameraCenter);
            SearchPlane.SetNormalAndPosition(detectPoint.right, detectPoint.transform.position);
        }
        if (x == null)
        {
            if (y == null) return 0;
            else return -1;
        }
        else
        {
            if (y == null) return 1;
            else
            {
                float DistanceToX = SearchPlane.GetDistanceToPoint(x.transform.position);
                float DistanceToY = SearchPlane.GetDistanceToPoint(y.transform.position);
                if (DistanceToX < 0) return 1;
                else if (DistanceToY < 0) return -1;
                else
                {
                    int retval = (DistanceToX.CompareTo(DistanceToY));
                    return retval;
                }
            }
        }
    }

    private enum CameraFrameWalls
    {
        left, right, down, up, near, far
    }

    // Publish Events

    private void OnEnemyObjDetected(GameObject detObj)
    {
        if (EnemyObjDetected != null)
        {
            EnemyObjDetected.Invoke(detObj);
        }
    }

    private void OnEnemyObjGone()
    {
        EnemyObjGone.Invoke();
    }

    private void OnLevObjectDetected(GameObject detObj)
    {
        if (LevObjectDetected != null)
        {
            LevObjectDetected.Invoke(detObj);
        }
    }

    private void OnLevObjectGone()
    {
        if (LevObjectGone != null)
        {
            LevObjectGone.Invoke();
        }
    }

    private void OnTKPullTargetDetected(GameObject detObj)
    {
        if (TKPullTargetDetected != null)
        {
            TKPullTargetDetected.Invoke(detObj);
        }
    }
}