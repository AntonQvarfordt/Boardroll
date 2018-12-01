using DG.Tweening;
using UnityEngine;
public class MenuPanel : MonoBehaviour
{
	public float TransitionTime = 1.5f;
	public bool Showing;
	private CanvasGroup _canvasGroup;

	public GameObject[] VisualElements;

	private void Awake()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
	}

	private void Start()
	{
		//Invoke("Show" , 2);
	}

	private void Update()
	{
		//if (Input.GetKeyDown("q"))
		//{
		//	Hide();
		//}
		//if (Input.GetKeyDown("e"))
		//{
		//	Show();
		//}
	}

	public void Show()
	{
		ShowPanel();
		Showing = true;
	}

	private void ShowPanel()
	{
		_canvasGroup.DOFade(1 , TransitionTime / 2).OnComplete(ShowElements);
	}
	private void ShowElements()
	{
		var elementTransitionTime = TransitionTime / 2;
		var perElementTime = elementTransitionTime / VisualElements.Length;
		for (int i = 0; i < VisualElements.Length; i++)
		{
			var element = VisualElements[i];
			element.transform.localScale = Vector3.zero;
			element.SetActive(true);
			element.transform.DOScale(Vector3.one , perElementTime).SetDelay(perElementTime * i).SetEase(Ease.OutBack);
		}
	}

	public void Hide()
	{
		HideElements();
		Showing = false;
	}

	private void HidePanel()
	{
		_canvasGroup.DOFade(0 , TransitionTime / 2);
	}
	private void HideElements()
	{
		var elementTransitionTime = TransitionTime / 2;
		var perElementTime = elementTransitionTime / VisualElements.Length;
		for (int i = 0; i < VisualElements.Length; i++)
		{
			var element = VisualElements[i];
			//element.transform.localScale = Vector3.zero;

			if (i == (VisualElements.Length - 1))
				element.transform.DOScale(Vector3.zero , perElementTime).SetDelay(perElementTime * i).OnComplete(HidePanel).SetEase(Ease.InBack);
			else
				element.transform.DOScale(Vector3.zero , perElementTime).SetDelay(perElementTime * i).SetEase(Ease.InBack);
		}
	}

	private void DisableElements()
	{
		foreach (GameObject element in VisualElements)
		{
			element.SetActive(false);
		}
	}
}
