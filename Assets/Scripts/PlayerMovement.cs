using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    /*
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            MOVEMENT AND PHYSICS VARIABLES
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    */
    Rigidbody2D rb;
    Vector3 inputData;
    
    [SerializeField] float currentMoveSpeed = 0.0f;
    [SerializeField] float maxMoveSpeed = 40.0f;

    [SerializeField] float timeZeroToMax = 2.0f;
    float accelerationRate = 10.0f;
    float decelerationRate = 10.0f;

    /*
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                  HUNGER VARIABLES
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    */

    float hungerLevel;
    [SerializeField] float maxHungerLevel = 100.0f;


    /*
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
          BOOLEAN NECTAR AND POLLEN VARIABLES
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    */
    bool hasPollen = false;
    bool hasNectar = false;

    /*
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
               ANIMATION VARIABLES     
     @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    */

    Animator anim;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        Love();
    }

    private void Update() {
        HandleInput();
        FlipPlayer(inputData.x);
        
        UpdateAnimator();
        UpdateVariables();
    }
    private void FixedUpdate() {
        CalculateSpeed();
       
        rb.AddForce(inputData * currentMoveSpeed * Time.deltaTime);
    }

    public void IncreaseHunger(float amount) {
        hungerLevel += amount;
        UpdateHunger();

        if(hungerLevel == maxHungerLevel) {
            Debug.Log("YOU ARE TOO HUNGRY");
        }
    }

    private void HandleInput() {
        inputData = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
    }

    private void UpdateHunger() {
        hungerLevel = Mathf.Clamp(hungerLevel, 0, maxHungerLevel);
    }

    private void UpdateAnimator() {
        anim.SetBool("hasPollen", hasPollen);
        anim.SetBool("hasNectar", hasNectar);
    }

    private void UpdateVariables() {
        accelerationRate = maxMoveSpeed / timeZeroToMax;
        decelerationRate = accelerationRate * 2;
    }

    private void CalculateSpeed() {
        if (Mathf.Abs(inputData.magnitude) > 0) {
            currentMoveSpeed += accelerationRate * Time.fixedDeltaTime;
        } else if (inputData.magnitude == 0) {
            currentMoveSpeed -= decelerationRate * Time.fixedDeltaTime;
        }

        currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, 0, maxMoveSpeed);
    }
    private void FlipPlayer(float horizontalMovement) {
        if (horizontalMovement > 0) {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        } else if (horizontalMovement < 0) {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    //add Nectar object as parameter to keep track of the info later on
    public void AddNectar() {
        hasNectar = true;
    }

    public void AddPollen() {
        hasPollen = true;
    }

    private void Love() {
        print("I love you");
    }
}
