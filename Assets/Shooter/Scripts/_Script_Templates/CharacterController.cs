using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FitnessModel
{
    public class CharacterController : MonoBehaviour
    {
        public float _RotationSpeed;
        UserInput _UserInput;
        AnimationState state;
        private void Awake()
        {
            _UserInput = GetComponent<UserInput>();
            state = GetComponent<AnimationState>();
        }

        private void Update()
        {

            if (state.characterStates == AnimationState.CharacterStates.Idle && state._AnimationFinished)
            {
                if (_UserInput.Target != Vector3.zero)
                {
                    state.NextState("Point");
                    StartCoroutine(FaceTarget(_UserInput.Target));
                    _UserInput.Target = Vector3.zero;
                }
                else
                {
                    if (_UserInput.Jumping)
                        state.NextState("Jump");

                    if (_UserInput.Squatting)
                        state.NextState("Squat");

                    if (_UserInput.Throwing)
                        state.NextState("Throw");
                }

            }

        }
        
        // Rototating the character to the pointed point
        IEnumerator FaceTarget(Vector3 Target)
        {
            Quaternion targetRotation = Quaternion.identity;
            Vector3 _TargetPosition = Target;
            
            do
            {
                Vector3 targetDirection = (_TargetPosition - transform.position).normalized;
                // I ignore the y, so the character doesn't bend
                targetDirection = new Vector3(targetDirection.x, 0, targetDirection.z);  
                targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _RotationSpeed);
                yield return null;
            } while (Quaternion.Angle(transform.rotation, targetRotation) > 3);
            
        }
  
    }
}

