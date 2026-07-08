using System;
using UnityEngine;

/// <summary>
/// This class holds the settings for a single slide show slide.
/// </summary>
/// <remarks>
/// Implements ISerializationCallbackReceiver so default values are set after Unity serialization.
/// </remarks>
[Serializable]
public class Slide : ISerializationCallbackReceiver
{
    [Tooltip("This is how long (in seconds) that this slide will be displayed for.")]
    [SerializeField]
    private float _displayTime;

    [Tooltip("The background color of this slide.")]
    [SerializeField]
    private Color _backgroundColor;

    [Header("Image Options")]
    [Tooltip("The image displayed by this slide. If you can't drag the image in, go to its import settings and make sure it is set to \"Sprite (2D and UI)\".")]
    [SerializeField]
    private Sprite _image;

    [Tooltip("This sets how the image is aligned on the screen.")]
    [SerializeField]
    private TextAnchor _ImageScreenAlignment;

    [Tooltip("This sets how the image is displayed.")]
    [SerializeField]
    private ImageDisplayModes _imageDisplayMode;

    [Tooltip("Whether or not to preserve the aspect ratio of the image. This is useful when ImageDisplayMode is set to modes like Stretch Full Screen or Custom Size")]
    [SerializeField]
    private bool _preserveAspectRatio;

    [Tooltip("This is the image size used when ImageDisplayMode is set to one of the custom size options.")]
    [SerializeField]
    private Vector2 _customImageDisplaySize;

    [Header("Transition Options")]
    [Tooltip("Specifies the type of transition between this slide and the next one.")]
    [SerializeField]
    private TransitionTypes _transitionType;

    [Tooltip("This allows you to override the TransitionDuration setting of the parent slide show. A negative value means this slide will use the slide show's default transition duration.")]
    [SerializeField]
    private float _transitionDurationOverride;

    [Tooltip("This allows you to override the FadeInTime setting of the parent slide show. A negative value means this slide will use the slide show's default fade in time.")]
    [SerializeField]
    private float _fadeInTimeOverride;

    [Tooltip("This allows you to override the FadeOutTime setting of the parent slide show. A negative value means this slide will use the slide show's default fade out time.")]
    [SerializeField]
    private float _fadeOutTimeOverride;

    [Header("Layout / Styling (Comic Panel)")]
    [Tooltip("Local position offset for this frame relative to its default position (pixels).")]
    [SerializeField]
    private Vector2 _positionOffset;

    [Tooltip("Scale multiplier for this frame (1 = native).")]
    [SerializeField]
    private Vector2 _scale = Vector2.one;

    [Tooltip("Rotation (degrees) to apply to this frame.")]
    [SerializeField]
    private float _rotation;

    [Tooltip("Padding around the frame (left, right, top, bottom) in pixels.")]
    [SerializeField]
    private Vector4 _padding; // left, right, top, bottom

    [Tooltip("Border thickness in pixels.")]
    [SerializeField]
    private float _borderThickness;

    [Tooltip("Border color for the panel.")]
    [SerializeField]
    private Color _borderColor;

    [Tooltip("Enable drop shadow for the panel.")]
    [SerializeField]
    private bool _shadowEnabled;

    [Tooltip("Shadow offset in pixels.")]
    [SerializeField]
    private Vector2 _shadowOffset;

    [Tooltip("Shadow color.")]
    [SerializeField]
    private Color _shadowColor;

    [Tooltip("If true, adapt layout to safe areas / screen size.")]
    [SerializeField]
    private bool _responsive = true;

    [Tooltip("If true, lock the image aspect ratio when resizing.")]
    [SerializeField]
    private bool _lockAspectRatio = true;

    public void OnBeforeSerialize()
    {
        if (_backgroundColor == new Color(0, 0, 0, 0))
        {
            _displayTime = 5f;
            _backgroundColor = Color.black;

            _imageDisplayMode = ImageDisplayModes.StretchFullScreen;
            _ImageScreenAlignment = TextAnchor.MiddleCenter;
            _preserveAspectRatio = true;
            _customImageDisplaySize = new Vector2(100, 100);

            _transitionType = TransitionTypes.SimpleFadeOutThenFadeIn;
            _transitionDurationOverride = -1f;

            _fadeInTimeOverride = -1f;
            _fadeOutTimeOverride = -1f;

            // Layout defaults
            _positionOffset = Vector2.zero;
            _scale = Vector2.one;
            _rotation = 0f;
            _padding = new Vector4(8f, 8f, 8f, 8f);
            _borderThickness = 2f;
            _borderColor = Color.black;
            _shadowEnabled = false;
            _shadowOffset = new Vector2(4f, -4f);
            _shadowColor = new Color(0, 0, 0, 0.5f);
            _responsive = true;
            _lockAspectRatio = true;
        }
    }

    public void OnAfterDeserialize()
    {
    }

    public Color BackgroundColor { get { return _backgroundColor; } }
    public float DisplayTime { get { return _displayTime; } }

    public Sprite Image { get { return _image; } }
    public ImageDisplayModes ImageDisplayMode { get { return _imageDisplayMode; } }
    public TextAnchor ImageScreenAlignment { get { return _ImageScreenAlignment; } }
    public bool PreserveAspectRatio { get { return _preserveAspectRatio; } }
    public Vector2 CustomImageSize { get { return _customImageDisplaySize; } }

    public TransitionTypes TransitionType { get { return _transitionType; } }
    public float TransitionDurationOverride { get { return _transitionDurationOverride; } }
    public float FadeInTimeOverride { get { return _fadeInTimeOverride; } }
    public float FadeOutTimeOverride { get { return _fadeOutTimeOverride; } }

    // Layout / styling properties
    public Vector2 PositionOffset { get { return _positionOffset; } }
    public Vector2 Scale { get { return _scale; } }
    public float Rotation { get { return _rotation; } }
    public Vector4 Padding { get { return _padding; } } // left, right, top, bottom
    public float BorderThickness { get { return _borderThickness; } }
    public Color BorderColor { get { return _borderColor; } }
    public bool ShadowEnabled { get { return _shadowEnabled; } }
    public Vector2 ShadowOffset { get { return _shadowOffset; } }
    public Color ShadowColor { get { return _shadowColor; } }
    public bool Responsive { get { return _responsive; } }
    public bool LockAspectRatio { get { return _lockAspectRatio; } }

    public bool IsCustomSizeImage
    {
        get
        {
            return _imageDisplayMode == Slide.ImageDisplayModes.CustomSize;
        }
    }

    public bool IsFullscreenImage
    {
        get
        {
            return _imageDisplayMode == Slide.ImageDisplayModes.StretchFullScreen;
        }
    }

    public enum FadeType
    {
        FadeIn,
        FadeOut,
    }

    public enum ImageDisplayModes
    {
        NativeSize,
        StretchFullScreen,
        CustomSize,
    }

    public enum TransitionTypes
    {
        None,
        SimpleFadeOutThenFadeIn,
        CustomFadeOutThenFadeIn,

        // New transitions
        ZoomInZoomOut,
        SlideLeft,
        SlideRight,
        SlideUp,
        SlideDown,
        RotateClockwise,
        RotateCounterClockwise,
    }
}
