using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneOf;

namespace OneOfTest.Json;

public class OneOfJsonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is IOneOf v)
        {
            serializer.Serialize(writer, v.Value);
        }
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.StartObject)
        {
            return existingValue;
        }

        var obj = JObject.Load(reader);
        var instance = DeserializeToDiscriminatedUnion(obj, objectType);

        return Activator.CreateInstance(objectType, instance);
    }

    private object DeserializeToDiscriminatedUnion(JObject obj, Type objectType)
    {
        var oneOfCaseTypes = objectType?.BaseType?.GenericTypeArguments.ToList() ?? new List<Type>();
        object result = null;
        foreach (var oneOfDiscriminatedUnion in oneOfCaseTypes)
        {
            string json = obj.ToString();
            var oneOfCase = JsonConvert.DeserializeObject(json, oneOfDiscriminatedUnion);
            if (oneOfCase is not null)
            {
                return oneOfCase;
            }
        }

        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IOneOf));
    }
}