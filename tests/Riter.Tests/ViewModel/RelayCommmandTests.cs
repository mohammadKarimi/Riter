using Riter.Core;

namespace Riter.Tests.ViewModel;

public class RelayCommandTests
{
    [Fact]
    public void Should_ExecuteAction_When_RelayCommandCalled()
    {
        var executed = false;
        var command = new RelayCommand(() => executed = true);
        command.Execute("Test");
        executed.Should().BeTrue();
    }

    [Fact]
    public void Should_NotExecute_When_CanExecuteReturnsFalse()
    {
        var executed = false;
        var command = new RelayCommand(() => executed = true, () => false);
        var canExecute = command.CanExecute("Test");
        canExecute.Should().BeFalse();
        executed.Should().BeFalse();
    }
}
