using System;
using System.Reflection;

namespace Ray.EssayNotes.AutoFac.NetFrameworkApi.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}