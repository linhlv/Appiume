using System;

namespace Appiume.Apm.Web.WebApi.Modeling
{
    [Serializable]
    public class ReturnValueApiDescriptionModel
    {
        public Type Type { get; }
        public string TypeAsString { get; }

        public ReturnValueApiDescriptionModel(Type type)
        {
            Type = type;
            TypeAsString = type.FullName;
        }
    }
}