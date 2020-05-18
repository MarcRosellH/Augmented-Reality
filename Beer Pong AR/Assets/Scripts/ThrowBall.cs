using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject touchPosition;
    public GameObject first_parent;
    public GameObject camera_parent;
    public string respawnName = "";
    turnManager _turnManager;
    public int playerTurn = 1;
  


    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;
    bool holding = false;

    [SerializeField]
    float throwForceInXandY = 10.0f;

    [SerializeField]
    float throwForceInZ = 40.0f;

    Rigidbody rb;
    AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio_source = GetComponent<AudioSource>();
        _turnManager = this.GetComponent<turnManager>();
    }

    // Update is called once per frame

    void OnTouch()
    {
        Vector3 mousePos = Input.GetTouch(0).position;
        Vector3 ballScreen = Camera.main.WorldToScreenPoint(touchPosition.transform.position);
        mousePos.z = ballScreen.z;
        //mousePos.z = Camera.main.nearClipPlane * howClose;
        //newPosition = Camera.main.ScreenToViewportPoint(mousePos);
        Vector3 newPosition = Vector3.Lerp(ballScreen, mousePos, 80.0f * Time.deltaTime);
        this.transform.localPosition = Camera.main.ScreenToWorldPoint(newPosition);

    }
    void Update()
    {
        if (holding)
            OnTouch();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Touched");
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
            holding = true;
            transform.SetParent(first_parent.transform);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && holding)
        {
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;

            endPos = Input.GetTouch(0).position;
            direction = startPos - endPos;

            float swipeDistance = (endPos - startPos).magnitude;


            if (timeInterval > 0.4f && swipeDistance > 100.0f)
            {
                rb.useGravity = true;
                
                rb.AddForce(-direction.x * throwForceInXandY, -direction.y * throwForceInXandY, throwForceInZ*swipeDistance);
                holding = false;

                Debug.Log("Throwed");
                Invoke("ChangeTurnReset", 7.0f);
            }
            else
            {
                Debug.Log("Reset Normal");
                _Reset();
            }

        }
    }



    void _Reset()
    {
        transform.SetParent(camera_parent.transform);
        Transform respawnPoint = GameObject.Find(touchPosition.name).transform;
        this.gameObject.transform.position = respawnPoint.position;
        this.gameObject.transform.rotation = respawnPoint.rotation;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        holding = false;

    }

    public void ChangeTurnReset()
    {
        Debug.Log("Changing turn");
        transform.SetParent(first_parent.transform);
        Transform respawnPoint = GameObject.Find(respawnName).transform;
        this.gameObject.transform.position = respawnPoint.position;
        this.gameObject.transform.rotation = respawnPoint.rotation;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        holding = false;


        if (playerTurn == 1)
        {
            playerTurn = 2;
            _turnManager.ChangeTurn(playerTurn);
        }
        else
        {
            playerTurn = 1;
            _turnManager.ChangeTurn(playerTurn);
        }
    }



    void OnCollisionEnter(Collision collision)
    {

        audio_source.Play();
        
    }
}
