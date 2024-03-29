﻿using System.Linq;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List(bool? activeOnly = null)
    {
        IEnumerable<User> users;

        if (activeOnly.HasValue)
        {
            users = _userService.GetAll().Where(u => u.IsActive == activeOnly.Value);
        }
        else
        {
            users = _userService.GetAll();
        }

        var items = users.Select(p => new UserListItemViewModel
        {
            Id = p.Id,
            Forename = p.Forename,
            Surname = p.Surname,
            Email = p.Email,
            IsActive = p.IsActive,
            DateOfBirth = p.DateOfBirth
        }) ;

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }
}
