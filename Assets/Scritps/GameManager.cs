using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PageController _pageController;
    [SerializeField] private GameObject _sleepScreen;
    [SerializeField] private float _sleepModeTime = 60;
    [SerializeField] private float _timer;
    private CanvasGroup _canvasGroup;
    private Coroutine _coroutine;
    private float _tLerp = 0;
    public bool _isInSleepMode;
    private void Start()
    {
        _canvasGroup = _sleepScreen.GetComponent<CanvasGroup>();
    }
    private void Update()
    {
        UpdateTimer();

        if (Input.touchCount > 0 || Input.GetMouseButton(0))
            ResetTimer();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        if (!_isInSleepMode && _timer <= 0)
            SetActiveSleepScreen();

    }
    private void UpdateTimer()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;
    }
    private void ResetTimer()
    {

        _timer = _sleepModeTime;
        if (_isInSleepMode)
            SetInactiveSleepScreen();
    }
    private void SetActiveSleepScreen()
    {
        _isInSleepMode = true;
        _pageController.CLoseAllPages();
        _pageController.CloseBubble();
        _tLerp = 0;
        _sleepScreen.SetActive(true);
        _coroutine = StartCoroutine(SetAlpha(1));
    }

    private async void SetInactiveSleepScreen()
    {
        _isInSleepMode = false;

        _tLerp = 0;
        _coroutine = StartCoroutine(SetAlpha(0));
        await System.Threading.Tasks.Task.Delay(500);
        _sleepScreen.SetActive(false);
    }
    private IEnumerator SetAlpha(float alpha)
    {
        if (_canvasGroup.alpha != alpha)
        {
            _tLerp += Time.fixedDeltaTime;
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, alpha, _tLerp);
            yield return new WaitForFixedUpdate();
            _coroutine = StartCoroutine(SetAlpha(alpha));
        }
    }
}
