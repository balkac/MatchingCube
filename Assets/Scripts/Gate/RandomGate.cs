using System.Collections.Generic;

public class RandomGate : BaseGate
{
	private void Swap(ref Collectible collectible1, ref Collectible number2)
	{
		Collectible temp;
		temp = collectible1;
		collectible1 = number2;
		number2 = temp;
	}
	
	private void Shuffle(Collectible[] collectedCollectibles)
	{
		int len = collectedCollectibles.Length;
		for (int i = 0; i < len; i++)
		{
			Swap(ref collectedCollectibles[i],ref collectedCollectibles[UnityEngine.Random.Range(0,len)]); 
		}
	}
	
	protected override void OnEnterCustomActions(List<Collectible> collectedCollectibles, float amountOfRise)
	{
		Collectible[] collectibleArray = new Collectible[collectedCollectibles.Count];
		for (int i = 0; i < collectedCollectibles.Count; i++)
		{
			collectibleArray[i] = collectedCollectibles[i];
		}
		
		Shuffle(collectibleArray);
		
		for (int i = 0; i < collectedCollectibles.Count; i++)
		{
			collectedCollectibles[i] = collectibleArray[i];
		}

		for (int i = 0; i < collectedCollectibles.Count; i++)
		{
			float height = (collectedCollectibles.Count - 1 - i) * amountOfRise;
			collectedCollectibles[i].SetHeight(height);
		}
	}
}