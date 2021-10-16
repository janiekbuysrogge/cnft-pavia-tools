﻿using System;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace sales_lookup.Models.CNFT.io
{
    // <auto-generated />
    //
    // To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
    //
    //    using QuickType;
    //
    //    var marketplaceSearchResult = MarketplaceSearchResult.FromJson(jsonString);

    public partial class MarketplaceSearchResult
    {
        [JsonProperty("found")]
        public long Found { get; set; }

        [JsonProperty("assets")]
        public Asset[] Assets { get; set; }
    }

    public partial class Asset
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("policy")]
        public string Policy { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("verified")]
        public Verified Verified { get; set; }

        [JsonProperty("paymentSession")]
        public object PaymentSession { get; set; }

        [JsonProperty("sold")]
        public bool Sold { get; set; }

        [JsonProperty("dateListed")]
        public long DateListed { get; set; }
    }

    public partial class Metadata
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("thumbnail")]
        public string[] Thumbnail { get; set; }

        [JsonProperty("mediaType")]
        public object MediaType { get; set; }

        [JsonProperty("files")]
        public object[] Files { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }
    }

    public partial class Tag
    {
        [JsonProperty("-----Info-----", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Info { get; set; }

        [JsonProperty("-----Land Sale 1-----", NullValueHandling = NullValueHandling.Ignore)]
        public LandSale1[] LandSale1 { get; set; }

        [JsonProperty("-----Product Information-----", NullValueHandling = NullValueHandling.Ignore)]
        public ProductInformation[] ProductInformation { get; set; }
    }

    public partial class LandSale1
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("x", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? X { get; set; }

        [JsonProperty("y", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Y { get; set; }
    }

    public partial class ProductInformation
    {
        [JsonProperty("Pavia.io", NullValueHandling = NullValueHandling.Ignore)]
        public Uri PaviaIo { get; set; }

        [JsonProperty("Copyright", NullValueHandling = NullValueHandling.Ignore)]
        public string Copyright { get; set; }
    }

    public partial class Verified
    {
        [JsonProperty("verified")]
        public bool VerifiedVerified { get; set; }

        [JsonProperty("project")]
        public string Project { get; set; }
    }

    public partial class MarketplaceSearchResult
    {
        public static MarketplaceSearchResult FromJson(string json) => JsonConvert.DeserializeObject<MarketplaceSearchResult>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this MarketplaceSearchResult self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}