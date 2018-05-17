using UnityEngine;

public class Spear : MonoBehaviour
{
    public float throwDamage = 10f;
    public float throwRange = 20f;
    public float throwForce = 1000;
    public float respawnTime = 5.0f;
    float timer;
    Vector3 pos;
    Quaternion rot;
    Vector3 scale;

    public Camera fpsCam;
    public Transform playerCam;


    bool thrown;
    bool beingCarried;
	GameObject tiger;// = GameObject.FindGameObjectWithTag("Tiger");
    Spear clone;

    private void Awake()
    {
        beingCarried = true;
        thrown = false;
    }

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        if (timer > respawnTime)
        {
            thrown = false;
            beingCarried = true;
        }

        if (beingCarried == true)
        {
            
            GetComponent<Rigidbody>().isKinematic = true;
            pos = transform.position;
            rot = transform.rotation;
            scale = transform.localScale;
            //set up the spear in front of the camera
            transform.parent = playerCam;
            pos.x = 0.5f;
            pos.y = -0.25f;
            pos.z = 0f;

            rot = Quaternion.Euler(0, -7, 0);

            scale.x = 0.05f;
            scale.y = 0.05f;
            scale.z = 0.05f;

            transform.localPosition = pos;
            transform.localRotation = rot;
            transform.localScale = scale;

            if (Input.GetButtonDown("Fire1"))
            {
                Throw();
            }

            else if (Input.GetButtonDown("Fire2"))
            {
                Stab();
            }
        }
		
	}

    void Throw()
    {

        timer = 0f;
        
        thrown = true;

        beingCarried = false;

        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        //clone.GetComponent<Rigidbody>().useGravity = true;
        
        GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, throwRange))
        {
            TigerHealth tigerHealth = hit.transform.GetComponent<TigerHealth>();
            if (tigerHealth != null)
            {
                tigerHealth.TakeDamage(throwDamage);
            }
        }

    }

    void Stab()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == tiger)
        {
			tiger = GameObject.FindGameObjectWithTag("Tiger");
            TigerHealth tigerHealth = tiger.GetComponent<TigerHealth>();
            if (tigerHealth != null)
            {
                if (thrown == true)
                {
                    tigerHealth.TakeDamage(throwDamage);
                    //Destroy(gameObject);
                }
            }
            else
            {
                //Destroy(gameObject);
            }
        }
    }
}
