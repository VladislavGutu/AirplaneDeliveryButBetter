using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Menu
{

	[Header("References")]
	public Button playButton;
	public Button quitButton;
	public TMPro.TMP_Text version;

	public GameObject mainButtonsHolder;

	[Header("Background Display")]
	public Player player;
	public float playerPitch;
	public float blurRadius = 12;
	public BlurEffect blurEffect;


	void Start()
	{
		version.text = $"Version {Application.version}";

		if (PlayerPrefs.GetInt("FirstEnter",0) == 0)
		{
			SceneManager.LoadScene(0);
		}

		PlayerPrefs.SetInt("FirstEnter", 1);
		
		playButton.onClick.AddListener(PlayGame);
		quitButton.onClick.AddListener(Quit);
		EventSystem.current.SetSelectedGameObject(playButton.gameObject);

	}


	void LateUpdate()
	{
		if (GameController.IsState(GameState.InMainMenu))
		{
			player.SetPitch(playerPitch);
		}
	}


	void PlayGame()
	{
		GameController.StartGame();
		CloseMenu();
	}


	protected override void OnMenuOpened()
	{
		base.OnMenuOpened();
		blurEffect.enabled = true;
		blurEffect.blurRadius = blurRadius;
	}

	protected override void OnMenuClosed()
	{
		base.OnMenuClosed();
		if (Application.isPlaying)
		{
			StartCoroutine(AnimateFadeOutBlur());
		}
	}


	IEnumerator AnimateFadeOutBlur()
	{
		float startBlur = blurEffect.blurRadius;
		float t = 0;
		while (t < 1)
		{
			t += Time.unscaledDeltaTime * 1.5f;
			blurEffect.blurRadius = Mathf.Lerp(startBlur, 0, Seb.Ease.Quadratic.Out(t));
			yield return null;
		}
		blurEffect.enabled = false;
	}


	void Quit()
	{
		GameController.Quit();
	}

	void OnDestroy()
	{
		blurEffect.enabled = false;
	}

}
