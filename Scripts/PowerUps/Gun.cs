using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 35f;
    public float fireRate = 15f;

    public Camera shootCam;

    [SerializeField]
    Rigidbody crystalShot;

    [SerializeField]
    Transform fireTransform;

    [SerializeField]
    float crystalSpeed;

    //public ParticleSystem muzzleFlash;
    //public GameObject impactEffect;

    private float nectTimeTofire = 0f;


    // Update is called once per frame
    void Update () {
		if(InputManager.BButton() && Time.time >= nectTimeTofire)
        {
            nectTimeTofire = Time.time + 1f / fireRate; //The greater the fire rate, the less time between shots
            Shoot();
        }
	}

    void Shoot()
    {
        //muzzleFlash.Play(); 
        //RaycastHit hit;

        //if(Physics.Raycast(shootCam.transform.position, shootCam.transform.forward, out hit, range))
        //{
        //    Debug.Log(hit.transform.name);

        //    Target target = hit.transform.GetComponent<Target>();
        //    if (target != null)
        //    {
        //        target.TakeDamage(damage);
        //    }

        //   if (hit.rigidbody != null)
        //    {
        //        hit.rigidbody.AddForce(-hit.normal * impactForce);
        //    }

        //    //GameObject impactGameObj = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //    //Destroy(impactGameObj, 1.5f);
        //}

        Rigidbody cyrstalShotInstance = Instantiate(crystalShot, fireTransform.position, fireTransform.rotation) as Rigidbody;
        cyrstalShotInstance.velocity = crystalSpeed * fireTransform.forward;
    }
}
