using UnityEngine;
using TS.PageSlider;
using UnityEngine.UI;
using System.Threading.Tasks;
public class DotsImageController : MonoBehaviour
{
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _inActiveColor;
    private PageDot _pageDot;
    private Image _image;
    private void Awake()
    {
        _pageDot = GetComponent<PageDot>();
        _image = GetComponent<Image>();

        _pageDot.OnActiveStateChanged.AddListener(ChangeState);
    }
    private void Start()
    {
        #region Kostil
        //Posivay Kostil neactivnosti dlya PageDots
        ChangeState();
        #endregion
        _image.color = _inActiveColor;
    }
    public void ChangeState(bool isActive)
    {
        _image.color = isActive ? _activeColor : _inActiveColor;
    }
    private async void ChangeState()
    {
        await Task.Delay(100);
        _pageDot.ChangeActiveState(false);
    }
}
