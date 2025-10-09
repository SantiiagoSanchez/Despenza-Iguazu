﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DespensaIguazu.BD.Migrations
{
    /// <inheritdoc />
    public partial class Roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"INSERT INTO AspNetRoles(Id, Name, NormalizedName)
                               VALUES('ba018153-7544-4fbd-a59c-c2660628308e', 'Admin', 'ADMIN')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = 'ba018153-7544-4fbd-a59c-c2660628308e'");
        }
    }
}
