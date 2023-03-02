using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFuel : MonoBehaviour
{
    [SerializeField] private float minDestroyTime = 3f, maxDestroyTime = 8f;
    [SerializeField] private float healthValue = 20f;

    private void Start()
    {
        Invoke("RemoveFuelFromPlay", Random.Range(minDestroyTime, maxDestroyTime));
    }

    private void OnDisable()
    {
        CancelInvoke("RemoveFuelFromPlay");
    }

    private void RemoveFuelFromPlay()
    {
        Destroy(gameObject);
    }

    public float GetHealthValue()
    {
        return healthValue;
    }
}
