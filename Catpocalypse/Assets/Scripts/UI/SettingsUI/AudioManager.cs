using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider SFXSlider;
    [SerializeField] private AudioClip soundEffect;

    private bool isSliderBeingMoved = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already a WaveManager in this scene. Self destructing!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // If AudioSource is not found, log an error message
            Debug.LogError("AudioSource component not found on the GameObject. Please attach an AudioSource component.");
        }
    }

    private void Start() { 

        if (masterSlider != null && musicSlider != null && SFXSlider!= null)
        {
          
            musicSlider.value = 1.0f;
            SFXSlider.value = 1.0f;
            masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            SFXSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

            // Add listener for the slider's OnPointerUp event
            masterSlider.onValueChanged.AddListener(OnSliderPointerUp);
            musicSlider.onValueChanged.AddListener(OnSliderPointerUp);
            SFXSlider.onValueChanged.AddListener(OnSliderPointerUp);
        }
        else
        {
            Debug.LogError("Slider component not assigned to the AudioManager script. Please assign the Slider component in the Unity Editor.");
        }
    }

    private void OnMasterVolumeChanged(float volume)
    {
        // Check if audioSource is not null before accessing it
        if (audioSource != null)
        {
            // Set the audio volume based on the slider value
            audioSource.volume = volume;

            // Set the flag to indicate that the slider is being moved
            isSliderBeingMoved = true;
        }
    }

    private void OnMusicVolumeChanged(float volume)
    {
        // Check if audioSource is not null before accessing it
        if (audioSource != null)
        {
            // Set the audio volume based on the slider value
            audioSource.volume = volume;

            // Set the flag to indicate that the slider is being moved
            isSliderBeingMoved = true;
        }
    }

    private void OnSFXVolumeChanged(float volume)
    {
        // Check if audioSource is not null before accessing it
        if (audioSource != null)
        {
            // Set the audio volume based on the slider value
            audioSource.volume = volume;

            // Set the flag to indicate that the slider is being moved
            isSliderBeingMoved = true;
        }
    }

    private void OnSliderPointerUp(float volume)
    {
        // Check if audioSource is not null before accessing it
        if (audioSource != null)
        {
            // Check if the slider is not actively being moved
            if (!isSliderBeingMoved)
            {
                // Play the sound effect when the slider value changes
                if (soundEffect != null)
                {
                    audioSource.PlayOneShot(soundEffect);
                }
            }

            // Reset the flag after the slider is released
            isSliderBeingMoved = false;
        }
    }

    public float MusicVolume { get { return masterSlider.value * musicSlider.value; } }
    public float SFXVolume { get { return masterSlider.value * SFXSlider.value; } }


}