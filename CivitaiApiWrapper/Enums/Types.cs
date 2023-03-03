using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CivitaiApiWrapper.Enums
{
    public enum Types
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
