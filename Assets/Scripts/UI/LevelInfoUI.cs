using TMPro;
using UnityEngine;

public class LevelInfoUI : MonoBehaviour
{
	[SerializeField] private TMP_Text levelText;
	
	[SerializeField] private string preFix = "Level ";
    
	public void Init()
	{
		levelText.text = preFix + UserSaveManager.Instance.GetVirtualLevelID();
	}
}