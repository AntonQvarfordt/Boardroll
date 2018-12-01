using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class UIManager : MonoBehaviour {

	public static UIManager Instance;
	public Image ScreenOverlay;

	public void Awake()
	{
		Invoke("FadeIn" , 3);
	}

	public void FadeToBlack()
	{
		ScreenOverlay.DOFade(1 , 2f);
	}

	public void FadeIn()
	{
		ScreenOverlay.DOFade(0 , 2f);
	}

}
