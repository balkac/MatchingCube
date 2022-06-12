using UnityEngine;

public class UserSaveManager : MonoBehaviour
{
	private static UserSaveManager _instance;
	public static UserSaveManager Instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<UserSaveManager>();

			return _instance;
		}
	}

	private const string LAST_COMPLETED_LEVEL = "LastCompletedLevel";

	private const string ALL_LEVELS_COMPLETED_COUNT = "AllLevelsCompletedCount";

	private int GetAllLevelsCompletionCount()
	{
		var hasLevelsCompletedCount = PlayerPrefs.HasKey(ALL_LEVELS_COMPLETED_COUNT);

		return hasLevelsCompletedCount ? PlayerPrefs.GetInt(ALL_LEVELS_COMPLETED_COUNT) : 0;
	}

	public int GetLastCompletedCount()
	{
		var hasCoinCount = PlayerPrefs.HasKey(LAST_COMPLETED_LEVEL);

		return hasCoinCount ? PlayerPrefs.GetInt(LAST_COMPLETED_LEVEL) : 0;
	}
	
	public void SaveCurLevel(int curLevelID)
	{
		int totalPlayableLevelCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

		if (curLevelID % totalPlayableLevelCount == 0)
		{
			PlayerPrefs.SetInt(ALL_LEVELS_COMPLETED_COUNT, GetAllLevelsCompletionCount() + 1);
		}

		PlayerPrefs.SetInt(LAST_COMPLETED_LEVEL, curLevelID);
		PlayerPrefs.Save();
	}

	public int GetVirtualLevelID()
	{
		int allLevelsCompletionCount = GetAllLevelsCompletionCount();

		int totalPlayableLevelCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings ;

		int curSceneID = GetLastCompletedCount() % UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;

		int levelID = totalPlayableLevelCount * allLevelsCompletionCount + curSceneID;

		return levelID;
	}
	
	public int GetCurLevelID()
	{
		int curSceneID = GetLastCompletedCount() % UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
		
		return curSceneID;
	}
	
	
}