using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;
using LiveSplit.Model;
using LiveSplit.UI.Components;

namespace LiveSplit.PreviewTool
{
    // Renders AutoSplitIntegrationComponentSettings without a live LiveSplit host.
    // The component subscribes to LiveSplitState events in its ctor, so a real state is
    // built; the settings ctor only reads state.CurrentPhase (defaults to NotRunning).
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LiveSplitState state = MakeState();
            var component = new AutoSplitIntegrationComponent(state);

            // Settings is internal; the tool is a separate assembly, so reach it by reflection.
            var settings = (Control)typeof(AutoSplitIntegrationComponent)
                .GetProperty("Settings", BindingFlags.Instance | BindingFlags.NonPublic)!
                .GetValue(component)!;

            using var form = new Form
            {
                ClientSize = settings.Size,
                Text = "AutoSplit Integration Settings (preview)",
                StartPosition = FormStartPosition.CenterScreen,
            };
            form.Controls.Add(settings);
            Application.Run(form);
        }

        private static LiveSplitState MakeState()
        {
            // LiveSplitState(run, form, layout, layoutSettings, settings). Only CurrentPhase is
            // read here (enum default NotRunning), so nulls are fine; fall back to an
            // uninitialized instance if a future ctor change starts dereferencing them.
            try
            {
                return (LiveSplitState)Activator.CreateInstance(
                    typeof(LiveSplitState), new object?[] { null, null, null, null, null })!;
            }
            catch
            {
                return (LiveSplitState)FormatterServices.GetUninitializedObject(typeof(LiveSplitState));
            }
        }
    }
}
