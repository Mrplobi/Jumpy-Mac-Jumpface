﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colision : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(GetComponent<Collider2D>().bounds.min.y< other.bounds.max.y && GetComponent<Collider2D>().bounds.max.y > other.bounds.max.y)//On arrive par au dessus
        {
            Physics physics = gameObject.GetComponent<Physics>();

            Debug.Log("BIPBIP DETECTED");
            physics.IsGrounded = true;
            physics.numberJumpCurrent = 0;
            physics.Velocity = new Vector3(physics.Velocity.x, 0, 0);
            transform.position = new Vector3(transform.position.x, other.bounds.max.y+ GetComponent<Collider2D>().bounds.size.y/2);
        }

    }
    public void DetectColision()
    {
        Physics physics = gameObject.GetComponent<Physics>();
        //float size = gameObject.GetComponent<Collider2D>().Distance() * gameObject.transform.localScale.y;
        Vector3 velocity = physics.Velocity;
        //Vector2 direction = new Vector2(gameObject.GetComponent<Physics>().Velocity.x, gameObject.GetComponent<Physics>().Velocity.y);
        Vector2 originD = new Vector2(gameObject.transform.position.x, gameObject.GetComponent<Collider2D>().bounds.min.y + 0.01f);
        Vector2 originU = new Vector2(gameObject.transform.position.x, gameObject.GetComponent<Collider2D>().bounds.max.y + 0.01f);
        Vector2 originR = new Vector2(gameObject.GetComponent<Collider2D>().bounds.max.x + 0.01f, gameObject.transform.position.y);
        Vector2 originL = new Vector2(gameObject.GetComponent<Collider2D>().bounds.min.x + 0.01f, gameObject.transform.position.y);
        RaycastHit2D hitD = Physics2D.Raycast(originD, -gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitU = Physics2D.Raycast(originU, gameObject.transform.up, velocity.y * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitR = Physics2D.Raycast(originR, gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        RaycastHit2D hitL = Physics2D.Raycast(originL, -gameObject.transform.right, velocity.x * Time.deltaTime, LayerMask.GetMask("Environment"));
        if (hitD)
        {
            //Debug.Log("collisionDown");
            physics.IsGrounded = true;
           /* Vector3 impact = new Vector3(hitD.point.x, hitD.point.y+
                gameObject.GetComponent<Collider2D>().bounds.size.y/2, 0);
            gameObject.transform.position = impact;*/
            
            Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
            physics.Velocity = newSpeed;
            physics.numberJumpCurrent = 0;
            transform.position = new Vector3(transform.position.x, hitD.collider.bounds.max.y + GetComponent<Collider2D>().bounds.size.y / 2);
            //check if jump buffered
            InputButton action=GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
            if (action!=null)
            {
                action.Invoke();
            }
        }
        else
        {
            physics.IsGrounded = false;
        }
        if (hitU)
        {
           // Debug.Log("collisionUp");
            Vector3 newSpeed = new Vector3(physics.Velocity.x, 0, 0);
            physics.Velocity = newSpeed;
            transform.position = new Vector3(transform.position.x, hitU.collider.bounds.min.y - GetComponent<Collider2D>().bounds.size.y / 2 - 0.01f);
        }
        if (hitR)
        {
        //    Debug.Log("collisionRight");
            Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
            physics.Velocity = newSpeed;
            transform.position = new Vector3(hitR.collider.bounds.min.x - GetComponent<Collider2D>().bounds.size.x / 2 - 0.01f, transform.position.y);
            InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
            InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveLeft, 10);

            if (actionJump != null && actionMove != null && !physics.IsGrounded)
            {
                Debug.Log("WallJUMP");
            }
        }
        if (hitL)
        {
          //  Debug.Log("collisionLeft");
            Vector3 newSpeed = new Vector3(0, physics.Velocity.y, 0);
            physics.Velocity = newSpeed;
            transform.position = new Vector3(hitL.collider.bounds.max.x + GetComponent<Collider2D>().bounds.size.x / 2 + 0.01f, transform.position.y);
            InputButton actionJump = GetComponent<InputManager>().SearchForFailedAction(physics.Jump, 10);
            InputButton actionMove = GetComponent<InputManager>().SearchForFailedAction(GetComponent<InputManager>().MoveRight, 10);

            if (actionJump != null && actionMove !=null && !physics.IsGrounded)
            {
                Debug.Log("WallJUMP");
            }
        }
    }

    private void Update()
    {
       // DetectColision();
    }
}

