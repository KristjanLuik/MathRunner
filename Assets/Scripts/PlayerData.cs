using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TrackManager;
using System;

[Serializable]
public class PlayerData
{
	public List<(TrackType, (MathProblem, MathProblem))> tracks;
	public int endArrowRequiredAmount;

	public PlayerData(List<(TrackType, (MathProblem, MathProblem))> trackStuff, int arrowRequiredAmount = 1)
	{
		this.tracks = trackStuff;
		this.endArrowRequiredAmount = arrowRequiredAmount;
	}
}
