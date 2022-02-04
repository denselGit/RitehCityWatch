using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CityWatch.Common
{
    public abstract class Data
    {
        JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        [NonSerialized]
        [JsonIgnore]
        public static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        [Computed]
        [JsonIgnore]
        public Dictionary<string, string> AttributesCollection { get; set; }
        public string Attributes
        {
            get { return JsonSerializer.Serialize(AttributesCollection); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    AttributesCollection = new Dictionary<string, string>();
                }
                else
                {
                    AttributesCollection = JsonSerializer.Deserialize<Dictionary<string, string>>(value);
                }
            }
        }

        public void SetAttributeValue(string attribute, string value)
        {
            if (AttributesCollection.ContainsKey(attribute))
            {
                AttributesCollection[attribute] = value;
            }
            else
            {
                AttributesCollection.Add(attribute, value);
            }
        }

        public string GetAttributeValue(string attribute)
        {
            if (AttributesCollection.ContainsKey(attribute)) return AttributesCollection[attribute];

            return string.Empty;
        }

        public T GetAttributeValue<T>(string attribute)
        {
            if (AttributesCollection.ContainsKey(attribute)) return (T)Convert.ChangeType(AttributesCollection[attribute], typeof(T));

            return default(T);
        }
    }
}
