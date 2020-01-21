using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WooliesXChallenge.Tests
{
    [CollectionDefinition("wooliesx")]
    public class WooliesXCollectionFixture : ICollectionFixture<TestContext>
    {
    }
}
