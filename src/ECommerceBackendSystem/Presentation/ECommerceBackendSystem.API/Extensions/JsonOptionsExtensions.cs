using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace ECommerceBackendSystem.API.Extensions;

public static class JsonOptionsExtensions
{
    public static IMvcBuilder AddJsonOptions(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            x.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);
            x.JsonSerializerOptions.WriteIndented = true;
            x.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        });

        return builder;
    }
}
