using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TimeUpToGameOver : MonoBehaviour
{
	public Text timerText;

	[SerializeField] private float totalTime;
	int seconds;
	// Update is called once per frame
	void Start()
	{
		//Invoke�́Z�b��ɏ��������s���郁�\�b�h
		Invoke("ChangeScene", totalTime);
	}
	void Update()
	{
		totalTime -= Time.deltaTime;
		seconds = (int)totalTime;
		timerText.text = seconds.ToString();
	}
	void ChangeScene()
	{
		SceneManager.LoadScene("GameOver");
	}
}
