using Soenneker.Tests.HostedUnit;

namespace Soenneker.SmartEnum.AbbreviatedDescriptive.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class AbbreviatedDescriptiveSmartEnumTests : HostedUnitTest
{
    public AbbreviatedDescriptiveSmartEnumTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
