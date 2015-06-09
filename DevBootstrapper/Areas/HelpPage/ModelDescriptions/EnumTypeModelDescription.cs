#region using block

using System.Collections.ObjectModel;

#endregion

namespace DevBootstrapper.Areas.HelpPage.ModelDescriptions {
    public class EnumTypeModelDescription : ModelDescription {
        public EnumTypeModelDescription() {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}