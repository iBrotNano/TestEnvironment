using Microsoft.Extensions.Options;

namespace AppExample.Services
{
    public class TestServiceOptions : IOptions<TestServiceOptions>
    {
        #region Properties

        public bool Skip { get; set; } = false;
        public TestServiceOptions Value => this;

        #endregion Properties
    }
}