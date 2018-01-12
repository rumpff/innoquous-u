using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathObject : MonoBehaviour
{
    public enum ObjectTypes
    {
        LevelBorder,
        Razor
    }

    [SerializeField]
    private ObjectTypes m_objectType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().Kill();
            GameObject.Find("GameManager").GetComponent<GameManager>().UIManager.SetDeathCouse(GetDeathCause());
        }
    }

    private string GetDeathCause()
    {
        switch (m_objectType)
        {
            case ObjectTypes.LevelBorder:
                return "ringout";
                break;
            case ObjectTypes.Razor:
                return "razor";
                break;
            default:
                return "error";
                break;
        }
    }
}
