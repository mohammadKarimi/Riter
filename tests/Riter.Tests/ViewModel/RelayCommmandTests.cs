namespace Riter.Tests.ViewModel;

public class RelayCommandTests
{
    [Fact]
    public void RelayCommand_ShouldExecute_Action()
    {
        var executed = false;
        var command = new RelayCommand<string>(param => executed = true);
        command.Execute("Test");
        executed.Should().BeTrue();
    }

    [Fact]
    public void RelayCommand_ShouldNotExecute_WhenCanExecuteReturnsFalse()
    {
        var executed = false;
        var command = new RelayCommand<string>(param => executed = true, param => false);
        var canExecute = command.CanExecute("Test");
        canExecute.Should().BeFalse();
        executed.Should().BeFalse();
    }
}
