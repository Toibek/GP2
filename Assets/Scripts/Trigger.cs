using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{
    // Declare a public variable to store a reference to the canvas
    public Canvas canvas;

    // Use the `System.Serializable` attribute to make this enum serializable and visible in the Inspector
    [System.Serializable]
    public enum ButtonOption
    {
        Joystick,
        A,
        X,
        Y,
        B,
        WASD,
        SPACE
    }

    // Declare a serialized field to store the button selection
    [SerializeField] private ButtonOption button = ButtonOption.A;

    // This function is called when the Collider attached to this game object collides with another Collider
    private void OnTriggerEnter(Collider other)
    {
        // Get a reference to the canvas component that is a child of the game object that collided with this trigger
        canvas = other.GetComponentInChildren<Canvas>(includeInactive: true);

        // Check if the canvas was found
        if (canvas != null)
        {
            // If the canvas was found, set its game object to be active
            canvas.gameObject.SetActive(true);

            // Perform action based on the button selected in the Inspector
            switch (button)
            {
                case ButtonOption.Joystick:
                    // Do action for move
                    Debug.Log("MoveControler");
                    canvas.transform.GetChild(0);
                    canvas.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                case ButtonOption.A:
                    // Do action for button A
                    Debug.Log("A");
                    canvas.transform.GetChild(1);
                    canvas.transform.GetChild(1).gameObject.SetActive(true);
                    break;
                case ButtonOption.X:
                    // Do action for button X
                    Debug.Log("X");
                    canvas.transform.GetChild(2);
                    canvas.transform.GetChild(2).gameObject.SetActive(true);
                    break;
                case ButtonOption.Y:
                    // Do action for button Y
                    Debug.Log("Y");
                    canvas.transform.GetChild(3);
                    canvas.transform.GetChild(3).gameObject.SetActive(true);
                    break;
                case ButtonOption.B:
                    // Do action for button B
                    Debug.Log("B");
                    canvas.transform.GetChild(4);
                    canvas.transform.GetChild(4).gameObject.SetActive(true);
                    break;
                case ButtonOption.WASD:
                    // Do action for button WASD
                    Debug.Log("B");
                    canvas.transform.GetChild(5);
                    canvas.transform.GetChild(5).gameObject.SetActive(true);
                    break;
                case ButtonOption.SPACE:
                    // Do action for button SPACE
                    Debug.Log("B");
                    canvas.transform.GetChild(6);
                    canvas.transform.GetChild(6).gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }
    }

    // This function is called when the Collider attached to this game object stops colliding with another Collider
    private void OnTriggerExit(Collider other)
    {
        // Get a reference to the canvas component that is a child of the game object that was colliding with this trigger
        canvas = other.GetComponentInChildren<Canvas>(includeInactive: true);

        // Check if the canvas was found
        if (canvas != null)
        {
            // If the canvas was found, set its game object to be inactive
            canvas.gameObject.SetActive(false);
        }
    }
}
