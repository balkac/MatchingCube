using UnityEngine.SceneManagement;


public class LevelManager : Singleton<LevelManager>
{
	public int CurSceneID { get; private set; }
	
	public void LoadScene(int sceneID)
	{
		CurSceneID = sceneID;
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(CurSceneID, LoadSceneMode.Single);
	}
	
}