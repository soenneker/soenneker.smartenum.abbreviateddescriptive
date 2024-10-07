using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.SmartEnum.AbbreviatedDescriptive.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.SmartEnum.AbbreviatedDescriptive.Tests;

[Collection("Collection")]
public class AbbreviatedDescriptiveSmartEnumTests : FixturedUnitTest
{
    private readonly IAbbreviatedDescriptiveSmartEnum _util;

    public AbbreviatedDescriptiveSmartEnumTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IAbbreviatedDescriptiveSmartEnum>(true);
    }
}
