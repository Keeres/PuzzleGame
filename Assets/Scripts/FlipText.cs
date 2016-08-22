using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;

//Displays text from right to left

public class FlipText : MonoBehaviour {
	
	public Text myText; //You can also make this public and attach your UI text here.
	public string score = "0";

	string individualLine = ""; //Control individual line in the multi-line text component.
	
	//int numberOfAlphabetsInSingleLine = 20;
	

	void Start(){
	//	string text = myText.text;
		List <string> listofWords = score.Split (' ').ToList ();;

		foreach (string s in listofWords) {
			myText.text = " ";
			myText.text += Reverse (individualLine);
			  //Add a new line feed at the end, since we cannot accomodate more characters here.
			//individualLine = " "; //Reset this string for new line.
			individualLine +=s+" ";

		}
		UpdateScore ();
	}

	void UpdateScore(){
		//	string text = myText.text;
		List <string> listofWords = score.Split (' ').ToList ();;
		
		foreach (string s in listofWords) {
			
			myText.text += Reverse (individualLine);
			//Add a new line feed at the end, since we cannot accomodate more characters here.
			//individualLine = " "; //Reset this string for new line.
			individualLine +=s+" ";
			
		}
		


	}
	
	public static string Reverse(string s)
	{
		char[] charArray = s.ToCharArray();
		Array.Reverse(charArray);
		return new string(charArray);
	}
	
}