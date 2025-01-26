using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderController : MonoBehaviour
{
    public Slider hitBar; // Asigna el Slider desde el Inspector
    public float duration = 5f; // Duración para ir de 0 a 100 y de 100 a 0
    private bool increasing = true; // Controla la dirección del slider
    public BattleController Bc;

    void Start()
    {
        hitBar.maxValue = 100f; // Establece el valor máximo del slider
        hitBar.value = 0f; // Inicializa el slider en 0
        StartCoroutine(UpdateSlider());
    }

    private IEnumerator UpdateSlider()
    {
        while (true) // Bucle infinito
        {
            if (increasing)
            {
                // Incrementar el slider de 0 a 100
                for (float t = 0; t <= duration; t += Time.deltaTime)
                {
                    hitBar.value = Mathf.Lerp(0, 100, t / duration);
                    yield return null; // Espera un frame
                }
                increasing = false; // Cambia a decrementar
            }
            else
            {
                // Decrementar el slider de 100 a 0
                for (float t = 0; t <= duration; t += Time.deltaTime)
                {
                    hitBar.value = Mathf.Lerp(100, 0, t / duration);
                    yield return null; // Espera un frame
                }
                increasing = true; // Cambia a incrementar
            }
        }
    }

    void Update()
    {
        // Al pulsar la barra espaciadora, devuelve el valor actual
        if (Input.GetKeyDown(KeyCode.Space) && Bc.CombatEnabled && ! Bc.EnTexto)
        {
            float result = hitBar.value;
            int resultInt = (int)result;
            GetComponent<BattleController>().atacking(resultInt);
            GetComponent<BattleController>().InitDefense();
            GetComponent<BattleController>().SliderHit.SetActive(false);            
            
        }
    }
}
