using UnityEngine;
using UnityEngine.UI;
public class LoadingScreen : Singleton<LoadingScreen>
{
    private AsyncOperation currentLoadingOperation;
    private bool isLoading;

    [SerializeField]
    private Text percentLoadedText;
    [SerializeField]
    private bool hidePercentageText;
    private void Awake()
    {
        Configure();
        Hide();
    }
    private void Configure()
    {
        percentLoadedText?.gameObject.SetActive(!hidePercentageText);
    }
    private void Update()
    {
        if (isLoading)
        {
            SetProgress(currentLoadingOperation.progress);
            if (currentLoadingOperation.isDone)
            {
                Hide();
            }
        }
    }

    private void SetProgress(float progress)
    {
        if (percentLoadedText != null)
            percentLoadedText.text = Mathf.CeilToInt(progress * 100).ToString() + "%";
    }

    public void Show(AsyncOperation loadingOperation)
    {
        gameObject.SetActive(true);
        currentLoadingOperation = loadingOperation;
        SetProgress(0f);
        isLoading = true;
    }
    // Call this to hide it:
    public void Hide()
    {
        // Disable the loading screen:
        gameObject.SetActive(false);
        currentLoadingOperation = null;
        isLoading = false;
    }
}