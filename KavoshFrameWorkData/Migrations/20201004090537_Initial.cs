using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KavoshFrameWorkData.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Auditors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auditors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardofDirectorsLegalMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardofDirectorsLegalMembers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyActivityTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyActivityTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPartnershipTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPartnershipTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DomainSetting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TypeName = table.Column<string>(nullable: true),
                    Server = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDegrees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDegrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegalMemberTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalMemberTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Exception = table.Column<string>(nullable: true),
                    Properties = table.Column<string>(nullable: true),
                    LogEvent = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(nullable: true),
                    ActionName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    ActionId = table.Column<string>(nullable: true),
                    IP = table.Column<string>(nullable: true),
                    RequestId = table.Column<string>(nullable: true),
                    RequestPath = table.Column<string>(nullable: true),
                    date_str = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Newspapers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newspapers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shareholders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shareholders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleFormActionAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true),
                    SystemAction = table.Column<int>(nullable: false),
                    SystemFormId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleFormActionAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleFormActionAssignment_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    MainGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubGroups_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    CompanyTypeId = table.Column<int>(nullable: false),
                    CompanyActivityTypeId = table.Column<int>(nullable: true),
                    CompanyPartnershipTypeId = table.Column<int>(nullable: false),
                    EstablishmentDate = table.Column<DateTime>(nullable: true),
                    RegistrationDate = table.Column<DateTime>(nullable: true),
                    NationalID = table.Column<string>(nullable: true),
                    EconomicCode = table.Column<string>(nullable: true),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    RegistrationUnit = table.Column<string>(nullable: true),
                    OperationLicenseNumber = table.Column<string>(nullable: true),
                    CommercialCard = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    NumberOfCompanyBoardMembers = table.Column<int>(nullable: true),
                    ShareholderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyActivityTypes_CompanyActivityTypeId",
                        column: x => x.CompanyActivityTypeId,
                        principalTable: "CompanyActivityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyPartnershipTypes_CompanyPartnershipTypeId",
                        column: x => x.CompanyPartnershipTypeId,
                        principalTable: "CompanyPartnershipTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_CompanyTypes_CompanyTypeId",
                        column: x => x.CompanyTypeId,
                        principalTable: "CompanyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Shareholders_ShareholderId",
                        column: x => x.ShareholderId,
                        principalTable: "Shareholders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyBoardMembers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    BoardofDirectorsLegalMemberId = table.Column<int>(nullable: false),
                    EducationDegreeId = table.Column<int>(nullable: true),
                    AgentId = table.Column<int>(nullable: true),
                    AppointmentDate = table.Column<DateTime>(nullable: true),
                    CompletionDate = table.Column<DateTime>(nullable: true),
                    LastElectionMeetingDate = table.Column<DateTime>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    ResignationDate = table.Column<DateTime>(nullable: true),
                    VerdictDate = table.Column<DateTime>(nullable: true),
                    NewspaperEntryDate = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AgentCompanyId = table.Column<int>(nullable: false),
                    LegalMemberTypeId = table.Column<int>(nullable: false),
                    OrganizationalPositionId = table.Column<int>(nullable: false),
                    FormStatus = table.Column<int>(nullable: false),
                    FormStatusComments = table.Column<string>(nullable: true),
                    NationalCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyBoardMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyBoardMembers_Companies_AgentCompanyId",
                        column: x => x.AgentCompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyBoardMembers_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyBoardMembers_BoardofDirectorsLegalMembers_BoardofDirectorsLegalMemberId",
                        column: x => x.BoardofDirectorsLegalMemberId,
                        principalTable: "BoardofDirectorsLegalMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyBoardMembers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyBoardMembers_EducationDegrees_EducationDegreeId",
                        column: x => x.EducationDegreeId,
                        principalTable: "EducationDegrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyBoardMembers_LegalMemberTypes_LegalMemberTypeId",
                        column: x => x.LegalMemberTypeId,
                        principalTable: "LegalMemberTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyBoardMembers_OrganizationalPositions_OrganizationalPositionId",
                        column: x => x.OrganizationalPositionId,
                        principalTable: "OrganizationalPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MediaType = table.Column<string>(nullable: true),
                    RowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyMedias_Companies_RowId",
                        column: x => x.RowId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMeetingAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    Assignments = table.Column<string>(nullable: true),
                    Report = table.Column<string>(nullable: true),
                    Actions = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    FormStatus = table.Column<int>(nullable: false),
                    FormStatusComments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMeetingAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyMeetingAssignments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyShareholderArchives",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    ShareholderId = table.Column<int>(nullable: false),
                    SubShareholderId = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Shares = table.Column<long>(nullable: false),
                    FormStatus = table.Column<int>(nullable: false),
                    FormStatusComments = table.Column<string>(nullable: true),
                    OwnershipPercentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyShareholderArchives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyShareholderArchives_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyShareholderArchives_Shareholders_ShareholderId",
                        column: x => x.ShareholderId,
                        principalTable: "Shareholders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyShareholderArchives_Shareholders_SubShareholderId",
                        column: x => x.SubShareholderId,
                        principalTable: "Shareholders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyShareholders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    ShareholderId = table.Column<int>(nullable: false),
                    SubShareholderId = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Shares = table.Column<long>(nullable: false),
                    FormStatus = table.Column<int>(nullable: false),
                    FormStatusComments = table.Column<string>(nullable: true),
                    OwnershipPercentage = table.Column<double>(nullable: false),
                    IncludeInFormula = table.Column<bool>(nullable: false),
                    IsMainShareholder = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyShareholders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyShareholders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyShareholders_Shareholders_ShareholderId",
                        column: x => x.ShareholderId,
                        principalTable: "Shareholders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyShareholders_Shareholders_SubShareholderId",
                        column: x => x.SubShareholderId,
                        principalTable: "Shareholders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyShares",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    PreviousCapital = table.Column<long>(nullable: false),
                    PreviousShares = table.Column<long>(nullable: false),
                    CurrentCapital = table.Column<long>(nullable: false),
                    CurrentShares = table.Column<long>(nullable: false),
                    CapitalIncreaseFromAccumulatedProfits = table.Column<double>(nullable: false),
                    CapitalIncreaseFromCash = table.Column<double>(nullable: false),
                    CapitalIncreaseFromShareholderReceivables = table.Column<double>(nullable: false),
                    CapitalIncreaseFromAssetsRevaluation = table.Column<double>(nullable: false),
                    FormStatus = table.Column<int>(nullable: false),
                    FormStatusComments = table.Column<string>(nullable: true),
                    ParValue = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyShares_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyUser_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyUser_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enactments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enactments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enactments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FiscalYears",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    CompanyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiscalYears_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Portfos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    AgentsNumber = table.Column<int>(nullable: false),
                    AggregatePercentageCalculationType = table.Column<int>(nullable: false),
                    AggregatePercentage = table.Column<double>(nullable: false),
                    ShareholderHoldingCommunication = table.Column<bool>(nullable: false),
                    UnderManagementHoldingCommunication = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfos_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyMeetingAssignmentMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MediaType = table.Column<string>(nullable: true),
                    RowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyMeetingAssignmentMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyMeetingAssignmentMedias_CompanyMeetingAssignments_RowId",
                        column: x => x.RowId,
                        principalTable: "CompanyMeetingAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnactmentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    EnactmentId = table.Column<int>(nullable: false),
                    EnactmentTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnactmentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnactmentDetail_Enactments_EnactmentId",
                        column: x => x.EnactmentId,
                        principalTable: "Enactments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnactmentMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MediaType = table.Column<string>(nullable: true),
                    RowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnactmentMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnactmentMedias_Enactments_RowId",
                        column: x => x.RowId,
                        principalTable: "Enactments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyAnnualMeetings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    MainGroupId = table.Column<int>(nullable: false),
                    SubGroupId = table.Column<int>(nullable: false),
                    FiscalYearId = table.Column<int>(nullable: false),
                    HoldingShares = table.Column<long>(nullable: false),
                    DividedProfit = table.Column<long>(nullable: false),
                    BoardBonus = table.Column<long>(nullable: false),
                    TotalProfit = table.Column<double>(nullable: false),
                    BoardRight = table.Column<long>(nullable: false),
                    PrimaryAuditorId = table.Column<int>(nullable: false),
                    SecondaryAuditorId = table.Column<int>(nullable: false),
                    NewspaperId = table.Column<int>(nullable: false),
                    Newspaper2Id = table.Column<int>(nullable: true),
                    NewspaperId2 = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    SocialResponsibility = table.Column<double>(nullable: true),
                    AccountingRight = table.Column<double>(nullable: true),
                    AssemblyType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyAnnualMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyAnnualMeetings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAnnualMeetings_FiscalYears_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAnnualMeetings_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAnnualMeetings_Newspapers_Newspaper2Id",
                        column: x => x.Newspaper2Id,
                        principalTable: "Newspapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAnnualMeetings_Newspapers_NewspaperId",
                        column: x => x.NewspaperId,
                        principalTable: "Newspapers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAnnualMeetings_Auditors_PrimaryAuditorId",
                        column: x => x.PrimaryAuditorId,
                        principalTable: "Auditors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAnnualMeetings_Auditors_SecondaryAuditorId",
                        column: x => x.SecondaryAuditorId,
                        principalTable: "Auditors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyAnnualMeetings_SubGroups_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "SubGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    MainGroupId = table.Column<int>(nullable: false),
                    SubGroupId = table.Column<int>(nullable: false),
                    FiscalYearId = table.Column<int>(nullable: false),
                    FormStatus = table.Column<int>(nullable: false),
                    FormStatusComments = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDocuments_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDocuments_FiscalYears_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDocuments_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyDocuments_SubGroups_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "SubGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyExtraordinaryMeetings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<int>(nullable: false),
                    MainGroupId = table.Column<int>(nullable: false),
                    SubGroupId = table.Column<int>(nullable: false),
                    FiscalYearId = table.Column<int>(nullable: false),
                    Decisions = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    MeetingSubject = table.Column<int>(nullable: false),
                    AssemblyType = table.Column<int>(nullable: false),
                    CapitalChangeFrom = table.Column<long>(nullable: false),
                    CapitalChangeTo = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyExtraordinaryMeetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyExtraordinaryMeetings_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyExtraordinaryMeetings_FiscalYears_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyExtraordinaryMeetings_MainGroups_MainGroupId",
                        column: x => x.MainGroupId,
                        principalTable: "MainGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyExtraordinaryMeetings_SubGroups_SubGroupId",
                        column: x => x.SubGroupId,
                        principalTable: "SubGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortfoShareholderPairs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    PortfoId = table.Column<int>(nullable: false),
                    CompanyId = table.Column<int>(nullable: false),
                    ShareholderId = table.Column<int>(nullable: false),
                    Row = table.Column<int>(nullable: false),
                    Column = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfoShareholderPairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfoShareholderPairs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortfoShareholderPairs_Portfos_PortfoId",
                        column: x => x.PortfoId,
                        principalTable: "Portfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortfoShareholderPairs_Shareholders_ShareholderId",
                        column: x => x.ShareholderId,
                        principalTable: "Shareholders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyDocumentMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MediaType = table.Column<string>(nullable: true),
                    RowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyDocumentMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyDocumentMedias_CompanyDocuments_RowId",
                        column: x => x.RowId,
                        principalTable: "CompanyDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyExtraordinaryMeetingMedias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    AddedDate = table.Column<DateTime>(nullable: true),
                    SystemUserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    MediaType = table.Column<string>(nullable: true),
                    RowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyExtraordinaryMeetingMedias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyExtraordinaryMeetingMedias_CompanyExtraordinaryMeetings_RowId",
                        column: x => x.RowId,
                        principalTable: "CompanyExtraordinaryMeetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyActivityTypeId",
                table: "Companies",
                column: "CompanyActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyPartnershipTypeId",
                table: "Companies",
                column: "CompanyPartnershipTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyTypeId",
                table: "Companies",
                column: "CompanyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ShareholderId",
                table: "Companies",
                column: "ShareholderId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAnnualMeetings_CompanyId",
                table: "CompanyAnnualMeetings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAnnualMeetings_FiscalYearId",
                table: "CompanyAnnualMeetings",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAnnualMeetings_MainGroupId",
                table: "CompanyAnnualMeetings",
                column: "MainGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAnnualMeetings_Newspaper2Id",
                table: "CompanyAnnualMeetings",
                column: "Newspaper2Id");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAnnualMeetings_NewspaperId",
                table: "CompanyAnnualMeetings",
                column: "NewspaperId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAnnualMeetings_PrimaryAuditorId",
                table: "CompanyAnnualMeetings",
                column: "PrimaryAuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAnnualMeetings_SecondaryAuditorId",
                table: "CompanyAnnualMeetings",
                column: "SecondaryAuditorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyAnnualMeetings_SubGroupId",
                table: "CompanyAnnualMeetings",
                column: "SubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBoardMembers_AgentCompanyId",
                table: "CompanyBoardMembers",
                column: "AgentCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBoardMembers_AgentId",
                table: "CompanyBoardMembers",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBoardMembers_BoardofDirectorsLegalMemberId",
                table: "CompanyBoardMembers",
                column: "BoardofDirectorsLegalMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBoardMembers_CompanyId",
                table: "CompanyBoardMembers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBoardMembers_EducationDegreeId",
                table: "CompanyBoardMembers",
                column: "EducationDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBoardMembers_LegalMemberTypeId",
                table: "CompanyBoardMembers",
                column: "LegalMemberTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyBoardMembers_OrganizationalPositionId",
                table: "CompanyBoardMembers",
                column: "OrganizationalPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocumentMedias_RowId",
                table: "CompanyDocumentMedias",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocuments_CompanyId",
                table: "CompanyDocuments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocuments_FiscalYearId",
                table: "CompanyDocuments",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocuments_MainGroupId",
                table: "CompanyDocuments",
                column: "MainGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyDocuments_SubGroupId",
                table: "CompanyDocuments",
                column: "SubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExtraordinaryMeetingMedias_RowId",
                table: "CompanyExtraordinaryMeetingMedias",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExtraordinaryMeetings_CompanyId",
                table: "CompanyExtraordinaryMeetings",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExtraordinaryMeetings_FiscalYearId",
                table: "CompanyExtraordinaryMeetings",
                column: "FiscalYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExtraordinaryMeetings_MainGroupId",
                table: "CompanyExtraordinaryMeetings",
                column: "MainGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyExtraordinaryMeetings_SubGroupId",
                table: "CompanyExtraordinaryMeetings",
                column: "SubGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMedias_RowId",
                table: "CompanyMedias",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMeetingAssignmentMedias_RowId",
                table: "CompanyMeetingAssignmentMedias",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyMeetingAssignments_CompanyId",
                table: "CompanyMeetingAssignments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShareholderArchives_CompanyId",
                table: "CompanyShareholderArchives",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShareholderArchives_ShareholderId",
                table: "CompanyShareholderArchives",
                column: "ShareholderId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShareholderArchives_SubShareholderId",
                table: "CompanyShareholderArchives",
                column: "SubShareholderId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShareholders_CompanyId",
                table: "CompanyShareholders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShareholders_ShareholderId",
                table: "CompanyShareholders",
                column: "ShareholderId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShareholders_SubShareholderId",
                table: "CompanyShareholders",
                column: "SubShareholderId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyShares_CompanyId",
                table: "CompanyShares",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_CompanyId",
                table: "CompanyUser",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyUser_UserId",
                table: "CompanyUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EnactmentDetail_EnactmentId",
                table: "EnactmentDetail",
                column: "EnactmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnactmentMedias_RowId",
                table: "EnactmentMedias",
                column: "RowId");

            migrationBuilder.CreateIndex(
                name: "IX_Enactments_CompanyId",
                table: "Enactments",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FiscalYears_CompanyId",
                table: "FiscalYears",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfos_CompanyId",
                table: "Portfos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfoShareholderPairs_CompanyId",
                table: "PortfoShareholderPairs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfoShareholderPairs_PortfoId",
                table: "PortfoShareholderPairs",
                column: "PortfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfoShareholderPairs_ShareholderId",
                table: "PortfoShareholderPairs",
                column: "ShareholderId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleFormActionAssignment_RoleId",
                table: "RoleFormActionAssignment",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGroups_MainGroupId",
                table: "SubGroups",
                column: "MainGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CompanyAnnualMeetings");

            migrationBuilder.DropTable(
                name: "CompanyBoardMembers");

            migrationBuilder.DropTable(
                name: "CompanyDocumentMedias");

            migrationBuilder.DropTable(
                name: "CompanyExtraordinaryMeetingMedias");

            migrationBuilder.DropTable(
                name: "CompanyMedias");

            migrationBuilder.DropTable(
                name: "CompanyMeetingAssignmentMedias");

            migrationBuilder.DropTable(
                name: "CompanyShareholderArchives");

            migrationBuilder.DropTable(
                name: "CompanyShareholders");

            migrationBuilder.DropTable(
                name: "CompanyShares");

            migrationBuilder.DropTable(
                name: "CompanyUser");

            migrationBuilder.DropTable(
                name: "DomainSetting");

            migrationBuilder.DropTable(
                name: "EnactmentDetail");

            migrationBuilder.DropTable(
                name: "EnactmentMedias");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "PortfoShareholderPairs");

            migrationBuilder.DropTable(
                name: "RoleFormActionAssignment");

            migrationBuilder.DropTable(
                name: "Newspapers");

            migrationBuilder.DropTable(
                name: "Auditors");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "BoardofDirectorsLegalMembers");

            migrationBuilder.DropTable(
                name: "EducationDegrees");

            migrationBuilder.DropTable(
                name: "LegalMemberTypes");

            migrationBuilder.DropTable(
                name: "OrganizationalPositions");

            migrationBuilder.DropTable(
                name: "CompanyDocuments");

            migrationBuilder.DropTable(
                name: "CompanyExtraordinaryMeetings");

            migrationBuilder.DropTable(
                name: "CompanyMeetingAssignments");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Enactments");

            migrationBuilder.DropTable(
                name: "Portfos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "FiscalYears");

            migrationBuilder.DropTable(
                name: "SubGroups");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "MainGroups");

            migrationBuilder.DropTable(
                name: "CompanyActivityTypes");

            migrationBuilder.DropTable(
                name: "CompanyPartnershipTypes");

            migrationBuilder.DropTable(
                name: "CompanyTypes");

            migrationBuilder.DropTable(
                name: "Shareholders");
        }
    }
}
