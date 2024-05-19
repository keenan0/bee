using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCollect : MonoBehaviour {
    [SerializeField]
    PlantTimer timer;
    public bool secretedPollen { get; set; }
    public bool secretedNectar { get; set; }

    bool canCollect;
    bool collected;

    [SerializeField]
    float collectTime = 5.0f;
    float currentCollectTime;

    private void Awake() {
        timer = transform.parent.GetComponent<PlantTimer>();
        currentCollectTime = collectTime;
        canCollect = collected = false;
    }

    private void FixedUpdate() {
        if (canCollect && !collected) {
            currentCollectTime -= Time.fixedDeltaTime;
            Debug.Log(currentCollectTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            if (secretedPollen || secretedNectar) {
                canCollect = true;
                currentCollectTime = collectTime;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player") {
            if (canCollect) {
                if(currentCollectTime <= 0.0f) {
                    collected = true;
                    currentCollectTime = 0.0f;

                    if (secretedNectar) {
                        Debug.Log("collected nectar");
                        timer.ResetNectar();
                        secretedNectar = false;
                    }

                    if (secretedPollen) {
                        Debug.Log("collected pollen");
                        timer.ResetPollen();
                        secretedPollen = false;
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        canCollect = false;
        collected = false;
    }
}
