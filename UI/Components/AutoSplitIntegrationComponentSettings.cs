using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;

namespace LiveSplit.UI.Components
{
    public partial class AutoSplitIntegrationComponentSettings : UserControl
    {
        private readonly AutoSplitIntegrationComponent component;
        private readonly LiveSplitState state;

        internal string LabelAutoSplitVersion_Text
        {
            set => SetControlProperty(labelAutoSplitVersion, nameof(labelAutoSplitVersion.Text), "AutoSplit version: " + value);
        }

        internal bool ButtonStartAutoSplit_Enabled
        {
            set => SetControlProperty(buttonStartAutoSplit, nameof(buttonKillAutoSplit.Enabled), value);
        }

        internal bool ButtonKillAutoSplit_Enabled
        {
            set => SetControlProperty(buttonKillAutoSplit, nameof(buttonKillAutoSplit.Enabled), value);
        }

        private void SetControlProperty<T>(Control control, string propertyName, T value) where T : IEquatable<T>
        {
            try
            {
                PropertyInfo property = control.GetType().GetProperty(propertyName);

                if (value.Equals((T)property.GetValue(control)))
                    return;

                var setProperty = new Action(() =>
                {
                    property.SetValue(control, value);
                });

                if (control.InvokeRequired)
                    Invoke(setProperty);

                else
                    setProperty();
            }

            catch { }
        }

        internal AutoSplitIntegrationComponentSettings(AutoSplitIntegrationComponent component)
        {
            this.component = component;
            state = component.State;

            InitializeComponent();

            checkBoxGameTimePausing.Enabled = state.CurrentPhase == TimerPhase.NotRunning;
        }

        internal void OnStart() => checkBoxGameTimePausing.Enabled = false;

        internal void OnReset() => checkBoxGameTimePausing.Enabled = true;

        internal XmlNode GetSettings(XmlDocument document)
        {
            XmlElement settingsElement = document.CreateElement("Settings");

            SettingsHelper.CreateSetting(document, settingsElement, "Version", "1.8");
            SettingsHelper.CreateSetting(document, settingsElement, "AutoSplitPath", component.AutoSplitPath);
            SettingsHelper.CreateSetting(document, settingsElement, "SettingsPath", component.SettingsPath);
            SettingsHelper.CreateSetting(document, settingsElement, "GameTimePausing", component.GameTimePausing);

            return settingsElement;
        }

        internal void SetSettings(XmlNode settings)
        {
            if (((XmlElement)settings).IsEmpty)
                return;

            component.AutoSplitPath = textBoxAutoSplitPath.Text = SettingsHelper.ParseString(settings["AutoSplitPath"]);
            component.SettingsPath = textBoxSettingsPath.Text = SettingsHelper.ParseString(settings["SettingsPath"]);
            component.GameTimePausing = checkBoxGameTimePausing.Checked = SettingsHelper.ParseBool(settings["GameTimePausing"]);

            if (component.AutoSplit != null)
            {
                LabelAutoSplitVersion_Text = component.AutoSplit.Version;
                component.AutoSplit.IsRunning = component.AutoSplit.IsRunning;
            }
        }

        private void ButtonAutoSplitPathBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "AutoSplit.exe|*.exe"
            };

            if (File.Exists(component.AutoSplitPath))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(component.AutoSplitPath);
                dialog.FileName = Path.GetFileName(component.AutoSplitPath);
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                component.AutoSplitPath = textBoxAutoSplitPath.Text = dialog.FileName;

                component.StartAutoSplit();
            }
        }

        private void ButtonSettingsPathBrowse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "AutoSplit Settings (*.pkl; *.toml)|*.pkl;*.toml|All files (*.*)|*.*"
            };

            if (File.Exists(component.SettingsPath))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(component.SettingsPath);
                dialog.FileName = Path.GetFileName(component.SettingsPath);
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                component.SettingsPath = textBoxSettingsPath.Text = dialog.FileName;
                component.LoadAutoSplitSettings();
            }
        }

        private void ButtonStartAutoSplit_Click(object sender, EventArgs e) => component.StartAutoSplit();

        private void ButtonKillAutoSplit_Click(object sender, EventArgs e) => component.KillAutoSplit();

        private void CheckBoxGameTimePausing_CheckedChanged(object sender, EventArgs e) => component.GameTimePausing = checkBoxGameTimePausing.Checked;

        private void TextBoxAutoSplitPath_TextChanged(object sender, EventArgs e) => component.AutoSplitPath = textBoxAutoSplitPath.Text;

        private void TextBoxSettingsPath_TextChanged(object sender, EventArgs e) => component.SettingsPath = textBoxSettingsPath.Text;

    }
}