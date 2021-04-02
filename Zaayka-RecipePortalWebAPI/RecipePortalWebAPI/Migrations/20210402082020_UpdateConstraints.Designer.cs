﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipePortalWebAPI.Data;

namespace RecipePortalWebAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210402082020_UpdateConstraints")]
    partial class UpdateConstraints
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RecipePortalWebAPI.Models.Comment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RecipeId");

                    b.Property<int>("UserId");

                    b.Property<string>("commentText");

                    b.Property<DateTime>("datetime");

                    b.HasKey("id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("RecipePortalWebAPI.Models.Recipe", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("UserId");

                    b.Property<string>("category");

                    b.Property<int>("dislikes");

                    b.Property<string>("image");

                    b.Property<string>("ingredients");

                    b.Property<int>("likes");

                    b.Property<string>("method");

                    b.Property<string>("otherdetails");

                    b.Property<string>("title");

                    b.HasKey("id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("RecipePortalWebAPI.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("email")
                        .IsRequired();

                    b.Property<string>("name");

                    b.Property<string>("password")
                        .IsRequired();

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RecipePortalWebAPI.Models.Comment", b =>
                {
                    b.HasOne("RecipePortalWebAPI.Models.Recipe", "Recipe")
                        .WithMany("Comments")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RecipePortalWebAPI.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("RecipePortalWebAPI.Models.Recipe", b =>
                {
                    b.HasOne("RecipePortalWebAPI.Models.User", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}