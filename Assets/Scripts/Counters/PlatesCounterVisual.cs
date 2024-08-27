using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;
    private List<GameObject> plateList;
    private float plateSpacing = 0.1f;

    void Awake()
    {
        plateList = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        platesCounter.OnPlateAdded += OnPlateAddedVisual;
        platesCounter.OnPlateRemoved += OnPlateRemovedVisual;
    }

    private void OnPlateRemovedVisual(object sender, EventArgs e)
    {
        GameObject toBeRemoved = plateList[plateList.Count - 1];
        plateList.Remove(toBeRemoved);
        Destroy(toBeRemoved);
    }

    private void OnPlateAddedVisual(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        plateVisualTransform.localPosition = new Vector3(0, plateSpacing * plateList.Count, 0);
        plateList.Add(plateVisualTransform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
