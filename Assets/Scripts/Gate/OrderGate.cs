using System;
using System.Collections.Generic;
using System.Linq;

public class OrderGate : BaseGate
{
	protected override void OnEnterCustomActions(List<Collectible> collectedCollectibles, float amountOfRise)
	{
		Dictionary<Collectible, int> collectibleToEnumDict = new Dictionary<Collectible, int>();
		foreach (var collectible in collectedCollectibles)
		{
			collectibleToEnumDict.Add(collectible,(int) collectible.CollectibleType);
		}
		
		var orderedEnumerable = from entry in collectibleToEnumDict orderby entry.Value ascending select entry;

		List<Collectible> sortedCollectibles = new List<Collectible>();
		foreach (var enumerable in orderedEnumerable)
		{
			sortedCollectibles.Add(enumerable.Key);
		}
		
		for (int i = 0 ; i< collectedCollectibles.Count ; i++)
		{
			collectedCollectibles[i] = sortedCollectibles[i];
		}
		
		for (int i = 0; i < collectedCollectibles.Count; i++)
		{
			float height = (collectedCollectibles.Count - 1 - i) * amountOfRise;
			collectedCollectibles[i].SetHeight(height);
		}
	}
}