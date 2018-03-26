using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public PlayerController pC;
    public TileGenerator tileGenerator;
    private Vector3 offset;
    private Vector3 targetPos;

	// Use this for initialization
	void Start () {
        transform.position = new Vector3((float)(tileGenerator.colums / 2f)-0.5f, (float)transform.position.y, (float)(tileGenerator.rows / 2f) - 0.5f);
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {

        if (Time.timeScale == 0)
        {
            return;
        }

        targetPos = new Vector3(transform.position.x, (player.transform.position.y + offset.y), transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);

        //if (!pC.playerGoingUp)
        //{
        //    transform.position = Vector3.Lerp(transform.position, targetPos, 0.5f);
        //    //transform.position = targetPos;
        //}


        //if(transform.position.y - player.transform.position.y < 3f)
        //{
        //    pC.GameOver();
        //}
    }
}
