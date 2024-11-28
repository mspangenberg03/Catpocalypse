using UnityEngine;
using UnityEngine.UIElements;

public abstract class UpgradeCard : MonoBehaviour
{
    public void Start()
    {
        ChangeText();
    }

    protected abstract void ChangeText();

    public abstract bool Upgrade();

}