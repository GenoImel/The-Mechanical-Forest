using Akashic.Core;
using UnityEngine;

namespace Akashic.Runtime
{
    internal sealed class AkashicGameManager : GameManager
    {
        //[Header("Services")] 
        //[SerializeField] private ExampleService exampleService;

        [Header("Management")] 
        [SerializeField] private Transform controllerParentTransform;
        
        [SerializeField] private Transform servicesParentTransform;

        protected override string GetApplicationName()
        {
            return nameof(AkashicGameManager);
        }

        protected override void OnInitialized()
        {
            //AddService<ExampleService, IExampleService>(ExampleService);

            controllerParentTransform.gameObject.SetActive(true);
            servicesParentTransform.gameObject.SetActive(true);
        }
    }
}