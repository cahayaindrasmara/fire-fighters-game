using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f,1f)] private float currentIntensity =1.0f;
    private float[] startIntensities = new float[0];

    [SerializeField] private ParticleSystem [] fireParticleSystems = new ParticleSystem[0];
    [SerializeField] private AudioSource fireAudioSource;
    [SerializeField] private AudioClip fireSound;


    // Start is called before the first frame update
    void Start()
    {
        startIntensities = new float[fireParticleSystems.Length];

        for (int i = 0; i < fireParticleSystems.Length; i++) {
            startIntensities[i] =  fireParticleSystems[i].emission.rateOverTime.constant;
        }

        if (fireAudioSource != null && fireSound != null)
        {
            fireAudioSource.clip = fireSound;
            fireAudioSource.loop = true;
            fireAudioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeIntensity();
    }

    

    // private void ChangeIntensity() {
    //     for (int i = 0; i < fireParticleSystems.Length; i++) {
    //         var emission = fireParticleSystems[i].emission;
    //         emission.rateOverTime = currentIntensity * startIntensities[i];

    //     }
    // }

    private void ChangeIntensity() {
        for (int i = 0; i < fireParticleSystems.Length; i++) {
            var emission = fireParticleSystems[i].emission;
            var rate = emission.rateOverTime;
            rate.constant = currentIntensity * startIntensities[i];
            emission.rateOverTime = rate;

            // Optional: matikan jika 0
            if (currentIntensity <= 0f && fireParticleSystems[i].isPlaying)
                fireParticleSystems[i].Stop();
        }

        if (fireAudioSource != null)
        {
            fireAudioSource.volume = currentIntensity; // Semakin padam, suara mengecil
        }

    }

    public void ReduceIntensity(float amount)
    {
        currentIntensity = Mathf.Clamp01(currentIntensity - amount);
    }

    public bool IsBurning()
    {
        return currentIntensity > 0f;
    }
}
