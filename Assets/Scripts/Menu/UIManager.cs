using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	public static UIManager _instance;
	public Player player;
	public GameObject game;
	public PauseMenu pauseMenu;
	public GameObject gameUI;
	public GameObject[] hideInMapView;
	public MapMenu map;
	public Compass compass;
	public CanvasGroup hudGroup;
	public Toggle invertInputToggle;

	public float smoothT;
	float smoothV;

	public GameObject _mainMenuGO;
	public GameObject _howToPlayGO;
	public List<Image> _howToPlayImages;
	private int _currentHowToPlay = 0;

	void Awake()
	{
		hudGroup.alpha = 0;
		_instance = this;
	}
	
	public void PreviousImages()
	{
		_currentHowToPlay--;
		if (_currentHowToPlay < 0)
			_currentHowToPlay = 0;

		for (int i = 0; i < _howToPlayImages.Count; i++)
		{
			_howToPlayImages[i].enabled = false;
		}
		_howToPlayImages[_currentHowToPlay].enabled = true;
	}

	public void NextImages()
	{
		_currentHowToPlay++;
		if (_currentHowToPlay > _howToPlayImages.Count - 1)
			_currentHowToPlay = _howToPlayImages.Count - 1;

		for (int i = 0; i < _howToPlayImages.Count; i++)
		{
			_howToPlayImages[i].enabled = false;
		}
		_howToPlayImages[_currentHowToPlay].enabled = true;
	}

	public void CloseHowToPlay()
	{
		_howToPlayGO.SetActive(false);
		_mainMenuGO.SetActive(true);
	}
	
	void Update()
	{
		bool uiIsActive = GameController.IsState(GameState.Playing) || GameController.IsState(GameState.ViewingMap);

		hudGroup.alpha = Mathf.SmoothDamp(hudGroup.alpha, uiIsActive ? 1 : 0, ref smoothV, smoothT);
	}

	public void ToggleMap()
	{
		if (GameController.IsAnyState(GameState.Playing, GameState.ViewingMap))
		{
			ToggleMapDisplay();
		}
	}

	public void TogglePause()
	{
		if (GameController.IsAnyState(GameState.Playing, GameState.ViewingMap, GameState.Paused))
		{
			pauseMenu.TogglePauseMenu();
		}
	}


	public void ToggleMapDisplay()
	{
		bool showMap = map.ToggleActive(player);
		if (showMap)
		{
			GameController.SetState(GameState.ViewingMap);
		}
		else
		{
			GameController.SetState(GameState.Playing);
		}

		Seb.Helpers.GameObjectHelper.SetActiveAll(!showMap, hideInMapView);
	}
}
