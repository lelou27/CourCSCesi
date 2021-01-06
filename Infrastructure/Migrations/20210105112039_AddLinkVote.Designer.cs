﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyHN.Infrastructure;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MyHNDbContext))]
    [Migration("20210105112039_AddLinkVote")]
    partial class AddLinkVote
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("MyHN.Domain.Link", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("links");
                });

            modelBuilder.Entity("MyHN.Domain.Link", b =>
                {
                    b.OwnsMany("Domain.Vote", "Votes", b1 =>
                        {
                            b1.Property<Guid>("LinkId")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Direction")
                                .HasColumnType("INTEGER");

                            b1.HasKey("LinkId", "Id");

                            b1.ToTable("link_votes");

                            b1.WithOwner()
                                .HasForeignKey("LinkId");
                        });

                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
