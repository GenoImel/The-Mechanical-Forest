using Newtonsoft.Json.Serialization;
using System;
using Akashic.Runtime.Serializers.Save;

namespace Akashic.Runtime.Serializers.Settings
{
	internal sealed class SaveFileSerializationBinder : ISerializationBinder
	{
		public Type BindToType(string assemblyName, string typeName)
		{
			switch (typeName)
			{
				case "ConsumableItem":
					return typeof(ConsumableItem);
				case "AccessoryItem":
					return typeof(AccessoryItem);
				case "RelicItem":
					return typeof(RelicItem);
				default:
					throw new InvalidOperationException($"Unrecognized typeName: {typeName}");
			}
		}

		public void BindToName(Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = serializedType.Name;
		}
	}
}
