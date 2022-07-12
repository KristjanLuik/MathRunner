using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public Vector3 _startPosition;
    public Vector3 _nextPiecePosition;
    public List<TrackMeta> trackPieces = new List<TrackMeta>();
    // Start is called before the first frame update
    void Start()
    {
        this._nextPiecePosition = _startPosition;
        GenerateTestingroad();
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("Generate Next Piece")]
    void GenerateNextPiece(TrackType trackType) 
    {
        this._nextPiecePosition = this._nextPiecePosition + trackPieces[trackType.GetHashCode()].offset;
        Instantiate(
            trackPieces[trackType.GetHashCode()].piece,
            this._nextPiecePosition,
            trackPieces[trackType.GetHashCode()].piece.transform.rotation
            );
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
