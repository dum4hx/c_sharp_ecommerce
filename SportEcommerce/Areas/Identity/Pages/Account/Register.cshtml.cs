// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportEcommerce.Data;
using SportEcommerce.Models.User;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace SportEcommerce.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<ApplicationRole> _roleManager;

        // Application db context
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _roleManager = roleManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100,  ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            // Custom properties for user registration
            // Name
            [Required(ErrorMessage = "Firts name is required.")]
            [StringLength(40, ErrorMessage = "First name cannot exceed 40 characters.")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; } = string.Empty;

            [StringLength(40, ErrorMessage = "Last name cannot exceed 40 characters.")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "First surname is required")]
            [StringLength(40, ErrorMessage = "First surname cannot exceed 40 characters.")]
            [Display(Name = "First Surname")]
            public required string FirstSurname { get; set; } = string.Empty;

            [StringLength(40, ErrorMessage = "Last surname cannot exceed 40 characters.")]
            [Display(Name = "Last Surname")]
            public string LastSurname { get; set; }

            [Display(Name = "Country")]
            public int CountryId { get; set; }

            [Required]
            [StringLength(500)]
            [Display(Name = "Shipment address")]
            public string ShipmentAddress{ get; set; }

            [Required(ErrorMessage = "Please select a role.")]
            [StringLength(36)]
            [Display(Name = "Rol")]
            public string RoleId { get; set; }
        }

        // List of database countries
        public SelectList Countries { get; set; }

        public SelectList Roles { get; set; }

        /// <summary>
        /// Populates an existing Register.Countries with countries from the database. This method does not return any value.
        /// </summary>
        private void PopulateCountries()
        {
            Countries = new SelectList(_context.Countries.OrderBy(c => c.Name), "Id", "Name");
        }

        private void PopulateRoles()
        {
            Roles = new SelectList(_context.Roles.OrderBy(r => r.Name), "Id", "Name");
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            // Populate the countries dropdown list
            PopulateCountries();

            // Populate the roles dropdown list
            PopulateRoles();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // Populate countries and roles in case of failure
            PopulateCountries();
            PopulateRoles();

            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            
            if (ModelState.IsValid)
            {
                // Insert user and user role in database transaction
                using var transaction = await _context.Database.BeginTransactionAsync();

                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    // Insert user role-specific data
                    var selectedRol = await _roleManager.FindByIdAsync(Input.RoleId);
                    
                    if (selectedRol == null)
                    {
                        return Page();
                    }

                    // Insert into desired role table
                    if (selectedRol.Name == "Buyer")
                    {
                        Buyer buyer = new Buyer { 
                            Id = user.Id, 
                            ShipmentAddress = Input.ShipmentAddress, 
                            User = user};
                        
                        _context.Buyers.Add(buyer);
                    }
                    else if (selectedRol.Name == "Seller")
                    {
                        Seller seller = new Seller
                        {
                            Id = user.Id,
                            User = user
                        };
                        _context.Sellers.Add(seller);
                    }
                    _context.SaveChanges();

                    await transaction.CommitAsync();
                    await _userManager.AddToRoleAsync(user, selectedRol.Name);

                    _logger.LogInformation("User created with role-specific data.");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                    
                    //var userId = await _userManager.GetUserIdAsync(user);
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return new ApplicationUser { 
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    FirstSurname = Input.FirstSurname,
                    LastSurname = Input.LastSurname,
                    CountryId = Input.CountryId
                    };
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
