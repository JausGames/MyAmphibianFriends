using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using System.Linq;

namespace Inputs
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] PlayerController motor = null;
        [SerializeField] private float rotate;
        [SerializeField] private float index;
        [SerializeField] private bool jump = false;
        
        private Controls controls;
        private Controls Controls
        {
        get
            {
                if (controls != null) { return controls; }
                return controls = new Controls();
            }
        }
        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        // Update is called once per frame
        private void Start()
        {
            index = playerInput.playerIndex;
            var motors = FindObjectsOfType<PlayerController>();
            motor = motors.FirstOrDefault(m => m.GetPlayerIndex() == index);
            /*Controls.Enable();
            Controls.Player.Rotate.performed += ctx => SetRotate(ctx.ReadValue<float>());
            Controls.Player.Jump.performed += _ => SetJump(true);
            Controls.Player.Jump.canceled += _ => SetJump(false);*/
            
        }
        public void OnRotate(CallbackContext context)
        {
            if (motor == null) return;
            rotate = context.ReadValue<float>();
            motor.SetRotate(rotate);
        }
        public void OnJump(CallbackContext context)
        { 
            if (motor == null) return;
            if (context.ReadValue<float>() == 1) jump = true;
            else jump = false;
            motor.SetJump(jump);
        }
    }
}
