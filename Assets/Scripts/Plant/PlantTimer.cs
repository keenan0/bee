using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantTimer : MonoBehaviour {
    [SerializeField]
    PlantCollect collect;
    
    [SerializeField]
    Transform pollenFillBar;
    Slider pollenSlider;

    [SerializeField]
    Transform nectarFillBar;
    Slider nectarSlider;

    [SerializeField]
    float pollenSecretionTime = 30.0f;
    float currentPollenTime;

    [SerializeField]
    float nectarSecretionTime = 60.0f;
    float currentNectarTime;

    [SerializeField]
    float maxSecretionTime = 120.0f;

    private void Awake() {
        collect = transform.Find("collect_zone").GetComponent<PlantCollect>();

        nectarSecretionTime = Random.Range(1.0f, maxSecretionTime);
        pollenSecretionTime = Random.Range(1.0f, maxSecretionTime);

        pollenSlider = pollenFillBar.GetComponent<Slider>();
        nectarSlider = nectarFillBar.GetComponent<Slider>();

        pollenSlider.maxValue = pollenSecretionTime;
        nectarSlider.maxValue = nectarSecretionTime;

        ResetNectar();
        ResetPollen();

        UpdateSliderValues();
    }

    private void FixedUpdate() {
        UpdateSecretionTimes();
        UpdateSliderValues();
    }

    private void UpdateSliderValues() {
        pollenSlider.value = pollenSecretionTime - currentPollenTime;
        nectarSlider.value = nectarSecretionTime - currentNectarTime;
    }
    private void UpdateSecretionTimes() {
        currentPollenTime -= Time.fixedDeltaTime;
        currentNectarTime -= Time.fixedDeltaTime;

        if (currentNectarTime <= 0) {
            currentNectarTime = 0.0f;
            collect.secretedNectar = true;
        }

        if (currentPollenTime <= 0) {
            currentPollenTime = 0.0f;
            collect.secretedPollen = true;
        }
    }

    public void ResetPollen() {
        currentPollenTime = pollenSecretionTime;
    }

    public void ResetNectar() {
        currentNectarTime = nectarSecretionTime;
    }
}
