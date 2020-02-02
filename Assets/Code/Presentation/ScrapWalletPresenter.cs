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
			.OfType<ScrapWalletEvent, ScrapWalletEvent.AvailableScrapChanged>()
			.Subscribe(changed => SetAmount(changed.CurrentAmount));
	}

	private void SetAmount(int amount)
	{
		AmountText.text = $"Scrap: {amount}";
	}
}
