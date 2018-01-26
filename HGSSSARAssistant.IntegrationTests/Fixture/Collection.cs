using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HGSSSARAssistant.IntegrationTests.Fixture
{
    [CollectionDefinition("ContextCollection")]
    public class Collection : ICollectionFixture<TestContext>
    {
    }
}
