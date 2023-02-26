

using System.ComponentModel;

namespace CivitaiApi.CivitaiRequestParams
{
    public enum TypesEnum
    {
        [Description("Checkpoint")]
        Checkpoint,
        [Description("Textual Inversion")]
        TextualInversion,
        [Description("Hypernetwork")]
        Hypernetwork,
        [Description("Aesthetic Gradient")]
        AestheticGradient,
        [Description("LORA")]
        LORA
    }
}
