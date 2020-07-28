using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerDefenseGUI : MonoBehaviour {

	public bool IsGUIActive = true;
	public List<GameObject> TowerList = new List<GameObject>();
	bool isTurretSelected = false;
	Vector2 turretsScrollView = Vector2.zero;
	GameObject selectedTurret = null;
    private Vector3 elevatedPoint;
	void OnGUI()
	{
		if (isTurretSelected)
		{

		}
		else
		{
			selectedTurret = null;
			GUILayout.BeginArea(new Rect(0, Screen.height - 100, Screen.width, 100));
			turretsScrollView = GUILayout.BeginScrollView(turretsScrollView);

			for (int i = 0; i < TowerList.Count; ++i)
			{
				if (TowerList[i].GetComponent<tower>() == null)
				{
					TowerList.Remove(TowerList[i]);
					continue;
				}
				if (GUI.Button(new Rect(i * 100, 0, 100, 100), TowerList[i].name))
				{
					selectedTurret = Instantiate(TowerList[i]) as GameObject;
					selectedTurret.GetComponent<tower>().isActive = false;
					isTurretSelected = true;
				}
			}


			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isTurretSelected)
		{
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool collided = false;
			foreach (RaycastHit rh in Physics.RaycastAll(r))
			{
				if (rh.collider.tag != selectedTurret.tag)
					continue;
                selectedTurret.transform.position = rh.point + selectedTurret.GetComponent<tower>().positionOffset;
                collided = true;
				break;
			}
			if (Input.GetMouseButtonDown(0) && collided)
			{
				isTurretSelected = false;
				selectedTurret.GetComponent<tower>().isActive = true;
				selectedTurret = null;
			}
		}
	}
}
