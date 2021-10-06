using NetEti.ApplicationEnvironment;

namespace NetEti.DemoApplications
{
    public sealed class AppSettings
    {
        public string NewProperty { get; set; }

        private AppSettings()
        {
            this.NewProperty = "--- PropertyAccessDemo.NewProperty ---";
        }
    }
}
