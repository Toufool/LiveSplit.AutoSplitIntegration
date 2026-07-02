using System;
using LiveSplit.Model;
using LiveSplit.UI.Components;

[assembly: ComponentFactory(typeof(AutoSplitIntegrationFactory))]

namespace LiveSplit.UI.Components
{
    public class AutoSplitIntegrationFactory : IComponentFactory
    {
        public string ComponentName => "AutoSplit Integration";

        public string Description => "Directly connects AutoSplit with LiveSplit.";

        public ComponentCategory Category => ComponentCategory.Control;

        // Keep in sync with Version in LiveSplit.AutoSplitIntegration.csproj -->
        public Version Version => Version.Parse("1.8.3");

        public string UpdateName => ComponentName;

        public string UpdateURL => "https://raw.githubusercontent.com/Toufool/LiveSplit.AutoSplitIntegration/main/update/Components/";

        public string XMLURL => "https://raw.githubusercontent.com/Toufool/LiveSplit.AutoSplitIntegration/main/update/Components/update.LiveSplit.AutoSplitIntegration.xml";

        public IComponent Create(LiveSplitState state) => new AutoSplitIntegrationComponent(state);
    }
}
