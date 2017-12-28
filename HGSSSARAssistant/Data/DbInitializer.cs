using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HGSSSARAssistant.Models;
using Microsoft.AspNetCore.Identity;

namespace HGSSSARAssistant.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            IdentityRole role1 = new IdentityRole
            {
                Name = "Spašavatelj"
            };
            IdentityRole role2 = new IdentityRole
            {
                Name = "Admin"
            };

            if (!context.Roles.Any())
            {

                context.Roles.Add(role1);
                context.Roles.Add(role2);
            }

            List<Station> stationList = new List<Station>
            {
                new Station
                {
                    Title = "Stanica1"
                },
                new Station
                {
                    Title = "Stanica2"
                }
            };
            if (context.Stations.Any())
            {
                foreach (var item in stationList)
                {
                    context.Stations.Add(item);
                }
            }

            List<Category> categoryList = new List<Category>
            {
                new Category
                {
                    Name = "Pripravnik"
                },
                new Category
                {
                    Name = "Spasavatelj"
                }
            };

            List<ApplicationUser> userList = new List<ApplicationUser>
            {
                new ApplicationUser {
                FirstName = "Ivan",
                LastName = "Horvat",
                UserName = "Ivan",
                Station = stationList[0],
                Category = categoryList[0]
            }, new ApplicationUser {
                FirstName = "Andrija",
                LastName = "Perusic",
                UserName = "Pero",
                Station = stationList[1],
                Category = categoryList[1]
            }};

            if (!context.Users.Any())
            {
                foreach (var item in userList)
                {
                    context.Users.Add(item);
                }
            }

            IdentityUserRole<string> userRole1 = new IdentityUserRole<string> {
                UserId = userList[0].Id,
                RoleId = role1.Id
            };
            IdentityUserRole<string> userRole2 = new IdentityUserRole<string>
            {
                UserId = userList[1].Id,
                RoleId = role2.Id
            };

            if (!context.UserRoles.Any())
            {
                context.UserRoles.Add(userRole1);
                context.UserRoles.Add(userRole2);
            }

            List<Skill> skillList = new List<Skill>
            {
                new Skill
                {
                    Name = "Kartografija"
                },
                new Skill
                {
                    Name = "Ronjenje"
                }
            };

            if (!context.Skills.Any())
            {
                foreach (var item in skillList)
                {
                    context.Skills.Add(item);
                }
            }

            List<RescuerSkill> rescuerSkillList = new List<RescuerSkill>
            {
                new RescuerSkill
                {
                    Rescuer = userList[0],
                    Skill = skillList[0]
                },
                new RescuerSkill
                {
                    Rescuer = userList[1],
                    Skill = skillList[1]
                }
            };

            if (context.RescuerSkills.Any())
            {
                foreach (var item in rescuerSkillList)
                {
                    context.RescuerSkills.Add(item);
                }
            }

            List<ActionType> actionTypeList = new List<ActionType>
            {
                new ActionType
                {
                    Name = "Spasavanje"
                },
                new ActionType
                {
                    Name = "Potraga"
                }
            };

            if (context.ActionTypes.Any())
            {
                foreach (var item in actionTypeList)
                {
                    context.ActionTypes.Add(item);
                }
            }

            List<Models.Action> actionList = new List<Models.Action>
            {
                new Models.Action
                {
                    Title = "Akcija 1",
                    DateTime = DateTime.Now,
                    ActionType = actionTypeList[0]
                },
                new Models.Action
                {
                    Title = "Akcija 2",
                    DateTime = DateTime.Now,
                    ActionType = actionTypeList[1]
                }
            };

            if (context.Actions.Any())
            {
                foreach (var item in actionList)
                {
                    context.Actions.Add(item);
                }
            }

            List<RescuerAction> rescuerActionsList = new List<RescuerAction>
            {
                new RescuerAction
                {
                    Action = actionList[0],
                    Rescuer = userList[0]
                },
                 new RescuerAction
                {
                    Action = actionList[1],
                    Rescuer = userList[1]
                },
            };

            if (context.RescuerActions.Any())
            {
                foreach (var item in rescuerActionsList)
                {
                    context.RescuerActions.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}
