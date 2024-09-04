using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
    void Awake()
    {
        TrashCounter.ClearStaticData();
        CuttingCounter.ClearStaticData();
        BaseCounter.ClearStaticData();
    }
}
