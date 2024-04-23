using CoreRCON.Extensions.CounterStrike.Parsers;
using CoreRCON.Parsers;

namespace CoreRCON.Extensions.CounterStrike.Tests.Parsers;

public sealed class FragParserTests
{
    [Theory(DisplayName = "Parser: matches and parses")]
    [MemberData(nameof(Data))]
    public void Parser_Matches_And_Parses(string value, Frag frag)
    {
        var parser = new FragParser();
        if (!parser.IsMatch(value))
        {
            Assert.Fail("Input value was not matched by parser.");
        }

        Assert.Equal(frag, parser.Parse(value));
    }

    [Fact(DisplayName = "ParserPool: gets parser")]
    public void ParserPool_Gets_Parser()
    {
        var parser = new ParserPool().Get<Frag>();

        Assert.NotNull(parser);
        Assert.IsType(typeof(FragParser), parser);
    }

    public static TheoryData<string, Frag> Data = new()
    {
        {
            @"""TEST<0><[U:0:123456789]><TERRORIST>"" [0] killed ""TEST1<1><[U:0:123456789]><TERRORIST>"" [0] with ""ak47""",
            new(false, new(1, "TEST1", "[U:0:123456789]", "TERRORIST"), new(0, "TEST", "[U:0:123456789]", "TERRORIST"), "ak47")
        },
        {
            @"""TEST<0><[U:0:123456789]><TERRORIST>"" [0] killed ""TEST1<1><[U:0:123456789]><TERRORIST>"" [0] with ""ak47"" (headshot)",
            new(true, new(1, "TEST1", "[U:0:123456789]", "TERRORIST"), new(0, "TEST", "[U:0:123456789]", "TERRORIST"), "ak47")
        },
    };
}