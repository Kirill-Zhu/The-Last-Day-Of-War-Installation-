using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
public class BubbleController : MonoBehaviour
{
    [SerializeField] private List<Sprite> _photos;
    [SerializeField] private Image _imageWithPhoto;
    private Animator _animator;
    private Sprite _currentSprite;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public async void CloseBubble()
    {
        _animator.SetTrigger("Close");
        await Task.Delay(100);
        while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Close"))
        {
            Debug.Log("Close Animation Playeng");

            await Task.Yield();
        }
        this.gameObject.SetActive(false);
    }
    public async void OpenBubble(int indexPhoto)
    {
        if (_animator != null)
            _animator.SetTrigger("Open");

        _currentSprite = _photos[indexPhoto];
        _imageWithPhoto.sprite = _currentSprite;
        _imageWithPhoto.SetNativeSize();

        await Task.Delay(100);

        while (_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            Debug.Log("Open Animation Playeng");
            await Task.Yield();
        }

    }

}
