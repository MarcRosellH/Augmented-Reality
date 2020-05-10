using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwObject : MonoBehaviour
{
    public float maxObjectSpeed = 40.0f;
    public float flickSpeed = 0.4f;

    public string respawnName = "";
    public float howClose = 9.5f;

    float startTime, endTime, swipeDistance, swipeTime;
    Vector2 startPos;
    Vector2 endPos;
    float tempTime;

    float flickLength;
    float objectVelocity = 0.0f;
    float objectSpeed = 0.0f;
    Vector3 angle;

    bool thrown, holding;
    Vector3 newPosition, velocity;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
    }

    void OnTouch()
    {
       Vector3 mousePos = Input.GetTouch(0).position;
        Vector3 ballScreen = Camera.main.WorldToScreenPoint(this.transform.position);
        mousePos.z = ballScreen.z;
        //mousePos.z = Camera.main.nearClipPlane * howClose;
        //newPosition = Camera.main.ScreenToViewportPoint(mousePos);
        newPosition = Vector3.Lerp(ballScreen, mousePos, 80.0f * Time.deltaTime);
        this.transform.localPosition = Camera.main.ScreenToWorldPoint(newPosition);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (holding)
            OnTouch();
        else if (thrown)
            return;
        else
        { }

        if (Input.touchCount > 0)
        {
            Debug.Log("Enter Touching");
            Touch _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.transform == this.transform)
                    {
                        startTime = Time.time;
                        startPos = _touch.position;
                        holding = true;
                        transform.SetParent(null);
                    }
                }
            }

            else if (_touch.phase == TouchPhase.Ended && holding)
            {
                endTime = Time.time;
                endPos = _touch.position;
                swipeDistance = (endPos - startPos).magnitude;
                swipeTime = endTime - startTime;

                if (swipeTime < flickSpeed && swipeDistance > 100.0f)
                {
                    CalSpeed();
                    MoveAngle();
                    this.GetComponent<Rigidbody>().AddForce(new Vector3(/*angle.x **/ 0.0f,/* angle.y **/ 50.0f, /*angle.z **/ 30.0f));
                    this.GetComponent<Rigidbody>().useGravity = true;
                    holding = false;
                    thrown = true;
                    Invoke("_Reset", 5.0f);
                }
                else
                    _Reset();

            }

            if (startTime > 0)
                tempTime = Time.time - startTime;

            if (tempTime > flickSpeed)
            {
                startTime = Time.time;
                startPos = _touch.position;
            }

        }
    }



    void _Reset()
    {
        Transform respawnPoint = GameObject.Find(respawnName).transform;
        this.gameObject.transform.position = respawnPoint.position;
        this.gameObject.transform.rotation = respawnPoint.rotation;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        thrown = false;
        holding = false;
    }

    void CalSpeed()
    {
        flickLength = swipeDistance;
        if(swipeTime>0)
            objectVelocity = flickLength / (flickLength - swipeDistance);

        objectSpeed = objectVelocity * 50.0f;
        //objectSpeed = objectSpeed - (objectSpeed*1.7f);

        if(objectSpeed<= -maxObjectSpeed)
            objectSpeed = -maxObjectSpeed;

        swipeTime = 0;
             
    }

    void MoveAngle()
    {
        angle = Camera.main.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(endPos.y + 50.0f, Camera.main.GetComponent<Camera>().nearClipPlane - howClose));
    }



}
