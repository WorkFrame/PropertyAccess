using NetEti.ApplicationEnvironment;
using NetEti.Globals;

namespace NetEti.DemoApplications
{
    /// <summary>
    /// Demo
    /// </summary>
    public partial class Form1 : Form
    {
        private PropertyAccess? _propertyAccess;
        private AppSettings? _appSettings;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this._appSettings = GenericSingletonProvider.GetInstance<AppSettings>();
            this._propertyAccess = new PropertyAccess(this._appSettings);
            this.listBox1.Items.Add(String.Format("{0}: {1}", "NewProperty", this._appSettings.NewProperty));
        }
    }
}
