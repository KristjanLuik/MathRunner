using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Operatorns;

public class TrackManager : MonoBehaviour
{
    static public TrackManager instance { get { return s_Instance; } }
    static protected TrackManager s_Instance;
    public Vector3 _startPosition;
    public Vector3 _endPosition;
    public Vector3 _nextPiecePosition;
    public List<TrackMeta> trackPieces = new List<TrackMeta>();
    public GameObject endpiece;
    private PlayerData savestuff;

    protected void Awake()
    {
        s_Instance = this;
        savestuff = new PlayerData(new List<(TrackType, (MathProblem, MathProblem))> {
          //(TrackType.Obsticle,(new MathProblem(Operator.Add, 5), new MathProblem(Operator.Add, 10)))
          (TrackType.Obsticle,  (new MathProblem(Operator.Add, 5), new MathProblem(Operator.Add, 10))),
          (TrackType.Road,  (null, null)),
          (TrackType.Road,  (null, null)),
          (TrackType.Road,  (null, null)),
          (TrackType.Obsticle,  (new MathProblem(Operator.Multiply, 10), new MathProblem(Operator.Add, 14))),
          (TrackType.Road,  (null, null)),
          (TrackType.Road,  (null, null)),
          (TrackType.Road,  (null, null))
        });
    }

    void Start()
    {
        this._nextPiecePosition = _startPosition;
        //GenerateTestingroad();
        this.LoadRoad(savestuff);
    }

    public void LoadRoad(PlayerData loadedRoad) {

		for (int i = 0; i < loadedRoad.tracks.Count; i++)
		{
            this._nextPiecePosition = this._nextPiecePosition + trackPieces[loadedRoad.tracks[i].Item1.GetHashCode()].offset;
            GameObject generatedPiece = Instantiate(
                 trackPieces[loadedRoad.tracks[i].Item1.GetHashCode()].piece,
                 this._nextPiecePosition,
                 trackPieces[loadedRoad.tracks[i].Item1.GetHashCode()].piece.transform.rotation
                 );

            //Call obsticle init
            if (loadedRoad.tracks[i].Item1 == TrackType.Obsticle)
            {
                generatedPiece.GetComponent<Obsticle>().InitObsticle(loadedRoad.tracks[i].Item2.Item1, loadedRoad.tracks[i].Item2.Item2);
            }
            this._nextPiecePosition.z += trackPieces[loadedRoad.tracks[i].Item1.GetHashCode()].offset.magnitude;
            this._nextPiecePosition.x = trackPieces[loadedRoad.tracks[i].Item1.GetHashCode()].offset.x;
        }

     Instantiate(
        endpiece,
        this._nextPiecePosition + _endPosition,
        endpiece.transform.rotation
     );
    }

    [ContextMenu("Generate Testing road")]
    public void GenerateTestingroad() {
		for (int i = 0; i < 20; i++)
		{
            if (i % 2 == 0)
            {
                GenerateNextPiece(TrackType.Obsticle);
            }
            else {
                GenerateNextPiece(TrackType.Road);
            }
		}
    }


    [ContextMenu("Generate Next Piece")]
    void GenerateNextPiece(TrackType trackType) 
    {
        this._nextPiecePosition = this._nextPiecePosition + trackPieces[trackType.GetHashCode()].offset;
       GameObject generatedPiece = Instantiate(
            trackPieces[trackType.GetHashCode()].piece,
            this._nextPiecePosition,
            trackPieces[trackType.GetHashCode()].piece.transform.rotation
            );

        //Call obsticle init
        if (trackType == TrackType.Obsticle) {
            generatedPiece.GetComponent<Obsticle>().InitObsticle();
        }
        this._nextPiecePosition.z += trackPieces[trackType.GetHashCode()].offset.magnitude;
        this._nextPiecePosition.x = trackPieces[trackType.GetHashCode()].offset.x;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_nextPiecePosition, 0.5f);
    }

    [Serializable]
    public class TrackMeta {
        public Vector3 offset;
        public GameObject piece;
        public TrackType trackType;


    }
    public enum TrackType
    {
        Road,
        Obsticle
    }
}
