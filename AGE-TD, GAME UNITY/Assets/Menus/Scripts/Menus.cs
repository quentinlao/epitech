using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Menus : MonoBehaviour {
	public GUISkin skin = null;
	public Texture logo;
	public Texture background;
	public int logoHeightMargin = 0;
	public int menuHeightMargin = 70;
	public string gameSceneName = "";
	public List<string> creditsContentLines = new List<string>();
	
	Rect layoutRect;

	public void Start()
	{
		layoutRect = new Rect(Screen.width / 2 - 100 / 2, Screen.height / 2 - 100 / 2, 100, 100);
	}

	bool isDisplayingCredits = false;
	public void OnGUI()
	{
		if (skin != null)
			GUI.skin = skin;
		if (background != null)
		GUI.DrawTexture(new Rect(
									0, 
									0, 
									Screen.width, 
									Screen.height), 
								background);
		if (logo != null)
			GUI.DrawTexture(new Rect(
									(Screen.width / 2) - (logo.width / 2), 
									logoHeightMargin, 
									logo.width, 
									logo.height), 
								logo);
			
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 225, menuHeightMargin, 500, 400));
		if (!isDisplayingCredits)
		{
			if (GUILayout.Button("Play") && gameSceneName != "")
			{
				PlayerPrefs.SetString("SceneName", gameSceneName);
				Application.LoadLevel("Loading");
			}
			if (GUILayout.Button("Credits"))
				isDisplayingCredits = true;
			if (GUILayout.Button("Exit"))
				Application.Quit();
		}
		else
		{
			foreach (string str in creditsContentLines)
				GUILayout.Label(str);
			if (GUILayout.Button("Back"))
				isDisplayingCredits = false;
		}
		GUILayout.EndArea();
	}
}
