using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PageController : MonoBehaviour
{
    [SerializeField] private BubbleController _bubbleController;
    [SerializeField] private List<GameObject> _pages;
    [SerializeField] private float _speedAlphaChange = 1;
    private Coroutine _coroutine;
    private float _tLerp;
    public void OpenPhotoBubble(int index)
    {
        _bubbleController.gameObject.SetActive(true);
        _bubbleController.OpenBubble(index);
    }
    public void OpenPage(int index)
    {
        foreach (var page in _pages)
            page.SetActive(false);
        _pages[index].SetActive(true);

        _tLerp = 0;
        _coroutine = StartCoroutine(SetAlpha(index, 1));
    }

    public async void CLoseAllPages()
    {
        for (int i = 0; i < _pages.Count; i++)
        {
            if (_pages[i].activeInHierarchy)
            {
                _tLerp = 0;
                _coroutine = StartCoroutine(SetAlpha(i, 0));
                await Task.Delay(500);
                _pages[i].SetActive(false);
            }
        }
    }
    public void CloseBubble()
    {
        if (_bubbleController.gameObject.activeInHierarchy)
            _bubbleController.CloseBubble();
    }
    private IEnumerator SetAlpha(int pageIndex, float alpha)
    {
        Debug.Log("set Alpha " + alpha);
        var canvasGroup = _pages[pageIndex].GetComponent<CanvasGroup>();
        if (canvasGroup.alpha != alpha)
        {
            _tLerp += Time.fixedDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, alpha, _tLerp);
            yield return new WaitForFixedUpdate();
            StartCoroutine(SetAlpha(pageIndex, alpha));
        }
        else
            _coroutine = null;
    }
}
