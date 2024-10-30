using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public Slider volumeSlider;
    public AudioClip soundEffect;
    private bool isSliderBeingMoved = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // If AudioSource is not found, log an error message
            Debug.LogError("AudioSource component not found on the GameObject. Please attach an AudioSource component.");
        }
    }

    private void Start()
    {
        if (volumeSlider != null)
        {
            volumeSlider.value = 1.0f;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

            // Add listener for the slider's OnPointerUp event
            volumeSlider.onValueChanged.AddListener(OnSliderPointerUp);
        }
        else
        {
            Debug.LogError("Slider component not assigned to the AudioManager script. Please assign the Slider component in the Unity Editor.");
        }
    }

    private void OnVolumeChanged(float volume)
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
}