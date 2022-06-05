using PortfolioAPI.SDK.Enumerations;

namespace PortfolioAPI.Constraints
{
    public class VantageVerbParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value != null 
                && !string.IsNullOrEmpty(value.ToString())
            ) {
                Enum.TryParse((string)value, out VantageVerbs vantageVerb);

                return vantageVerb.ToString();
            }
            
            return null;
        }
    }
}
