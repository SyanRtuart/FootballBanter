﻿// <auto-generated />
using System;
using Matches.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Matches.Infrastructure.Migrations
{
    [DbContext(typeof(MatchContext))]
    partial class MatchContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:match.matchseq", "'matchseq', 'match', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:match.teamseq", "'teamseq', 'match', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Matches.Domain.Aggregates.Match.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "matchseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "match")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<int>("_awayTeamId")
                        .HasColumnName("AwayTeamId")
                        .HasColumnType("int");

                    b.Property<int>("_homeTeamId")
                        .HasColumnName("HomeTeamId")
                        .HasColumnType("int");

                    b.Property<int>("_statusId")
                        .HasColumnName("StatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("_utcDate")
                        .HasColumnName("UtcDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("matches","match");
                });

            modelBuilder.Entity("Matches.Domain.Aggregates.Match.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:HiLoSequenceName", "teamseq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "match")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("teams","match");
                });

            modelBuilder.Entity("Matches.Domain.Aggregates.Match.Match", b =>
                {
                    b.OwnsOne("Matches.Domain.Aggregates.Match.Score", "_score", b1 =>
                        {
                            b1.Property<int>("MatchId")
                                .HasColumnType("int");

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

                            b1.ToTable("matches");

                            b1.WithOwner()
                                .HasForeignKey("MatchId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
