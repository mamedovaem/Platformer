using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterController : MonoBehaviour
{
    [SerializeField] private PressedButton leftButton;
    public PressedButton LeftButton
    {
        get { return leftButton; }
    }
    [SerializeField] private PressedButton rightButton;
    public PressedButton RightButton
    {
        get { return rightButton; }
    }

    [SerializeField] private Button jumpButton;
    public Button JumpButton
    {
        get { return jumpButton; }
    }

    [SerializeField] private Button fireButton;
    public Button FireButton
    {
        get { return fireButton; }
    }
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.InitUIController(this);
    }

}
