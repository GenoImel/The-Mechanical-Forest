using Akashic.Core;
using Akashic.Core.Messages;

namespace Akashic.Runtime.Controllers.SaveMenu
{
    internal sealed class ShowSaveMenuMessage : IMessage
    {
    }

    internal sealed class HideSaveMenuMessage : IMessage
    {
    }
    
    internal sealed class RequestNewFileNameMessage : IMessage
    {
    }
    
    internal sealed class NewFileNameConfirmedMessage : IMessage
    {
        public string NewFileName { get; }
        
        public NewFileNameConfirmedMessage(string newFileName)
        {
            NewFileName = newFileName;
        }
    }
}