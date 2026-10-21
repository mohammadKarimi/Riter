using System.Windows.Input;
using Riter.Services;
using Xunit;

namespace Riter.Tests.Services;
public class HotKeyGestureParserTests
{
    [Fact]
    public void TryParse_ShouldParseCtrlPlusKey()
    {
        bool result = HotKeyGestureParser.TryParse("CTRL + A", out Key key, out ModifierKeys modifiers);

        result.Should().BeTrue();
        key.Should().Be(Key.A);
        modifiers.Should().Be(ModifierKeys.Control);
    }

    [Fact]
    public void TryParse_ShouldParseKeyWithoutModifiers()
    {
        bool result = HotKeyGestureParser.TryParse("D", out Key key, out ModifierKeys modifiers);

        result.Should().BeTrue();
        key.Should().Be(Key.D);
        modifiers.Should().Be(ModifierKeys.None);
    }

    [Fact]
    public void TryParse_ShouldParseMultipleModifiers()
    {
        bool result = HotKeyGestureParser.TryParse("CTRL + SHIFT + P", out Key key, out ModifierKeys modifiers);

        result.Should().BeTrue();
        key.Should().Be(Key.P);
        modifiers.Should().Be(ModifierKeys.Control | ModifierKeys.Shift);
    }

    [Fact]
    public void TryParse_ShouldParseDigitKey()
    {
        bool result = HotKeyGestureParser.TryParse("D1", out Key key, out ModifierKeys modifiers);

        result.Should().BeTrue();
        key.Should().Be(Key.D1);
        modifiers.Should().Be(ModifierKeys.None);
    }

    [Theory]
    [InlineData("")]
    [InlineData("CTRL")]
    [InlineData("CTRL + NotAKey")]
    public void TryParse_ShouldReturnFalse_WhenNoKeyTokenParsed(string combination)
    {
        bool result = HotKeyGestureParser.TryParse(combination, out Key key, out ModifierKeys modifiers);

        result.Should().BeFalse();
        key.Should().Be(Key.None);
    }

    [Fact]
    public void TryParse_ShouldReturnFalse_WhenCombinationIsNull()
    {
        bool result = HotKeyGestureParser.TryParse(null, out Key key, out ModifierKeys modifiers);

        result.Should().BeFalse();
        key.Should().Be(Key.None);
        modifiers.Should().Be(ModifierKeys.None);
    }
}
