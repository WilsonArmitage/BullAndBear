using PortfolioAPI.Constraints;
using PortfolioAPI.SDK.Enumerations;

namespace PortfolioAPI.Tests
{
    public class ParameterTransformer_Tests
    {
        [Theory]
        [InlineData("0", VantageVerbs.statement)]
        [InlineData("1", VantageVerbs.overview)]
        [InlineData("2", VantageVerbs.daily)]
        [InlineData("3", VantageVerbs.quote)]
        [InlineData("statement", VantageVerbs.statement)]
        [InlineData("overview", VantageVerbs.overview)]
        [InlineData("daily", VantageVerbs.daily)]
        [InlineData("quote", VantageVerbs.quote)]
        public void VantageVerb_ValidVerb(object parameter, VantageVerbs expectedResult)
        {
            VantageVerbParameterTransformer transformer = new VantageVerbParameterTransformer();

            string? result = transformer.TransformOutbound(parameter);

            Assert.NotNull(result);
            Assert.True(Enum.TryParse<VantageVerbs>(result, out VantageVerbs resultVerb));
            Assert.Equal(expectedResult, resultVerb);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        public void VantageVerb_InvalidVerb(object parameter, string? expectedResult)
        {
            VantageVerbParameterTransformer transformer = new VantageVerbParameterTransformer();

            string? result = transformer.TransformOutbound(parameter);

            Assert.Equal(expectedResult, result);
        }
    }
}