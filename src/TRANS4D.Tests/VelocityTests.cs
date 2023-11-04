using TRANS4D.BlockData;
using static TRANS4D.BlockData.Velocity;
namespace TRANS4D.Tests;

public class VelocityTests
{
    [Fact]
    public void InitializeVelocity_Succeeds()
    {
        var grdLX = GRDLX;
        var grdUX = GRDUX;
        var grdLY = GRDLY;
        var grdUY = GRDUY;
        var icntX = ICNTX;
        var icntY = ICNTY;
        var nbase = NBASE;

        Assert.NotNull(grdLX);
        Assert.NotNull(grdUX);
        Assert.NotNull(grdLY);
        Assert.NotNull(grdUY);
        Assert.NotNull(icntX);
        Assert.NotNull(icntY);
        Assert.NotNull(nbase);
    }
}