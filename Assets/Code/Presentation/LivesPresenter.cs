using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LivesPresenter : MonoBehaviour
{
	public Text LivesText;

	[Inject]
	public GameStatusAggregate GameStatus { private get; set; }

	private void Start()
	{
		GameStatus.Events
			.OfType<GameStatusEvent, GameStatusEvent.LivesInitialized>()
			.Subscribe(initialized => LivesText.text = initialized.Lives.ToString());

		GameStatus.Events
			.OfType<GameStatusEvent, GameStatusEvent.LifeLost>()
			.Subscribe(lifeLost => LivesText.text = lifeLost.Lives.ToString());
	}
}
