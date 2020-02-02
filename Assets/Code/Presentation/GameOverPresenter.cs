using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOverPresenter : MonoBehaviour
{
	public Canvas GameOverCanvas;

	[Inject]
	public GameStatusAggregate GameStatus { private get; set; }

	private void Start()
	{
		GameOverCanvas.enabled = false;

		GameStatus.Events
			.OfType<GameStatusEvent, GameStatusEvent.GameOver>()
			.Subscribe(_ => ShowGameOver());
	}

	private void ShowGameOver()
	{
        SceneManager.LoadScene("GameOver");
	}
}