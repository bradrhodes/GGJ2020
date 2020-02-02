using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScorePresenter : MonoBehaviour
{
	public Text ScoreText;

	[Inject]
	public EnemiesAggregate Enemies { private get; set; }

	private void Start()
	{
		ScoreText.text = "0";

		Enemies.Events
			.OfType<EnemiesEvent, EnemiesEvent.EnemyDestroyed>()
			.Subscribe(destroyed => ScoreText.text = destroyed.TotalDestroyed.ToString());
	}
}
