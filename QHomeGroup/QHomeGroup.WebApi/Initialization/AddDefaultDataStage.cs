using Microsoft.AspNetCore.Identity;
using QHomeGroup.Data.EF.Connector;
using QHomeGroup.Data.Entities.Content;
using QHomeGroup.Data.Entities.Introduce;
using QHomeGroup.Data.Entities.System;
using QHomeGroup.Data.Enum;
using QHomeGroup.Utilities.Constants;
using QHomeGroup.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QHomeGroup.Data.Entities.Projects;

namespace QHomeGroup.WebApi.Initialization
{
    public class AddDefaultDataStage : IInitializationStage
    {
        private readonly AppDbContext _context;
        private RoleManager<AppRole> _roleManager;
        private UserManager<AppUser> _userManager;

        public AddDefaultDataStage(AppDbContext context, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public int Order => 2;
        public async Task ExecuteAsync()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }

            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123654$");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            if (_context.Slides.Count() == 0)
            {
                var listImage = new List<string>() { "https://qhomegroup.com/images/slide.png" };
                List<Slide> slides = new List<Slide>()
                {
                    new Slide("Slide-1",SlideOption.Image, new List<string>(),listImage,Status.Active,"Xin chào bạn đến QHome" ),
                    new Slide("Slide-2",SlideOption.Image, new List<string>(),listImage,Status.Active,"Xin chào bạn đến QHome" ),
                    new Slide("Slide-3",SlideOption.Image, new List<string>(),listImage,Status.Active,"Xin chào bạn đến QHome" )
                };
                _context.Slides.AddRange(slides);
            }

            if (_context.IntroduceConfigs.Count() == 0)
            {
                List<IntroduceConfig> configs = new List<IntroduceConfig>
                {
                    new IntroduceConfig(CommonConstants.IntroduceKey.Vision, "https://qhomegroup.com/images/about3.png", "Vision", "Vision" ),
                    new IntroduceConfig(CommonConstants.IntroduceKey.CoreValue, "https://qhomegroup.com/images/about3.png", "CoreValue", "CoreValue" ),
                    new IntroduceConfig(CommonConstants.IntroduceKey.HistoryBegin, "https://qhomegroup.com/images/about3.png", "HistoryBegin", "HistoryBegin" ),
                    new IntroduceConfig(CommonConstants.IntroduceKey.ServiceQuality, "https://qhomegroup.com/images/about3.png", "ServiceQuality", "ServiceQuality" ),
                    new IntroduceConfig(CommonConstants.IntroduceKey.FactoryTechnology, "https://qhomegroup.com/images/about3.png", "FactoryTechnology", "FactoryTechnology" ),
                    new IntroduceConfig(CommonConstants.IntroduceKey.HumanResource, "https://qhomegroup.com/images/about3.png", "HumanResource", "HumanResource" ),
                };
                _context.IntroduceConfigs.AddRange(configs);
            }
            if(_context.HomeConfigs.Count() == 0)
            {
                var config = new HomeConfig("https://www.youtube-nocookie.com/embed/tiR-RZrsNlU", "Test", null, "Test", null, "Test", "Test");
                _context.HomeConfigs.Add(config);
            }
            if (_context.CompanyIntros.Count() == 0)
            {
                var intro = new CompanyIntro("Công ty cổ phần QHOME", "Tòa nhà Q.HOME, đường Bắc Nam, khu đô thị Hồ Xương Rồng, phường Phan Đình Phùng, TP Thái Nguyên, Thái Nguyên", "Tòa nhà Q.HOME, đường Bắc Nam, khu đô thị Hồ Xương Rồng, phường Phan Đình Phùng, TP Thái Nguyên, Thái Nguyên", null, "(0208) 3652 728", "0987 360 088", "contact@qhomegroup.com", "qhomegroup.com");
                _context.CompanyIntros.Add(intro);
            }
            await _context.SaveChangesAsync();
        }
    }
}
