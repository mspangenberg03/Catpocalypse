using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;


public class SlideShowUI : MonoBehaviour
{
    public event EventHandler OnNextSlideInput;
    public event EventHandler OnSkipSlideInput;
    public event EventHandler OnTriggerSlideChange;


    [Tooltip("This sets how frequently we will respond to button/key presses.")]
    [SerializeField, Min(0.5f)] private float _inputDelay = 1.0f;

    [Tooltip("This is the Image UI component that is used to display the current slide image in non-fullscreen modes.")]
    [SerializeField] private Image _slideImage;

    [Tooltip("This is the Image UI component that is used to display the background behind the slide image.")]
    [SerializeField] private Image _slideImageBackground;

    [Tooltip("This is the Image UI component that is used to fade the screen in and out.")]
    [SerializeField] private Image _screenFader;


    [SerializeField] private Transform _ButtonsContainer;
    [SerializeField] private Button _nextSlideButton;
    [SerializeField] private Button _skipSlideShowButton;
    [SerializeField] private Button _continueButton;

    [Header("Zoom Transition Easing")]
    [SerializeField] private AnimationCurve _zoomOutCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private AnimationCurve _zoomInCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);


    IDisposable _inputListener;

    private float _inputTimer = 0f;

    private bool _mouseIsOverAnyButton;


    private void OnEnable()
    {
        if (_inputListener == null)
            _inputListener = InputSystem.onAnyButtonPress.Call(OnAnyButtonPressed);
    }

    private void OnDisable()
    {
        _inputListener?.Dispose();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetButtons();
    }

    // Update is called once per frame
    void Update()
    {
        _inputTimer += Time.deltaTime;
    }

    /// <summary>
    /// This function is called to initiate a transition from one slide to the next. The transition is determined by the TransitionType property of the current slide.
    /// </summary>
    public void StartSlideTransition(SlideShowStateInfo stateInfo)
    {
        if (IsTransitioning)
        {
            Debug.LogError("SlideShowUI.StartSlideTransition() was called while a slide transition is already in progress!");
            return;
        }


        if (stateInfo.CurrentSlide.TransitionType != Slide.TransitionTypes.None)
        {
            StartCoroutine(DoSlideTransition(stateInfo));
        }
        else
        {
            // Trigger the player to change to the next slide.
            OnTriggerSlideChange?.Invoke(this, EventArgs.Empty);

            _screenFader.gameObject.SetActive(false);
        }
    }

    private IEnumerator DoSlideTransition(SlideShowStateInfo stateInfo)
    {
        float duration = stateInfo.GetCurrentSlideTransitionDuration();

        IsTransitioning = true;


        switch (stateInfo.CurrentSlide.TransitionType)
        {
            case Slide.TransitionTypes.SimpleFadeOutThenFadeIn:
                yield return StartCoroutine(DoSlideTransition_SimpleFadeOutThenFadeIn(duration));
                break;

            case Slide.TransitionTypes.CustomFadeOutThenFadeIn:
                yield return StartCoroutine(DoSlideTransition_CustomFadeOutThenFadeIn(stateInfo));
                break;

            case Slide.TransitionTypes.ZoomInZoomOut:
                yield return StartCoroutine(DoSlideTransition_Zoom(stateInfo));
                break;

            default:
                yield return StartCoroutine(DoSlideTransition_SimpleFadeOutThenFadeIn(duration));
                break;

        } // end switch


        IsTransitioning = false;
    }

    public IEnumerator DoSlideTransition_SimpleFadeOutThenFadeIn(float duration)
    {
        float fadeTime = duration / 2f;

        // Fade out the current slide.
        yield return StartCoroutine(DoFade(Slide.FadeType.FadeOut, fadeTime));

        // Trigger the player to change to the next slide.
        OnTriggerSlideChange?.Invoke(this, EventArgs.Empty);

        // Fade in the next slide.
        yield return StartCoroutine(DoFade(Slide.FadeType.FadeIn, fadeTime));
    }

    public IEnumerator DoSlideTransition_CustomFadeOutThenFadeIn(SlideShowStateInfo stateInfo)
    {
        float fadeOutTime = stateInfo.GetCurrentSlideFadeOutTime();
        float fadeInTime = stateInfo.GetNextSlideFadeInTime();


        // Fade out the current slide.
        yield return StartCoroutine(DoFade(Slide.FadeType.FadeOut, fadeOutTime));

        // Trigger the player to change to the next slide.
        OnTriggerSlideChange?.Invoke(this, EventArgs.Empty);

        // Fade in the next slide.
        yield return StartCoroutine(DoFade(Slide.FadeType.FadeIn, fadeInTime));
    }

    /// <summary>
    /// Zoom transition: zoom-out current slide while optionally slightly rotating, swap slide, then zoom-in next slide.
    /// Uses the current slide's transition duration (split evenly).
    /// Respects SlideShow.UseUnscaledTime.
    /// </summary>
    private IEnumerator DoSlideTransition_Zoom(SlideShowStateInfo stateInfo)
    {
        // Guard: make sure we have a slide image rect transform
        RectTransform rt = _slideImage.GetComponent<RectTransform>();
        if (rt == null)
        {
            // Fallback to simple fade
            float d = stateInfo.GetCurrentSlideTransitionDuration();
            yield return StartCoroutine(DoSlideTransition_SimpleFadeOutThenFadeIn(d));
            yield break;
        }

        float duration = stateInfo.GetCurrentSlideTransitionDuration();
        float half = Mathf.Max(0.01f, duration / 2f);

        bool useUnscaled = stateInfo.SlideShow != null && stateInfo.SlideShow.UseUnscaledTime;

        // Capture starting transform state
        Vector3 originalScale = rt.localScale;
        Quaternion originalRotation = rt.localRotation;
        Vector3 originalPos = rt.anchoredPosition3D;

        // Parameters for the visual zoom - conservative defaults.
        float outScaleFactor = 0.85f;
        float inScaleFactor = 1.05f;

        Slide nextSlide = stateInfo.NextSlide;
        if (nextSlide != null)
        {
            if (nextSlide.Scale != Vector2.zero)
            {
                inScaleFactor = Mathf.Max(0.001f, (nextSlide.Scale.x + nextSlide.Scale.y) * 0.5f);
            }
        }

        // OUT: zoom out
        float startTime = useUnscaled ? Time.unscaledTime : Time.time;
        float elapsed = 0f;
        while (elapsed < half)
        {
            elapsed = (useUnscaled ? Time.unscaledTime : Time.time) - startTime;
            float t = Mathf.Clamp01(elapsed / half);
            float eased = _zoomOutCurve != null ? _zoomOutCurve.Evaluate(t) : (1f - Mathf.Pow(1f - t, 3f));

            rt.localScale = Vector3.Lerp(originalScale, originalScale * outScaleFactor, eased);
            rt.localRotation = Quaternion.Slerp(originalRotation, originalRotation * Quaternion.Euler(0f, 0f, 6f * eased), eased);

            yield return null;
        }

        rt.localScale = originalScale * outScaleFactor;
        rt.localRotation = originalRotation * Quaternion.Euler(0f, 0f, 6f);

        // Trigger the player to change to the next slide.
        OnTriggerSlideChange?.Invoke(this, EventArgs.Empty);

        // Small frame to ensure UI update
        yield return null;

        // Incoming
        Slide displayedSlide = nextSlide ?? stateInfo.CurrentSlide;
        Vector3 incomingTargetScale = originalScale * (displayedSlide.Scale != Vector2.zero ? (displayedSlide.Scale.x + displayedSlide.Scale.y) * 0.5f : 1f);
        Quaternion incomingTargetRotation = originalRotation * Quaternion.Euler(0f, 0f, displayedSlide.Rotation);

        rt.localScale = incomingTargetScale * inScaleFactor;
        rt.localRotation = incomingTargetRotation * Quaternion.Euler(0f, 0f, 12f);

        // IN: zoom in
        startTime = useUnscaled ? Time.unscaledTime : Time.time;
        elapsed = 0f;
        while (elapsed < half)
        {
            elapsed = (useUnscaled ? Time.unscaledTime : Time.time) - startTime;
            float t = Mathf.Clamp01(elapsed / half);
            float eased = _zoomInCurve != null ? _zoomInCurve.Evaluate(t) : Mathf.Pow(t, 2f);

            rt.localScale = Vector3.Lerp(incomingTargetScale * inScaleFactor, incomingTargetScale, eased);
            rt.localRotation = Quaternion.Slerp(incomingTargetRotation * Quaternion.Euler(0f, 0f, 12f), incomingTargetRotation, eased);

            yield return null;
        }

        rt.localScale = incomingTargetScale;
        rt.localRotation = incomingTargetRotation;
        rt.anchoredPosition3D = originalPos;

        yield break;
    }

    public IEnumerator DoFade(Slide.FadeType fadeType, float fadeDuration)
    {
        IsTransitioning = true;

        _screenFader.gameObject.SetActive(true);


        float fadeStartTime = Time.time;
        float elapsedTime = 0f;
        while (elapsedTime <= fadeDuration)
        {
            // Calculate how far through the fade we are.
            elapsedTime = (Time.time - fadeStartTime);
            float percentComplete = elapsedTime / fadeDuration;

            AdjustScreenFaderAlpha(fadeType, percentComplete);

            // Wait one frame.
            yield return null;

        } // end while


        AdjustScreenFaderAlpha(fadeType, 1.0f);


        if (fadeType == Slide.FadeType.FadeIn)
            _screenFader.gameObject.SetActive(false);


        IsTransitioning = false;
    }

    private void AdjustScreenFaderAlpha(Slide.FadeType fadeType, float slideShowPercentComplete)
    {
        int alpha = Mathf.Clamp(Mathf.RoundToInt(255f * slideShowPercentComplete), 0, 255);

        Color32 color = _screenFader.color;
        color.a = fadeType == Slide.FadeType.FadeOut ? (byte)alpha : (byte)(255 - alpha);
        _screenFader.color = color;
    }

    public void ResetSlideDisplay(Color fadeColor)
    {
        _screenFader.color = fadeColor;
        _screenFader.gameObject.SetActive(true);

        ResetButtons();

        // Enable the slide show UI canvas.
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Disables the slide show UI. It is automatically re-enabled when a cut scene starts playing.
    /// </summary>
    public void DisableSlideShowDisplay()
    {
        _slideImage.gameObject.SetActive(false);
        _screenFader.gameObject.SetActive(false);
        EnableSlideDisplay(false);

        // Disable the canvas.
        gameObject.SetActive(false);
    }

    public void EnableSlideDisplay(bool state)
    {
        _slideImageBackground.gameObject.SetActive(state);
        _slideImage.gameObject.SetActive(state);
        _ButtonsContainer.gameObject.SetActive(state);
    }

    public void UpdateSlideDisplay(Slide slide)
    {
        _slideImageBackground.color = slide.BackgroundColor;

        DisplaySlideImage(slide);
    }

    private void DisplaySlideImage(Slide slide)
    {
        bool fullScreen = slide.IsFullscreenImage;
        bool preserveAspectRatio = slide.PreserveAspectRatio;

        _slideImage.preserveAspect = false;
        
        Vector2 imageSize = Vector2.zero;

        if (slide.ImageDisplayMode == Slide.ImageDisplayModes.StretchFullScreen)
        {
            RectTransform parentRectTrans = _slideImage.transform.parent.GetComponent<RectTransform>();
            imageSize.x = parentRectTrans.rect.size.x;
            imageSize.y = parentRectTrans.rect.size.y;
        }

        if (slide.IsCustomSizeImage)
        {
            imageSize.x = slide.CustomImageSize.x;
            imageSize.y = slide.CustomImageSize.y;
        }

        _slideImageBackground.GetComponent<HorizontalLayoutGroup>().childAlignment = slide.ImageScreenAlignment;

        if (slide.ImageDisplayMode == Slide.ImageDisplayModes.NativeSize)
        {
            _slideImage.SetNativeSize();
        }
        else
        {
            _slideImage.GetComponent<RectTransform>().sizeDelta = imageSize;
        }

        _slideImage.preserveAspect = preserveAspectRatio;
        _slideImage.sprite = slide.Image;

        // Apply per-slide transform overrides (position / scale / rotation)
        RectTransform rt = _slideImage.GetComponent<RectTransform>();
        if (rt != null)
        {
            rt.anchoredPosition = slide.PositionOffset;
            float uniformScale = (slide.Scale.x + slide.Scale.y) * 0.5f;
            rt.localScale = Vector3.one * uniformScale;
            rt.localRotation = Quaternion.Euler(0f, 0f, slide.Rotation);
        }
    }

    public void ShowContinueButton()
    {
        _nextSlideButton.gameObject.SetActive(false);
        _skipSlideShowButton.gameObject.SetActive(false);
        _continueButton.gameObject.SetActive(true);
    }

    public void ResetButtons()
    {
        _nextSlideButton.gameObject.SetActive(true);
        _skipSlideShowButton.gameObject.SetActive(true);
        _continueButton.gameObject.SetActive(false);
    }

    private void OnAnyButtonPressed(InputControl control)
    {
        if (_inputTimer >= _inputDelay)
        {
            if (!(control.device.displayName == "Mouse" && _mouseIsOverAnyButton))
            {
                _inputTimer = 0f;
                OnNextSlideInput?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void OnNextSlideButtonClicked()
    {
        OnNextSlideInput?.Invoke(this, EventArgs.Empty);
    }

    public void OnSkipButtonClicked()
    {
        OnSkipSlideInput?.Invoke(this, EventArgs.Empty);
    }
    
    public void OnMouseEnteredButtonArea()
    {
        _mouseIsOverAnyButton = true;
    }

    public void OnMouseExitedButtonArea()
    {
        _mouseIsOverAnyButton = false;
    }

    public bool IsTransitioning { get; private set; }
}
