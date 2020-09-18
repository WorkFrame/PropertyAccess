using NetEti.ApplicationEnvironment;

namespace NetEti.DemoApplications
{
    public sealed class AppSettings : BasicAppSettings
    {
        public string NewProperty { get; set; }

        private AppSettings() : base()
        {
            this.NewProperty = "PropertyAccessDemo.NewProperty";
        }
    }
}
