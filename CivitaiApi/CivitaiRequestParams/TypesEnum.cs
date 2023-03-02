

using System.ComponentModel;

namespace CivitaiApi.CivitaiRequestParams
{
    public enum TypesEnum
    {
        [Description("Checkpoint")]
        Checkpoint,
        [Description("TextualInversion")]
        TextualInversion,
        [Description("Hypernetwork")]
        Hypernetwork,
        [Description("AestheticGradient")]
        AestheticGradient,
        [Description("LORA")]
        LORA,
        [Description("Controlnet")]
        Controlnet,
        [Description("Poses")]
        Poses
    }
}
