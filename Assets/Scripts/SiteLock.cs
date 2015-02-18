using UnityEngine;
using System.Collections;

public class SiteLock : MonoBehaviour {
	
	public string[] allowedSites;
	
	void Start () {
		
		if (Application.isWebPlayer)
		{
			string url = Application.absoluteURL.Split('/')[2];
			bool foundGoodSite = false;
			for (int i = 0; i < allowedSites.Length; i++)
			{
				if (url.IndexOf(allowedSites[i]) >= 0 && url.IndexOf(allowedSites[i]) == (url.Length - allowedSites[i].Length))
				{
					foundGoodSite = true;
				}
			}
			
			if (foundGoodSite == false)
			{
				//site is invalid, lock us up!
				Application.Quit();
			}
		}
	}
}