﻿//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    [Header("Set Target Prefab")]
    //的プレハブ
    [SerializeField]
    private GameObject targetPrefab;
    //キャラクターのオブジェクト
    [SerializeField]
    private GameObject character;

    public Rigidbody rb;
    float velocity = 0f;

    [Header("Set Interval Min and Max")]
    //時間間隔の最小値
    public float minTime = 2f;
    //時間間隔の最大値
    public float maxTime = 5f;
    /*
        [Header("Set X Position Min and Max")]
        //X座標の最小値
        //public float xMinPosition = -10f;
        //X座標の最大値
        public float xMaxPosition = 10f;
        [Header("Set Y Position Min and Max")]
        //Y座標の最小値
        public float yMinPosition = 1f;
        //Y座標の最大値
        //public float yMaxPosition = 10f;
        [Header("Set Z Position Min and Max")]
        //Z座標の最小値
        //public float zMinPosition = -10f;
        //Z座標の最大値
        public float zMaxPosition = 10f;
    */
    [Header("的が生成される範囲の設定")]
    //キャラクターから前の距離の最大値
    public float frontMaxPosition = 3.0f;
    //キャラクターの左右の幅の最大値
    public float widthMaxPosition = 2.0f;
    //的を置く高さの最大値最小値
    public float minYPosition = 0.0f;
    public float maxYPosition = 1.0f;

    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;

    float firstMaxTime;

    // Start is called before the first frame update
    void Start()
    {
        interval = GetRandomTime();
        firstMaxTime = maxTime; 
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > interval)
        {
            GameObject target = Instantiate(targetPrefab);//targetをインスタンス化する(生成する)
            target.transform.position = character.transform.position + GetForwardPositionOfCharacter() + new Vector3(0, GetRandomYPosition(), 0);
            //target.transform.position = GetRandomPosition();
            velocity = rb.velocity.magnitude;
            if (velocity != 0)
            {
                maxTime = firstMaxTime * (1 / (Mathf.Sqrt(velocity))) + 0.2f;
                Debug.Log("maxTime : " + maxTime);
            }
            interval = GetRandomTime();//次に発生する時間間隔を決定する
            time = 0f;
        }
    }

    void FixedUpdate()
    {
    }

    private float GetRandomYPosition()
    {
        return Random.Range(minYPosition, maxYPosition);
    }

    private float GetRandomTime()
    {
        return Random.Range(minTime, maxTime);
    }

    /*  private Vector3 GetRandomPosition()
      {
          //それぞれの座標をランダムに生成する
          float x = Random.Range(xMinPosition, xMaxPosition);
          float y = Random.Range(yMinPosition, yMaxPosition);
          float z = Random.Range(zMinPosition, zMaxPosition);
          return new Vector3(x, y, z);
      }
  */
    private Vector3 GetForwardPositionOfCharacter()
    {
        var leftToRightVec = (character.transform.right + character.transform.right).normalized;//character.transform.leftは-character.transform.right
        return character.transform.forward * Random.Range(1, frontMaxPosition) + leftToRightVec * Random.Range(-widthMaxPosition, widthMaxPosition);
    }

}