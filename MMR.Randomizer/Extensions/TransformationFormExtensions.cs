using MMR.Common.Extensions;
using MMR.Randomizer.Attributes;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Extensions;

public static class TransformationFormExtensions
{
    public static Instrument? DefaultInstrument(this TransformationForm form)
    {
        return form.GetAttribute<DefaultInstrumentAttribute>()?.Default;
    }

    public static byte Id(this TransformationForm form)
    {
        return form.GetAttribute<IdAttribute>().Id;
    }
}
