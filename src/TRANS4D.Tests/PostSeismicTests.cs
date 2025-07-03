using static TRANS4D.BlockData.PostSeismic;

namespace TRANS4D.Tests;

public class PostSeismicTests
{
    [Fact]
    public void InitializePostSeismic_Succeeds()
    {
        var ps = PS;
        var psglx = PSGLX;
        var psgux = PSGUX;
        var psgly = PSGLY;
        var psguy = PSGUY;
        var icntpx = ICNTPX;
        var icntpy = ICNTPY;
        var nbasep = NBASEP;

        Assert.NotNull(ps);
        Assert.NotNull(psglx);
        Assert.NotNull(psgux);
        Assert.NotNull(psgly);
        Assert.NotNull(psguy);
        Assert.NotNull(icntpx);
        Assert.NotNull(icntpy);
        Assert.NotNull(nbasep);
    }
}