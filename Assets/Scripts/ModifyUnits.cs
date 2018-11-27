using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyUnits : MonoBehaviour
{
    [SerializeField]
    Unit[] unitsToModify;
    void Start()
    {
        foreach (Unit unit in unitsToModify)
        {
            unit.Movements = TransformNumToMovement(unit.movementSpeed);
            unit.rangeMov = TransformNumToMovement(unit.range);
        }
        //newMovement = TransformNumToMovement(3);
    }

    public Vector2[] newMovement;

    public Vector2[] TransformNumToMovement(int num)
    {
        //Vector2[] newMovement;
        //newMovement = new Vector2[150];
        newMovement = new Vector2[num * 4 * num + (num * 4)];



        for (int i = 1; i <= num; i++)
        {
            newMovement[(i - 1) * 4] = new Vector2(0, i);
            newMovement[(i - 1) * 4 + 1] = new Vector2(i, 0);
            newMovement[(i - 1) * 4 + 2] = new Vector2(0, -i);
            newMovement[(i - 1) * 4 + 3] = new Vector2(-i, 0);
        }

        for (int y = 1; y <= num; y++)
        {
            for (int x = 1; x <= num; x++)
            {
                int index = (y * num * 4) + (x - 1) * 4;
                Debug.Log(index);
                newMovement[index] = new Vector2(x, y);
                newMovement[index + 1] = new Vector2(-x, y);
                newMovement[index + 2] = new Vector2(x, -y);
                newMovement[index + 3] = new Vector2(-x, -y);
            }
        }
        return newMovement;
    }
}

