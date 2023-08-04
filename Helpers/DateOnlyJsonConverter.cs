﻿using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SlimPlan.Helpers
{
    public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        #region Fields

        private const string Format = "yyyy-MM-dd";

        #endregion Fields

        #region Methods

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
        }

        #endregion Methods
    }
}