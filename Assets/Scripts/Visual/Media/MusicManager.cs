﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MusicManager : MonoBehaviour
{
	float timeSetter = 0;
	//public Material backMaterial, GameMaterial;
	[SerializeField] private Image state;
	public Slider timearea;
	public string scenename;
	public static bool IsPlaying, IsPaused, isMoving;
	private int TrackNum, temp, backnum;
	public static Text Trackname;
	public Sprite play, pause;
	public Texture[] backgrounds, starfield;
	public GameObject _musicManager;
	public AudioClip click, pauseclick;
	public AudioClip[] music;
	public AudioSource _publicSource, sfxsource;
	[SerializeField] Text availableList,trackName;

	void Start()
	{
		Trackname = trackName;
		temp = Random.Range(0, music.Count());
		backnum = Random.Range(0, backgrounds.Count());
	    TrackNum = temp+1;
		availableList.text = TrackNum + " of " + music.Length;
		Trackname.text = music[temp].name;
		IsPlaying = true;
		IsPaused = false;
		_publicSource.clip = music[temp];
		_publicSource.Play();
		timearea.maxValue = music[temp].length;
	}
	void Update ()
	{
		if (!_publicSource.isPlaying && !IsPaused)
		   Next();
		if (!isMoving)
		timearea.value = _publicSource.time;
	}

	public void SliderPointerDown()
	{
		isMoving = true;
		Debug.Log("Down");
	}

	public void SliderPointerUp()
	{
		isMoving = false;
		_publicSource.time = timearea.value;
		Debug.Log("Up");
	}

	public void Previous()
	{
		sfxsource.PlayOneShot(click);
		TrackNum--;
		temp--;
		backnum--;
		if (temp == -1)
		{
			temp = Convert.ToInt32(music.Length) -1 ;
			TrackNum = temp+1;
		}
		if (backnum == -1)
		{
			backnum = Convert.ToInt32(backgrounds.Length) -1;
		}

		availableList.text = TrackNum + " of " + music.Length;
		Play();
	}
	public void Next()
	{
		sfxsource.PlayOneShot(click);
		TrackNum++;
		temp++;
		backnum++;
		if (temp == music.Length)
		{
		    temp = 0;
			TrackNum = temp + 1;
		}
		if (backnum == backgrounds.Length)
		{
			backnum = 0;
		}
		availableList.text = TrackNum + " of " + music.Length;
		Play();
	}

	 public void Play()
	{
			state.sprite = pause;
		_publicSource.time = 0;
			_publicSource.clip = music[temp];
			timearea.maxValue = music[temp].length;
			_publicSource.Stop();
			Trackname.text = music[temp].name;
		_publicSource.clip = music[temp];
		_publicSource.Play();
	}

	public void State()
	{
		if (!IsPaused)
		{
			state.sprite = play;
			IsPaused = true;
			sfxsource.PlayOneShot(pauseclick);
			_publicSource.Pause();
		}
		else
		{
			state.sprite = pause;
			IsPaused = false;
			_publicSource.UnPause();
		}
    }
}

