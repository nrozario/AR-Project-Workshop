using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamScript : MonoBehaviour
{
    public GameObject webCameraPlane;
    public Button fireButton;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {

        // Edit 2: if being run on a mobile device, create a camera parent object and set its transform 
        //  'this' (main camera) camera's transform.. Then rotate the parent 90 degrees right
        if (Application.isMobilePlatform)
        {
            GameObject cameraParent = new GameObject("camParent");
            cameraParent.transform.position = this.transform.position;
            this.transform.parent = cameraParent.transform;
            cameraParent.transform.Rotate(Vector3.right, 90);
        }

        // Enable gyro
        Input.gyro.enabled = true;

        // Edit 3: detect button click
        fireButton.onClick.AddListener(OnButtonDown);


        // Edit 1: Set plane to display web camera data
        WebCamTexture webCameraTexture = new WebCamTexture();
        webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCameraTexture;
        webCameraTexture.Play();
    }

    // Edit 3
    void OnButtonDown()
    {
        Vector3 bulletDirection = Camera.main.transform.position;
        Quaternion bulletRotation = Camera.main.transform.rotation * Quaternion.Euler(90, 0, 0);
        Vector3 bulletFireDirection = Camera.main.transform.forward;
        GameObject newBullet = Instantiate(bullet, bulletDirection, bulletRotation);

        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletFireDirection * 500f);
        Destroy(newBullet, 3);
        GetComponent<AudioSource>().Play();


    }

    // Update is called once per frame
    void Update()
    {
        // Edit 2
        Quaternion cameraRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
        this.transform.localRotation = cameraRotation;
    }
}
