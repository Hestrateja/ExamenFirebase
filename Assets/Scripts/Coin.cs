using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed = 5f;
    public Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0,-speed*Time.deltaTime,0);
    }
    private void OnMouseDown()
    {
        player.GetCoin();
        this.gameObject.SetActive(false);
    }
}
