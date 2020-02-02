using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScrapCostsPresenter : MonoBehaviour
{
	public Text TowerCostText;
	public Text WallCostText;

	[Inject]
	public RepairCosts Costs { private get; set; }

	private void Start()
	{
		TowerCostText.text = $"{Costs.Tower} x";
		WallCostText.text = $"{Costs.Wall} x";
	}
}
