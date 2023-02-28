using CivitaiApi.CivitaiRequestParams;
using NetCivitaiModelManager.Extensions;

namespace NetCivitaiModelManager.Models
{
    public class TypeToSelect
    {
        public string Name { get; set; }
        public TypesEnum Type { get; set; }
        public TypeToSelect(TypesEnum type)
        {
            Type = type;
            Name = type.GetEnumDescription();
        }
    }
}
