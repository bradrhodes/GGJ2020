using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScrapWalletPresenter : MonoBehaviour
{
	public Text AmountText; 

	[Inject]
	public ScrapWalletAggregate ScrapWallet { private get; set; }

	private void Start()
	{
		ScrapWallet.Events
			.OfType<ScrapWalletEvent, ScrapWalletEvent.Initialized>()
			.Subscribe(initialized => SetAmount(initialized.Amount));

		ScrapWallet.Events
			.OfType<ScrapWalletEvent, ScrapWalletEvent.Decreased>()
			.Subscribe(decreased => SetAmount(decreased.CurrentAmount));
	}

	private void SetAmount(int amount)
	{
		AmountText.text = $"Scrap: {amount}";
	}
}
