using UnityEngine;
using System;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float RunSpeed = 20f;
    public float WalkSpeed = 8f;

    float Gravity;
    float VelocityY;

    [SerializeField]
    GameObject Camera;

    public float JumpHeight = 1f;
    public float TimeToReachGround = 0.2f;

    float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;

    [Range(0, 1)]
    public float AirControlPercent;

    float SpeedSmoothTime = 0.1f;
    float SpeedSmoothVelocity;
    float CurrentSpeed;

    Animator Animator;
    public CharacterController Controller;

    private void Start()
    {
        Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 InputDir = input.normalized;

        Vector2 CameraPosition;

        Gravity = -(2 * JumpHeight) / Mathf.Pow(TimeToReachGround, 2);

        CameraPosition = new Vector2(Camera.transform.forward.x, Camera.transform.forward.z);

        //Debug.DrawRay(Camera.transform.position, Camera.transform.forward * 10, Color.red);
        float roattion = Mathf.Atan2(CameraPosition.x, CameraPosition.y) * Mathf.Rad2Deg;

        float n = Vector2.Dot(Vector2.up, CameraPosition);
        float m = Mathf.Sqrt(CameraPosition.x * CameraPosition.x + CameraPosition.y * CameraPosition.y);        
        float Angle = Mathf.Acos(n / m);

        if (InputDir != Vector2.zero)
        {
            float TargetRotation = Mathf.Atan2(InputDir.x, InputDir.y) * Mathf.Rad2Deg + roattion;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation, ref TurnSmoothVelocity, GetModifiedSmoothTime(TurnSmoothTime));

            //Debug.Log(Mathf.Atan2() * Mathf.Rad2Deg);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump(); // 2s/t^2 = g
        }

        bool Running = (Input.GetAxisRaw("Fire1") == 1.0f) && (Mathf.Abs(input.x) >= 0.5f || Mathf.Abs(input.y) >= 0.5f);
        float TargetSpeed = (Running) ? RunSpeed : WalkSpeed * InputDir.magnitude;
        CurrentSpeed = Mathf.SmoothDampAngle(CurrentSpeed, TargetSpeed, ref SpeedSmoothVelocity, GetModifiedSmoothTime(SpeedSmoothTime));

        VelocityY += Time.deltaTime * Gravity;
        Vector3 Velocity = transform.forward * CurrentSpeed + Vector3.up * VelocityY;

        Controller.Move(Velocity * Time.deltaTime);
        CurrentSpeed = new Vector2(Controller.velocity.x, Controller.velocity.z).magnitude;

        if (Controller.isGrounded) VelocityY = 0;

        float animSpeedPercent = ((Running) ? (CurrentSpeed / RunSpeed) : (CurrentSpeed / WalkSpeed) * 0.5f) * InputDir.magnitude;

        Animator.SetFloat("MovementBlend", animSpeedPercent, SpeedSmoothTime, Time.deltaTime);
        Debug.Log(animSpeedPercent);
    }

    private void Jump()
    {
        if (Controller.isGrounded)
        {
            float JumpVelocity = Mathf.Sqrt(-2 * Gravity * JumpHeight);
            VelocityY = JumpVelocity;
        }
    }

    float GetModifiedSmoothTime(float SmoothTime)
    {
        if (Controller.isGrounded) return SmoothTime;
        if (AirControlPercent == 0) return float.MaxValue;
        return SmoothTime / AirControlPercent;
    }
}



/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{        
    public float RunSpeed = 20f;    
    public float WalkSpeed = 8f;
    
    float Gravity;        
    float VelocityY;
        
    public float JumpHeight = 1f;    
    public float TimeToReachGround = 0.2f;

    float TurnSmoothTime = 0.1f;
    float TurnSmoothVelocity;

    [Range(0,1)]
    public float AirControlPercent;

    float SpeedSmoothTime = 0.1f;
    float SpeedSmoothVelocity;
    float CurrentSpeed;

    //Animator Animator;
    public CharacterController Controller;

    private void Start()
    {
        //Animator = GetComponent<Animator>();
        Controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 InputDir = input.normalized;

        Gravity = -(2 * JumpHeight) / Mathf.Pow(TimeToReachGround, 2);

        if(InputDir != Vector2.zero)
        {
            float TargetRotation = Mathf.Atan2(InputDir.x, InputDir.y) * Mathf.Rad2Deg;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetRotation, ref TurnSmoothVelocity, GetModifiedSmoothTime(TurnSmoothTime));
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump(); // 2s/t^2 = g
        }

        bool Running = (Input.GetAxisRaw("Fire1")==1.0f) && (Mathf.Abs(input.x)>=0.5f || Mathf.Abs(input.y) >= 0.5f) || (Input.GetAxisRaw("Fire3") == 1.0f)&& Mathf.Abs(input.y)>=0.5f;
        float TargetSpeed = (Running) ? RunSpeed : WalkSpeed * InputDir.magnitude;
        CurrentSpeed = Mathf.SmoothDampAngle(CurrentSpeed, TargetSpeed, ref SpeedSmoothVelocity, GetModifiedSmoothTime(SpeedSmoothTime));

        VelocityY += Time.deltaTime * Gravity;
        Vector3 Velocity = transform.forward * CurrentSpeed + Vector3.up * VelocityY;

        Controller.Move(Velocity * Time.deltaTime);
        CurrentSpeed = new Vector2(Controller.velocity.x, Controller.velocity.z).magnitude;

        if (Controller.isGrounded) VelocityY = 0;

        float animSpeedPercent = ((Running)? (CurrentSpeed/RunSpeed) : (CurrentSpeed/WalkSpeed) * 0.5f) * InputDir.magnitude;        

        //Animator.SetFloat("SpeedPercent", animSpeedPercent, SpeedSmoothTime, Time.deltaTime);
    }



    private void Jump()
    {
        if (Controller.isGrounded)
        {
            float JumpVelocity = Mathf.Sqrt(-2 * Gravity * JumpHeight);
            VelocityY = JumpVelocity;
        }
    }

    float GetModifiedSmoothTime(float SmoothTime)
    {
        if (Controller.isGrounded) return SmoothTime;
        if (AirControlPercent == 0) return float.MaxValue;
        return SmoothTime / AirControlPercent;
    }
}
*/
