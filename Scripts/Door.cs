using UnityEngine;

public class Door : MonoBehaviour
{
    private readonly int OpenTrigger = Animator.StringToHash("Open");

    [SerializeField] private Animator _animator;

    public bool isOpen;

    public void Open()
    {
        _animator.SetTrigger(OpenTrigger);
        isOpen = true;
    }
}
