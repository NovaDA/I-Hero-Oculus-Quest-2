using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace FitnessModel
{
    public class UserInput : MonoBehaviour
    {
        bool _Jumping;
        public bool Jumping
        {
            get { return _Jumping; }
        }
        bool _Squatting;

        public bool Squatting
        {
            get { return _Squatting; }
        }

        bool _Idle;

        public bool Idle
        {
            get { return _Idle; }
        }
        bool _Throwing;

        public bool Throwing
        {
            get { return _Throwing; }
        }

        Vector3 _Target = Vector3.zero;

        public Vector3 Target
        {
            get { return _Target;  }
            set { _Target = value;  }
        }

        private void Update()
        {

            // Checking if any button is pressed once
            if (Input.GetMouseButtonDown(0))
            {
                FindTarget();
            }

             _Jumping = Input.GetKeyDown(KeyCode.J);
             _Squatting = Input.GetKeyDown(KeyCode.S);
             _Throwing = Input.GetKeyDown(KeyCode.Space);

        }

        private void FindTarget()
        {
            // finding the point i clicked at
             var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

             RaycastHit hit = new RaycastHit();

             if (Physics.Raycast(ray, out hit))
             {
                Debug.Log(hit.collider.gameObject.name);

                _Target = hit.point;
                Debug.Log("The Point is : " + _Target);
             }

        }
    }
}

