using Akashic.Core.Messages;
using Akashic.Runtime.Actors.Battle.Environment;
using Akashic.Runtime.ScriptableObjects.Battle;

namespace Akashic.Runtime.Actors.Battle
{
    internal sealed class LoadBattleEnvironmentFromTriggerMessage : IMessage
    {
      public readonly EncounterData EncounterToLoad;
      public readonly BattleEnvironment BattleEnvironmentToLoad;
      
        public LoadBattleEnvironmentFromTriggerMessage(
            EncounterData encounterToLoad,
            BattleEnvironment battleEnvironmentToLoad
            )
        {
            EncounterToLoad = encounterToLoad;
            BattleEnvironmentToLoad = battleEnvironmentToLoad;
        }
    }

    internal sealed class BattleInitializedMessage : IMessage
    {
        
    }
}