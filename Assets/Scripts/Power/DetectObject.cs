using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    // Event that is raised when an object is detected
    public static event Action<GameObject> LevObjectDetected;
    public static event Action<GameObject> EnemyObjDetected;
    // Event that is rasied when an object is no longer in sight
    public static event Action LevObjectGone;
    public static event Action EnemyObjGone;

    //A boolean that InputCameraChange modifies to inform this class to run the private method CastSphere
    public static bool EnemySearchNeeded;
    public static float SearchDirection;

    [SerializeField]
    Transform detectPoint;
    [SerializeField]
    float detectRadius, detectRange;

    Vector3 detectVector;
    GameObject currentLevObject = null;
    GameObject currentLockOnTarget = null;

    private void Update()
    {
        detectVector = detectPoint.position;
        //CastSphere("LevitatableObject");
        FindTeleObj();
        if (EnemySearchNeeded && EnemyObjDetected != null)
        {
            InputCameraChange cameraChange = (InputCameraChange)EnemyObjDetected.Target;
            if (cameraChange.lockOn)
            {
                FindSecondEnemy();
            }

            else CastSphere("Enemy");
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
        if (FindTag == "LevitatableObject") SearchingLayerMask = 1 << LayerMask.NameToLayer("TeleObjects");
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
                        FindTeleObj();
                        //OnLevObjectDetected(hit.collider.gameObject);
                        //currentLevObject = hit.collider.gameObject;
                        //currentLevObject.GetComponentInChildren<Light>().enabled = true;
                    }
                    else if (FindTag == "Enemy")
                    {
                        OnEnemyObjDetected(hit.collider.gameObject);
                        //We found an enemy, we don't need to search anymore
                        EnemySearchNeeded = false;
                    }
                }
            }

        }
        else
        {
            Debug.Log("No" + FindTag);
            if (FindTag == "LevitatableObject")
            {
                Debug.Log("Deselected: " + currentLevObject);
                currentLevObject.GetComponentInChildren<Light>().enabled = false;
                currentLevObject = null;
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

    private void FindTeleObj()
    {
        int TeleLayer = 1 << LayerMask.NameToLayer("TeleObjects");
        Camera LockOnCamera = GameObject.Find("Main Camera").GetComponent<Cinemachine.CinemachineBrain>().OutputCamera;
        Plane[] CameraFrame = new Plane[1];
        if (LockOnCamera != null) CameraFrame = GeometryUtility.CalculateFrustumPlanes(LockOnCamera);
        else Debug.Log("Camera Not Found");
        Collider[] TeleObjsInRange = Physics.OverlapSphere(detectVector, detectRange, TeleLayer);
        List<GameObject> TeleObjInFrame = new List<GameObject>();
        GameObject TeleObjToReturn = null;

        if (CameraFrame.Length == 6)
        {
            foreach (Collider LevObjCollider in TeleObjsInRange)
            {
                bool InCamera = GeometryUtility.TestPlanesAABB(CameraFrame, LevObjCollider.bounds);
                if (InCamera) TeleObjInFrame.Add(LevObjCollider.gameObject);
            }

            TeleObjInFrame.Sort(CompareUnsignedDistanceToCenterScreen);
            if (TeleObjInFrame.Count > 0) TeleObjToReturn = TeleObjInFrame[0];
        }
        if (TeleObjToReturn != null)
        {
            if (currentLevObject != null)
            {

                currentLevObject.GetComponentInChildren<Light>().enabled = false;
            }
            OnLevObjectDetected(TeleObjToReturn);
            currentLevObject = TeleObjToReturn;
            currentLevObject.GetComponentInChildren<Light>().enabled = true;
        }
        else OnLevObjectGone();
        
    }

    private void FindSecondEnemy()
    {
        int EnemyLayer = 1 << LayerMask.NameToLayer("Enemies");
        Camera LockOnCamera = GameObject.Find("Main Camera").GetComponent<Cinemachine.CinemachineBrain>().OutputCamera;
        Plane[] CameraFrame = new Plane[1];
        if (LockOnCamera != null) CameraFrame = GeometryUtility.CalculateFrustumPlanes(LockOnCamera);
        else Debug.Log("Camera Not Found");
        currentLockOnTarget.layer = LayerMask.NameToLayer("Ignore Raycast");
        Collider[] EnemiesInRange = Physics.OverlapSphere(transform.position, detectRange, EnemyLayer);
        currentLockOnTarget.layer = LayerMask.NameToLayer("Enemies");
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

                int PlaneIndexToTranslate;
                if (SearchDirection == -1)
                {
                    PlaneIndexToTranslate = (int)CameraFrameWalls.right;
                }
                else PlaneIndexToTranslate = (int)CameraFrameWalls.left;
                if (EnemiesInFrame.Count != 0)
                {
                    Vector3 CameraCenter = LockOnCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Vector3.Distance(LockOnCamera.transform.position, detectPoint.transform.position)));
                    Plane SearchPlane = Plane.Translate(CameraFrame[PlaneIndexToTranslate], CameraCenter);
                    if (SearchDirection < 0)
                    {
                        SearchPlane.normal = -(detectPoint.right);
                    }
                    else
                    {
                        SearchPlane.normal = detectPoint.right;
                    }
                    if (SearchPlane.GetDistanceToPoint(EnemiesInFrame[0].transform.position) > 0)
                    {
                        EnemyToReturn = EnemiesInFrame[0].gameObject;
                    }

                }
            //}
            if (EnemyToReturn == null && EnemiesInFrame.Count > 0)
            {
                if(EnemiesInFrame[0])
                EnemyToReturn = EnemiesInFrame[EnemiesInFrame.Count - 1].gameObject;
            }
        }
        if (EnemyToReturn != null)
        {
            OnEnemyObjDetected(EnemyToReturn);
        }
        EnemySearchNeeded = false;
    }

    private int CompareDistanceToCenterScreen(GameObject X, GameObject Y)
    {
        Camera LockOnCamera = GameObject.Find("Main Camera").GetComponent<Cinemachine.CinemachineBrain>().OutputCamera;
        Vector3 CameraCenter = LockOnCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Vector3.Distance(LockOnCamera.transform.position, detectPoint.transform.position)));
        Plane SearchPlane;
        //Searchdirection < 0 means left, and > 0 means right
        //-1 returns X as being closer, 1 returns Y as being closer, 0 means they're the same
        if (SearchDirection < 0)
        {
            SearchPlane = Plane.Translate(GeometryUtility.CalculateFrustumPlanes(LockOnCamera)[(int)CameraFrameWalls.right], CameraCenter);
            SearchPlane.normal = -(detectPoint.right);
        }
        else
        {
            SearchPlane = Plane.Translate(GeometryUtility.CalculateFrustumPlanes(LockOnCamera)[(int)CameraFrameWalls.left], CameraCenter);
            SearchPlane.normal = detectPoint.right;
        }
        Debug.DrawLine(LockOnCamera.transform.position, CameraCenter + detectPoint.transform.forward * 10, Color.red);
        Debug.DrawLine(CameraCenter, CameraCenter + SearchPlane.normal/ 2, Color.blue);
        Debug.DrawLine(CameraCenter - Vector3.up / 2, CameraCenter + Vector3.up/ 2, Color.green);
        Debug.Break();
        //x is null
        if (X == null)
        {
            if (Y == null) return 0; //if y is null, neither are greater, return 0
            else return 1; //if x is null and y is not null, then y is closer to the center, so we return it (shouldn't happen but still)
        }
        //x is not null
        else
        {
            if (Y == null) return -1; //if X is not null and Y is, then X is closer to the center, so we return it
            else
            {
                float DistanceToX = SearchPlane.GetDistanceToPoint(X.transform.position);
                float DistanceToY = SearchPlane.GetDistanceToPoint(Y.transform.position);
                if (DistanceToX < 0) { //X is in the opposite direction of the search plane
                    if (DistanceToY < 0) //Y is also on the wrong side of the search plane
                    {
                        return -DistanceToX.CompareTo(DistanceToY);
                        //since both values are on the wrong side of the search plane, we want the more negative value to be considered larger, hence the negative
                        //that way the end of the list is the farthest in the wrong direction
                    }
                    else return 1; //if X is on the wrong side and Y isn't, then we want to consider Y to be closer
                }
                else //X is on the correct side of the plane
                {
                    if (DistanceToY < 0) return -1; //if X is on the correct side and Y isn't, then we want X to be considered closer
                    else return DistanceToX.CompareTo(DistanceToY); //Both X and Y are on the correct side, therefore we sort based on which is closer
                }

            }
        }
    }

    private int CompareUnsignedDistanceToCenterScreen(GameObject X, GameObject Y)
    {
        //-1 means X is closer, 1 means Y is closer, and 0 means they're the same distance
        Camera LockOnCamera = GameObject.Find("Main Camera").GetComponent<Cinemachine.CinemachineBrain>().OutputCamera;
        Vector3 CameraCenter = LockOnCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Vector3.Distance(LockOnCamera.transform.position, detectPoint.transform.position)));
        if (X == null)
        {
            if (Y == null) return 0; //if y is null, neither are greater, return 0
            else return 1; //if x is null and y is not null, then y is closer to the center, so we return it (shouldn't happen but still)
        }
        //x is not null
        else
        {
            if (Y == null) return -1; //if X is not null and Y is, then X is closer to the center, so we return it
            else
            {
                float DistanceToX = Vector3.Distance(X.transform.position, CameraCenter);
                float DistanceToY = Vector3.Distance(Y.transform.position, CameraCenter);
                return DistanceToX.CompareTo(DistanceToY);
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
            currentLockOnTarget = detObj;
            EnemyObjDetected.Invoke(detObj);
        }
    }

    private void OnEnemyObjGone()
    {
        currentLockOnTarget = null;
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
}