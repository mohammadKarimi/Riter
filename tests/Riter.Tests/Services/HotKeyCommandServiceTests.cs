using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Riter.Core;
using Riter.Services;

namespace Riter.Tests.Services;
public class HotKeyCommandServiceTests
{
    private readonly Mock<AppSettings> _appSettingsMock;
    private readonly Dictionary<HotKey, Action> _hotKeyCommandMap;
    private readonly HotKeyCommandService _service;

    public HotKeyCommandServiceTests()
    {
        _appSettingsMock = new Mock<AppSettings>();
        _hotKeyCommandMap = new Dictionary<HotKey, Action>();
        _service = new HotKeyCommandService(_appSettingsMock.Object);
    }
}
