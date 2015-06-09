#region using block

using System;
using System.Reflection;

#endregion

namespace DevBootstrapper.Areas.HelpPage.ModelDescriptions {
    public interface IModelDocumentationProvider {
        string GetDocumentation(MemberInfo member);
        string GetDocumentation(Type type);
    }
}