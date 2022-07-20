using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TrackManager;
using System;

public class PlayerData
{
	public List<(TrackType, (MathProblem, MathProblem))> tracks;

	public PlayerData(List<(TrackType, (MathProblem, MathProblem))> trackStuff)
	{
		this.tracks = trackStuff;
	}
}
