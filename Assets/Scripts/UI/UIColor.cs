using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIColor : MonoBehaviour
{
    [SerializeField] private Color[] lightColors = new Color[] {
                                                    new Color(0, 0.75f, 0),
                                                    new Color(0.75f, 0, 0),
                                                    new Color(0.75f, 0.75f, 0),
                                                    new Color(0, 0, 0.75f)
        };
    [SerializeField] private Color[] colors = new Color[] {
                                                    new Color(0, 0.5f, 0),
                                                    new Color(0.5f, 0, 0),
                                                    new Color(0.5f, 0.5f, 0),
                                                    new Color(0, 0, 0.4f)
        };

    #region Singleton
    public static UIColor instance;

    private void Awake()
    {
        instance = this;

    }

    #endregion

    public enum Colors {green, red, yellow, blue, lightGreen, lightRed, lightYellow, lightBlue}

    public Color GetColor(Colors col)
    {
        switch (col)
        {
            case Colors.green:
                return colors[0];
            case Colors.lightGreen:
                return lightColors[0];
            case Colors.red:
                return colors[1];
            case Colors.lightRed:
                return lightColors[1];
            case Colors.yellow:
                return colors[2];
            case Colors.lightYellow:
                return lightColors[2];
            case Colors.blue:
                return colors[3];
            case Colors.lightBlue:
                return lightColors[3];
            default:
                return Color.white;
        }
    }
    public Color GetColorByString(string col)
    {
        switch (col)
            {
                case "green":
                    return colors[0];
                case "lightGreen":
                    return lightColors[0];
                case "red":
                    return colors[1];
                case "lightRed":
                    return lightColors[1];
                case "yellow":
                    return colors[2];
                case "lightYellow":
                    return lightColors[2];
                case "blue":
                    return colors[3];
                case "lightBlue":
                    return lightColors[3];
                default:
                    return Color.white;
            }
    }


}
