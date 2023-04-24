using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class CustomButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool _isPushed = false;

    public UnityAction OnClickCallback;
    private CanvasGroup _canvasGroup;

    void Awake()
    {
        DOTween.Init();
    }
    
    private void Start()
    {
        _canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        ButtonActive(true);
    }

    public void ButtonActive(bool active)
    {
        _isPushed = !active;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnButtonClick();
        OnClickCallback?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isPushed) return;
        OnButtonDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_isPushed) return;
        OnButtonUp();
    }

    private void OnButtonDown()
    {
        // Down時の共通処理
        transform.DOScale(0.9f, 0.3f).SetEase(Ease.OutElastic).SetLink(gameObject);
        _canvasGroup.DOFade(0.8f, 0.3f).SetEase(Ease.InCubic).SetLink(gameObject);
    }

    private void OnButtonUp()
    {
        // Up時の共通処理
        transform.DOScale(1f, 0.3f).SetEase(Ease.OutElastic).SetLink(gameObject);
        _canvasGroup.DOFade(1f, 0.3f).SetEase(Ease.OutCubic).SetLink(gameObject);
    }

    private void OnButtonClick()
    {
        // Click時の共通処理（SE鳴らすなど）
    }
}