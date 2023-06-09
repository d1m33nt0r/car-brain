using System.Linq;
using NeuralNet;
using UnityEngine;

namespace DavidJalbert
{
    public class TinyCarStandardInput : MonoBehaviour
    {
        public LearnManager learnManager;
        public TinyCarController carController;
        
        [Tooltip("For how long the boost should last in seconds.")]
        public float boostDuration = 1;
        [Tooltip("How long to wait after a boost has been used before it can be used again, in seconds.")]
        public float boostCoolOff = 0;
        [Tooltip("The value by which to multiply the speed and acceleration of the car when a boost is used.")]
        public float boostMultiplier = 2;

        private float boostTimer = 0;
        
        private void Update()
        {
            var steerLeftInput = 0;
            if (Input.GetKey(KeyCode.A))
            {
                steerLeftInput = 1;
            }

            var steerRightInput = 0;
            if (Input.GetKey(KeyCode.D))
            {
                steerRightInput = 1;
            }

            var forwardInput = 0;
            if (Input.GetKey(KeyCode.W))
            {
                forwardInput = 1;
            }

            var reverseInput = 0;
            if (Input.GetKey(KeyCode.S))
            {
                reverseInput = 1;
            }
            
            var boostInput = 0;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                boostInput = 1;
            }
            double[] doubleArray = new double[carController.inputs.Length];
            for (int i = 0; i < carController.inputs.Length; i++)
            {
                doubleArray[i] = (double)carController.inputs[i];
            }
            learnManager.LearnDataSet.data.Add(new LearnData(doubleArray, new double[]{forwardInput, reverseInput, steerLeftInput, steerRightInput, boostInput}));
            
            float motorDelta = forwardInput - reverseInput;
            float steeringDelta = steerRightInput - steerLeftInput;
            
            if (boostInput == 1 && boostTimer == 0)
            {
                boostTimer = boostCoolOff + boostDuration;
            }
            else if (boostTimer > 0)
            {
                boostTimer = Mathf.Max(boostTimer - Time.deltaTime, 0);
                carController.setBoostMultiplier(boostTimer > boostCoolOff ? boostMultiplier : 1);
            }

            carController.setSteering(steeringDelta);
            carController.setMotor(motorDelta);
        }
    }
}