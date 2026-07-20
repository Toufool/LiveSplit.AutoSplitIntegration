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
        private readonly Setting[] settingDefs;

        internal string LabelAutoSplitVersion_Text
        {
            set => SetControlProperty(labelAutoSplitVersion, nameof(Label.Text), "AutoSplit version: " + value);
        }

        internal bool ButtonStartAutoSplit_Enabled
        {
            set => SetControlProperty(buttonStartAutoSplit, nameof(Button.Enabled), value);
        }

        internal bool ButtonKillAutoSplit_Enabled
        {
            set => SetControlProperty(buttonKillAutoSplit, nameof(Button.Enabled), value);
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

            // Control was disposed or its handle destroyed mid-update (e.g. AutoSplit
            // process events firing while the layout is being torn down on shutdown).
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        internal AutoSplitIntegrationComponentSettings(AutoSplitIntegrationComponent component)
        {
            this.component = component;
            state = component.State;

            InitializeComponent();

            settingDefs = BuildSettingDefs();
            foreach (Setting setting in settingDefs)
                setting.Bind();

            checkBoxGameTimePausing.Enabled = state.CurrentPhase == TimerPhase.NotRunning;
        }

        internal void OnStart() => checkBoxGameTimePausing.Enabled = false;

        internal void OnReset() => checkBoxGameTimePausing.Enabled = true;

        internal XmlNode GetSettings(XmlDocument document)
        {
            XmlElement settingsElement = document.CreateElement("Settings");

            SettingsHelper.CreateSetting(document, settingsElement, "Version", "1.8");
            foreach (Setting setting in settingDefs)
                setting.Save(document, settingsElement);

            return settingsElement;
        }

        internal void SetSettings(XmlNode settings)
        {
            if (((XmlElement)settings).IsEmpty)
                return;

            foreach (Setting setting in settingDefs)
                setting.Load(settings);

            if (component.AutoSplit != null)
            {
                LabelAutoSplitVersion_Text = component.AutoSplit.Version;
                component.AutoSplit.IsRunning = component.AutoSplit.IsRunning;
            }
        }

        private void BrowseForFile(string filter, string currentPath, TextBox pathTextBox, Action<string> onSelected)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = filter,
            };

            if (File.Exists(currentPath))
            {
                dialog.InitialDirectory = Path.GetDirectoryName(currentPath);
                dialog.FileName = Path.GetFileName(currentPath);
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = dialog.FileName;
                onSelected(dialog.FileName);
            }
        }

        private void ButtonAutoSplitPathBrowse_Click(object sender, EventArgs e) => BrowseForFile(
            "AutoSplit.exe|*.exe",
            component.AutoSplitPath,
            textBoxAutoSplitPath,
            path =>
            {
                component.AutoSplitPath = path;
                component.StartAutoSplit();
            });

        private void ButtonSettingsPathBrowse_Click(object sender, EventArgs e) => BrowseForFile(
            "AutoSplit Settings (*.pkl; *.toml)|*.pkl;*.toml|All files (*.*)|*.*",
            component.SettingsPath,
            textBoxSettingsPath,
            path =>
            {
                component.SettingsPath = path;
                component.LoadAutoSplitSettings();
            });

        private void ButtonStartAutoSplit_Click(object sender, EventArgs e) => component.StartAutoSplit();

        private void ButtonKillAutoSplit_Click(object sender, EventArgs e) => component.KillAutoSplit();

        // Registry of persisted settings. Each entry two-way binds a control to a
        // component property and knows how to save/load itself. Adding a setting =
        // one row here (plus the component property and the Designer control) — no
        // change-handler, no GetSettings/SetSettings edits.
        private Setting[] BuildSettingDefs() =>
        [
            TextSetting("AutoSplitPath", textBoxAutoSplitPath, nameof(component.AutoSplitPath), v => component.AutoSplitPath = v),
            TextSetting("SettingsPath", textBoxSettingsPath, nameof(component.SettingsPath), v => component.SettingsPath = v),
            BoolSetting("GameTimePausing", checkBoxGameTimePausing, nameof(component.GameTimePausing), v => component.GameTimePausing = v),
        ];

        private Setting TextSetting(string key, TextBox box, string boundProperty, Action<string> set) => new(
            bind: () => box.DataBindings.Add(nameof(TextBox.Text), component, boundProperty, false, DataSourceUpdateMode.OnPropertyChanged),
            save: (document, parent) => SettingsHelper.CreateSetting(document, parent, key, box.Text),
            load: settings =>
            {
                string value = SettingsHelper.ParseString(settings[key]);
                box.Text = value;   // reflect in the UI
                set(value);         // and in the component: bindings aren't live until the panel is shown
            });

        private Setting BoolSetting(string key, CheckBox box, string boundProperty, Action<bool> set) => new(
            bind: () => box.DataBindings.Add(nameof(CheckBox.Checked), component, boundProperty, false, DataSourceUpdateMode.OnPropertyChanged),
            save: (document, parent) => SettingsHelper.CreateSetting(document, parent, key, box.Checked),
            load: settings =>
            {
                bool value = SettingsHelper.ParseBool(settings[key]);
                box.Checked = value;
                set(value);
            });

        /// <summary>
        /// One persisted setting: a two-way binding between a control and a component
        /// property, plus how it saves to and loads from the layout XML. The stable XML
        /// key is passed explicitly so it stays decoupled from the code identifier.
        /// </summary>
        private sealed class Setting(Action bind, Action<XmlDocument, XmlElement> save, Action<XmlNode> load)
        {
            public readonly Action Bind = bind;
            public readonly Action<XmlDocument, XmlElement> Save = save;
            public readonly Action<XmlNode> Load = load;
        }

    }
}
