﻿// <auto-generated />
using System;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Matches.Infrastructure.Migrations
{
    [DbContext(typeof(MatchContext))]
    [Migration("20200628171615_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Base.Infrastructure.Inbox.InboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InboxMessages","Match");
                });

            modelBuilder.Entity("Base.Infrastructure.InternalCommands.InternalCommand", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("InternalCommands","Match");
                });

            modelBuilder.Entity("Base.Infrastructure.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages","Match");
                });

            modelBuilder.Entity("Matches.Domain.Match.Match", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("_awayTeamId")
                        .HasColumnName("AwayTeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("_homeTeamId")
                        .HasColumnName("HomeTeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("_statusId")
                        .HasColumnName("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("_utcDate")
                        .HasColumnName("UtcDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Matches","Match");
                });

            modelBuilder.Entity("Matches.Domain.Team.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("_name")
                        .IsRequired()
                        .HasColumnName("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teams","Match");
                });

            modelBuilder.Entity("Matches.Domain.Match.Match", b =>
                {
                    b.OwnsOne("Matches.Domain.Match.Score", "_score", b1 =>
                        {
                            b1.Property<Guid>("MatchId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("AwayTeam")
                                .HasColumnName("ScoreAwayTeam")
                                .HasColumnType("int");

                            b1.Property<int>("HomeTeam")
                                .HasColumnName("ScoreHomeTeam")
                                .HasColumnType("int");

                            b1.Property<string>("Winner")
                                .HasColumnName("ScoreWinner")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("MatchId");

                            b1.ToTable("Matches");

                            b1.WithOwner()
                                .HasForeignKey("MatchId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
