using Xunit;
using Moq;
using UserModule;
using Core.Interfaces;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserControllerTests
{
    private readonly Mock<IApplicationUserService> _mockUserService;
    
    public UserControllerTests()
    {
        _mockUserService = new Mock<IApplicationUserService>();
       
    }


}
