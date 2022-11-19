using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                    TotalPages = table.Column<int>(type: "int", nullable: false),
                    PublicationYear = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditionNumber = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTag",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTag", x => new { x.BooksId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_BookTag_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HierarchyLevel = table.Column<HierarchyId>(type: "hierarchyid", nullable: false),
                    PageNumber = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0502be3e-a18f-4f6a-9e00-55e1c72679fc"), "Hebrew" },
                    { new Guid("09e61944-0b90-43c8-92e3-7515e743f6eb"), "Javanese" },
                    { new Guid("0ace24cd-facc-4899-a560-87b3ad313425"), "Egyptian" },
                    { new Guid("0b88c149-85e7-4e00-9a3b-3a88990f26cf"), "Icelandic" },
                    { new Guid("0db477f4-cb39-4f8f-9d0a-341185654f31"), "Corsica" },
                    { new Guid("12350841-9256-4c68-a738-57e6427a8f33"), "Estonian" },
                    { new Guid("159d1c72-4a39-42e3-b609-7d4fa61823a8"), "Slovak" },
                    { new Guid("164c00ac-9e32-4ad8-8f82-3fc212559a8e"), "Dutch" },
                    { new Guid("169fbfc8-0de8-47fb-b35c-a0d81f7dfc43"), "Arabic" },
                    { new Guid("19b81293-94d6-403e-a8cf-a7258a163aca"), "Irish" },
                    { new Guid("1b8d74c2-aa0b-409b-a951-1f081a73cd20"), "Hindi" },
                    { new Guid("1e3add34-bf36-4da5-bf40-438aa4b2a529"), "Telugu" },
                    { new Guid("20231879-989f-4a62-bdbd-26db80da38e0"), "Sanskrit" },
                    { new Guid("2210afea-f3e1-469c-86e4-0384b29b08cc"), "Czech" },
                    { new Guid("23d8d8c1-2ac5-4f8c-849e-d79c90a5c64f"), "Croatian" },
                    { new Guid("2b3b9806-8171-4754-a256-1bee0d62020b"), "Algerian" },
                    { new Guid("2d153960-45d5-4d5f-9a7c-9cfc32bdfa9a"), "Tagalog" },
                    { new Guid("2dceb4ba-ea02-41e2-bda3-1e48d885a579"), "Berber" },
                    { new Guid("345eb97b-1fe9-464c-9f71-dc3538c4c673"), "Cantonese" },
                    { new Guid("34cf419d-d1f2-4caf-8f55-2b3e827058db"), "Latin" },
                    { new Guid("3946ed87-5cc6-499a-9718-328ec250796d"), "Swedish" },
                    { new Guid("3c306cd9-e5b6-437a-a475-68406d4e4323"), "Tahitian" },
                    { new Guid("3ee0b0b8-be72-4bc5-81a9-eb3248d9a11f"), "Hungarian" },
                    { new Guid("404ec673-3b41-4967-bf54-404444b57c1a"), "Scottish" },
                    { new Guid("418da325-e1ba-4d10-97ed-e83843d68ca1"), "Romanian" },
                    { new Guid("43daad06-35ea-45d6-8fbc-7e6fc55be5db"), "Panjabi" },
                    { new Guid("4797916d-ead2-48cc-bd7b-a002f0e9c8f2"), "Indonesian" },
                    { new Guid("4a8daea6-d2bc-47c3-a4dc-6afdfe9bf6e9"), "Thai" },
                    { new Guid("4bac7ce6-c86c-40fe-bc04-76ed8a4b01b4"), "Creole" },
                    { new Guid("4e60f4c1-8424-48d3-8145-07c12bba08ab"), "Serbian" },
                    { new Guid("51e98920-81cf-4496-a023-0d5ba7efa592"), "Finn" },
                    { new Guid("54cf006f-1799-4125-8d97-d83dc3b5ec32"), "Esperanto" },
                    { new Guid("56316071-856b-4c26-8cf5-089dac172734"), "Aramaic" },
                    { new Guid("583c8e66-a633-4d62-8cf7-943202d592f4"), "Ukrainian" },
                    { new Guid("59f19552-5af3-4bc6-abe9-1ab874995c6d"), "Norwegian" },
                    { new Guid("5c16053e-cf93-4980-b6a3-059570e6a585"), "Russian" },
                    { new Guid("5f9c1c5b-d4fe-44b5-a241-1ae6a774745f"), "Malayalam" },
                    { new Guid("6354f687-4f3f-45a4-ba9a-88574e6c8974"), "Gypsy" },
                    { new Guid("657f752e-c326-44d9-9e63-711694113c59"), "Cypriot" },
                    { new Guid("66efdc74-a929-4bc3-bf57-8e7937253d89"), "Afrikaans" },
                    { new Guid("69b4b6fe-fe4f-4f9c-858b-200f4068b939"), "Danish" },
                    { new Guid("6bbe19ce-bf99-4d14-92eb-89e9a846862b"), "Mandarin" },
                    { new Guid("6ce8d208-1fc0-4957-b5bd-64b0ddef9079"), "Lithuanian" },
                    { new Guid("6d16fe9e-b478-4868-be52-a6902bda0d19"), "Welsh" },
                    { new Guid("70b4d04f-fcf5-4238-bc13-a2b93a844ecc"), "Japanese" },
                    { new Guid("70cdec22-1d8d-4bf1-8786-8316cd63d4a2"), "French" },
                    { new Guid("81076d8c-75f2-4698-afe4-3d32c3de2de9"), "Slovene" },
                    { new Guid("841e73b8-7c85-49b0-9a71-8488d8d194b9"), "Armenian" },
                    { new Guid("8503b30a-876f-4306-99f3-b077466dacee"), "Wu" },
                    { new Guid("85c660d4-4849-46da-8e6f-d7bb32f97f2b"), "Greek" },
                    { new Guid("96ae9cee-2366-44c5-8e3b-a64819f82829"), "Turkish" },
                    { new Guid("9cc8ac3b-d0a5-48a8-a3a9-2bc0f26e1024"), "Portuguese" },
                    { new Guid("a740a2f6-a59a-4e6a-b5e8-eb017328f752"), "Malay" },
                    { new Guid("bad2e9fb-f0a7-4913-b100-ee19e93e7c39"), "Burmese" },
                    { new Guid("bcd66cb0-c198-4001-826d-ca3757ef864d"), "Finnish" },
                    { new Guid("c1cc834f-36de-4ef1-addc-53d25453f897"), "Spanish" },
                    { new Guid("c1e633ad-f991-476a-ad8c-7223966f97ab"), "Tamil" },
                    { new Guid("cb0a28aa-687d-42fc-9b95-465008dad95b"), "Flemish" },
                    { new Guid("cd299394-a791-4fb7-884e-81de530865ad"), "Vietnamese" },
                    { new Guid("cf087fd5-b0f0-48fb-bcdc-a1293bf7206f"), "German" },
                    { new Guid("cfd28780-f2e0-405b-a626-8f26a3da0b3a"), "Polish" },
                    { new Guid("d28ebf89-130b-47a7-b553-b0430d00aa91"), "Korean" },
                    { new Guid("d4a5689a-8d00-42b3-b051-2997ea3a2de6"), "Hawaiian" },
                    { new Guid("d7a48efb-3d1d-4beb-8483-9718d50b9e92"), "English" },
                    { new Guid("d85a7c2a-e59e-456f-8a46-e6b19e2563dc"), "Bengali" },
                    { new Guid("d9864cff-515f-4fd6-a264-54c545782c8d"), "Inuit" },
                    { new Guid("dd126987-caea-4f31-b1fc-09af5770a75d"), "Tibetan" },
                    { new Guid("df4f910c-a42d-4d1c-86f5-3f06bb4ee953"), "Catalan" },
                    { new Guid("e22e00f3-8bf2-44e8-918b-4df52907905d"), "Bosnian" },
                    { new Guid("e3497954-38a0-4520-a97a-49e44cd637f6"), "Brazilian" },
                    { new Guid("e61a7b7f-a2fd-4df6-9fb5-9dacf0743e03"), "Nepalese" },
                    { new Guid("e95b0f69-1197-4b4a-9c02-93e5b7c1cb85"), "Georgian" },
                    { new Guid("f442ba41-ab32-4497-8125-d73053095c13"), "Italian" },
                    { new Guid("fc2ff3a6-c04e-416f-b00c-da2a557f288d"), "Bulgarian" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BooksId",
                table: "AuthorBook",
                column: "BooksId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageId",
                table: "Books",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTag_TagsId",
                table: "BookTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Name",
                table: "Publishers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_BookId",
                table: "Sections",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BookTag");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
