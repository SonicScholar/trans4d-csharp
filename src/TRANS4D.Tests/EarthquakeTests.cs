using static TRANS4D.BlockData.Earthquake;

public class EarthquakeTests
{
    [Fact]
    public void InitializeEarthquake_Succeeds()
    {
        var depth = DEPTH;
        var dip = DIP;
        var dslip = DSLIP;
        var eqlat = EQLAT;
        var eqlatr = EQLATR;
        var eqlon = EQLON;
        var eqlonr = EQLONR;
        var eqrad = EQRAD;
        var hl = HL;
        var iteqk = ITEQK;
        var nfp = NFP;
        var nloc = NLOC;
        var sslip = SSLIP;
        var strike = STRIKE;
        var width = WIDTH;

        Assert.NotNull(depth);
        Assert.NotNull(dip);
        Assert.NotNull(eqlat);
        Assert.NotNull(eqlatr);
        Assert.NotNull(eqlon);
        Assert.NotNull(eqlonr);
        Assert.NotNull(eqrad);
        Assert.NotNull(hl);
        Assert.NotNull(iteqk);
        Assert.NotNull(nfp);
        Assert.NotNull(nloc);
    }
}